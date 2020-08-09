using System;
using System.Collections.Generic;
using System.IO;
using tibexr.Static;

namespace tibexr
{
    public struct DatHeader
    {
        public uint Signature;
        public ushort ItemCount;
        public ushort OutfitCount;
        public ushort EffectCount;
        public ushort MissleCount;
    }

    public struct DatItem
    {
        //Props
    }

    public struct DatFlagDefinition
    {
        public string Name;
        public byte Flag;
        public byte Size;

        public DatFlagDefinition(string name, byte flag, byte size = 1)
        {
            Name = name;
            Flag = flag;
            Size = size;
        }

        public void ApplyFlag(DatItem item)
        {
            if (item.Equals(default)) 
            {
                item = new DatItem();
            }

        }
    }

    public class DatFileHandler
    {
        private List<DatFlagDefinition> Flags = new List<DatFlagDefinition>()
        {
             new DatFlagDefinition("Ground Tile", 0x00, 2),
             new DatFlagDefinition("Clip", 0x01, 0),
             new DatFlagDefinition("Bottom", 0x02, 1),
             new DatFlagDefinition("Top", 0x03, 1),
             new DatFlagDefinition("Container (items)", 0x04, 1),
             new DatFlagDefinition("Stackable", 0x05, 1),
             new DatFlagDefinition("Corpse", 0x06, 1),
             new DatFlagDefinition("Useable", 0x07, 1),
             new DatFlagDefinition("Writable", 0x08, 2),
             new DatFlagDefinition("Readable", 0x09, 2),
             new DatFlagDefinition("Container (fluid)", 0x0A, 1),
             new DatFlagDefinition("Splash", 0x0B, 1),
             new DatFlagDefinition("Blocking", 0x0C, 1),
             new DatFlagDefinition("Immovable", 0x0D, 1),
             new DatFlagDefinition("Blocking (missles)", 0x0E, 1),
             new DatFlagDefinition("Blocking (path)", 0x0F, 1),
             new DatFlagDefinition("Pickupable", 0x10, 1),
             new DatFlagDefinition("Hangable", 0x11, 1),
             new DatFlagDefinition("Horizontal", 0x12, 1),
             new DatFlagDefinition("Vertical", 0x13, 1),
             new DatFlagDefinition("Rotatable", 0x14, 1),
             new DatFlagDefinition("LightInfo", 0x15, 3),
             new DatFlagDefinition("Hide", 0x16, 1),
             new DatFlagDefinition("Translucent", 0x17, 1),
             new DatFlagDefinition("Offset", 0x18, 3),
             new DatFlagDefinition("Height", 0x19, 2),
             new DatFlagDefinition("Layer", 0x1A, 1),
             new DatFlagDefinition("Idle Animation", 0x1B, 1),
             new DatFlagDefinition("Minimap", 0x1C, 2),
             new DatFlagDefinition("Actions", 0x1D, 1),
             new DatFlagDefinition("Ground Item", 0x1E, 1),
        };


        public DatFileHandler(string filePath)
        {
            string file = Path.Combine(Environment.CurrentDirectory, filePath);

            Console.WriteLine($"Reading .dat file at {file}");

            FileStream datFileStream = null;
            BinaryReader datBinReader = null;

            try
            {
                datFileStream = File.Open(file, FileMode.Open, FileAccess.Read);
                datBinReader = new BinaryReader(datFileStream);

                ExtractHeader(datBinReader);
            }
            finally
            {
                if (datBinReader != null)
                    datBinReader.Close();

                if (datFileStream != null)
                    datFileStream.Close();
            }
        }

        public DatHeader ExtractHeader(BinaryReader datReader)
        {
            DatHeader header = new DatHeader();
            uint signature = datReader.ReadUInt32();

            if (!TibiaVersions.IsDatVersionSupported(signature))
            {
                Console.WriteLine($"The .dat version of {signature.ToString("X0")} is not supported.");
                return header;
            }

            ushort itemCount = datReader.ReadUInt16();
            ushort outfitCount = datReader.ReadUInt16();
            ushort effectCount = datReader.ReadUInt16();
            ushort missleCount = datReader.ReadUInt16();

            header.ItemCount = itemCount;
            header.OutfitCount = outfitCount;
            header.EffectCount = effectCount;
            header.MissleCount = missleCount;

            return header;

            //for (int itemId = 0; itemId < itemCount; itemId++)
            //{
            //    byte flag = 0x00;

            //    while (flag != 0xFF)
            //    {
            //        flag = datReader.ReadByte();

            //        Console.WriteLine($"Item {itemId} has flag {flag.ToString("X0")}");
            //    }
            //}


        }
    }

}
