using System;
using System.Collections.Generic;
using System.Text;

namespace tibexr.Util
{
    public static class PrettyPrint
    {
        static string currentStep = "";

        static string Timestamp
        {
            get
            {
                return ((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
            }
        }

        public static void PPStep(string step)
        {
            currentStep = step;
        }

        public static void PPFormat(string message)
        {
            Console.WriteLine($"[{Timestamp}] [{currentStep.ToUpper()}] {message}");
        }

        public static void PPFormatInline(string message)
        {
            Console.Write($"[{Timestamp}] [{currentStep.ToUpper()}] {message}");
        }

        public static void PPFormatRawInline(string message)
        {
            Console.Write(message);
        }

        public static void PPClear()
        {
            Console.Clear();
        }

        public static void PPWait()
        {
            PPFormatInline("Resetting");
            for (int i = 0; i < 4; i++)
            {
                PPFormatRawInline(".");
                System.Threading.Thread.Sleep(1000);
            }

        }

    }
}
