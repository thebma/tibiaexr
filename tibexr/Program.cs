using System;
using System.IO;

namespace tibexr
{
    class Program
    {
        static void Main(string[] args)
        {
            SprFileHandler sprFileHandler70 = new SprFileHandler(@"data\Tibia7.0.spr");
            sprFileHandler70.SaveRange(0, 100);

            //SprFileHandler sprFileHandler76 = new SprFileHandler(@"data\Tibia7.6.spr");


            //SprFileComparer spriteComparer = new SprFileComparer(sprFileHandler70, sprFileHandler76, sprFileHandler70);
            //spriteComparer.SaveChanged();

            //Console.WriteLine("Reading .dat file..");
            //DatFileHandler datFileHandler = new DatFileHandler(@"data\Tibia.dat");

            Console.ReadLine();
        }
    }
}
