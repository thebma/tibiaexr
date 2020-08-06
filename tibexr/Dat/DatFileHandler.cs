using System;
using System.IO;

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

    public class DatFileHandler
    {

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

        public void ExtractHeader(BinaryReader datReader)
        {
            DatHeader header = new DatHeader();


        }
    }

}
