using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace tibexr
{
    public struct SprHeader
    {
        public uint Signature;
        public ushort Size;

        public int ByteSize()
        {
            return (32 + 16) / 8; // 32 bits for signature, 16 bits for size. divide to get whole bytes.
        }
    }

    public struct SprAddress
    {
        public int Id;
        public uint Address;
    }

    public struct SprData
    {
        public SprAddress MetaInfo;
        public SprDataColor ColorKey;
        public List<SprDataPixel> Pixels;

        public SprData(SprAddress addr, SprDataColor color)
        {
            MetaInfo = addr;
            ColorKey = color;
            Pixels = new List<SprDataPixel>();
        }

    }

    public struct SprDataPixel
    {
        public int Position;
        public SprDataColor Color;

        public SprDataPixel(int position, byte[] color)
        {
            Position = position;
            Color = new SprDataColor(color);
        }

        public SprDataPixel(int position, byte r, byte g, byte b)
        {
            Position = position;
            Color = new SprDataColor(r,g,b);
        }
    }

    public struct SprDataColor
    {
        public byte Red;
        public byte Green;
        public byte Blue;

        public SprDataColor(byte r, byte g, byte b)
        {
            Red = r;
            Green = g;
            Blue = b;
        }

        public SprDataColor(byte[] bytes)
        {
            Red = bytes[0];
            Green = bytes[1];
            Blue = bytes[2];
        }
    }

    public class SprFileHandler
    {
        private const int CHANNELS = 3;
        private const int SIZE = 32;

        private readonly Dictionary<uint, bool> supportedSignatures = new Dictionary<uint, bool>
        {
            { 1030577315, true }, // 7.0 -> 0x3D6D5CA3
            { 1102703238, true }, // 7.4 -> 0x41B9EA86
            { 1134056126, true }, // 7.6 -> 0x439852BE
            { 1277298068, true } // 8.6 old -> 0x4C220594
        };

        private SprHeader Header;
        private List<SprAddress> SpriteAddressTable = new List<SprAddress>();
        private List<SprData> Sprites = new List<SprData>();

        public SprFileHandler(string filePath)
        {
            string file = Path.Combine(Environment.CurrentDirectory, filePath);

            Console.WriteLine($"Reading .spr file at {file}");

            FileStream sprFileStream = null;
            BinaryReader sprBinReader = null;

            try
            {
                sprFileStream = File.Open(file, FileMode.Open, FileAccess.Read);
                sprBinReader = new BinaryReader(sprFileStream);

                (bool found, SprHeader header) = ExtractHeader(sprBinReader);

                if (!found)
                {
                    return;
                }

                Header = header;

                ExtractSpriteAddressTable(sprBinReader);

                foreach (SprAddress addr in SpriteAddressTable)
                {
                    SprData sprite = ExtractSpriteData(sprBinReader, addr);
                    Sprites.Add(sprite);
                }

                Console.WriteLine($"Extracted {Sprites.Count} sprites from the .spr file!");
            }
            finally
            {
                if (sprBinReader != null)
                    sprBinReader.Close();

                if (sprFileStream != null)
                    sprFileStream.Close();
            }
        }

        private (bool, SprHeader) ExtractHeader(BinaryReader sprBinReader)
        {
            //Make sure we are at the start of the stream.
            sprBinReader.BaseStream.Seek(0, SeekOrigin.Begin);

            uint signature = sprBinReader.ReadUInt32();

            bool found = supportedSignatures.TryGetValue(signature, out bool isSupported);

            if (!found || !isSupported)
            {
                Console.WriteLine($"Signature 0x{signature.ToString("X0")} is not supported at this time.");
                return (false, default);
            }
            else
            {
                Console.WriteLine($"Signature 0x{signature.ToString("X0")} found.");
            }

            ushort size = sprBinReader.ReadUInt16();

            Console.WriteLine($"The .spr file contains {size} sprites, trying to extract...");

            SprHeader header = new SprHeader
            {
                Signature = signature,
                Size = size
            };

            return (true, header);
        }

        private void ExtractSpriteAddressTable(BinaryReader sprBinReader)
        {
            if (Header.Equals(default))
            {
                Console.WriteLine("ERROR: Trying to extract SAT when we don't have a header, did ExtractHeader run successfully?");
                return;
            }

            /*
                Put the buffer offset to 6, so we can skip the header of the sprite file.
             */

            sprBinReader.BaseStream.Seek(Header.ByteSize(), SeekOrigin.Begin);

            for (int spriteId = 0; spriteId < (Header.Size - 1); spriteId++)
            {
                uint address = sprBinReader.ReadUInt32();

                if (address != 0)
                    SpriteAddressTable.Add(new SprAddress { Id = spriteId, Address = address });
            }

            Console.WriteLine($"Found {SpriteAddressTable.Count} useable sprite addresses.");
        }

        private SprData ExtractSpriteData(BinaryReader sprBinReader, SprAddress addr)
        {
            sprBinReader.BaseStream.Seek(addr.Address, SeekOrigin.Begin);

            byte[] colorKey = sprBinReader.ReadBytes(3);

            long offset = sprBinReader.ReadUInt16();
            long loopToPosition = sprBinReader.BaseStream.Position + offset;
            int pixels = 0;

            SprData sprite = new SprData(addr, new SprDataColor(colorKey));

            while (sprBinReader.BaseStream.Position < loopToPosition)
            {
                //Write the pixels that are "transparent" with our color key.
                ushort transparentPixels = sprBinReader.ReadUInt16();

                for (int i = 0; i < transparentPixels; i++)
                {
                    int position = pixels * CHANNELS;

                    sprite.Pixels.Add(
                        new SprDataPixel(position, colorKey)
                    );

                    pixels++;
                }

                //Write the pixels with an unique color we can read out.
                ushort coloredPixels = sprBinReader.ReadUInt16();

                for (int i = 0; i < coloredPixels; i++)
                {
                    byte r = sprBinReader.ReadByte();
                    byte g = sprBinReader.ReadByte();
                    byte b = sprBinReader.ReadByte();

                    int position = pixels * CHANNELS;

                    sprite.Pixels.Add(
                        new SprDataPixel(position, r, g, b)
                    );

                    pixels++;
                }
            }

            //Fill in the remaining pixels, due to compression reasons this isn't included.
            //(then it makes me wonder why they didn't do a "count to first pixel" optimization.
            int trailingPixels = (SIZE * SIZE) - pixels;

            for (int i = 0; i < trailingPixels; i++)
            {
                int position = pixels * CHANNELS;

                sprite.Pixels.Add(
                    new SprDataPixel(position, colorKey)
                );

                pixels++;
            }

            return sprite;
        }

        public void SaveOne(SprData data)
        {
            //TODO: Reconstruct pixel data array.
            byte[] bmpDataArray = new byte[3072];

            foreach (SprDataPixel pixel in data.Pixels)
            {
                bmpDataArray[pixel.Position] = pixel.Color.Blue;
                bmpDataArray[pixel.Position + 1] = pixel.Color.Green;
                bmpDataArray[pixel.Position + 2] = pixel.Color.Red;

            }

            ImageFile sprImg = new ImageFile();
            sprImg.SetData(data);
            sprImg.WriteImage(ImageFileFormat.Png);
        }

        public void SaveAll()
        {
            foreach (SprData sprite in Sprites)
            {
                SaveOne(sprite);
            }
        }

        public void SaveRange(int min, int max)
        {
            foreach (SprData sprite in Sprites)
            {
                if (sprite.MetaInfo.Id < min) continue;
                if (sprite.MetaInfo.Id > max) continue;

                SaveOne(sprite);
            }
        }

        public void SaveSpecific(int id)
        {
            foreach (SprData sprite in Sprites)
            {
                if (sprite.MetaInfo.Id != id) continue;
                SaveOne(sprite);
            }
        }

        public int SpriteCount()
        {
            return Sprites.Count;
        }

        public SprData GetSprite(int id)
        {
            foreach (SprData sprite in Sprites)
            {
                if (sprite.MetaInfo.Id == id)
                {
                    return sprite;
                }
            }

            return default;
        }
    }
}
