using System;
using System.Linq;

namespace tibexr.Static
{
    public struct TibiaVersion
    {
        public string Fullname;
        public string Shortname;
        public int SpriteSignature;
        public int DatSignature;

        public TibiaVersion(string fullname, int sprSig, int datSig)
        {
            Fullname = fullname;

            string name = fullname.Replace("Tibia ", "");
            int[] semver = fullname
                              .Replace("Tibia ", "")
                              .Split('.')
                              .Select(x => int.Parse(x))
                              .ToArray();

            Shortname = (semver[0] * 100 + semver[1]).ToString();

            SpriteSignature = sprSig;
            DatSignature = datSig;
        }

        public string GetSpriteSignatureInHex()
        {
            return SpriteSignature.ToString("X0");
        }

        public string GetDatSignatureInHex()
        {
            return DatSignature.ToString("X0");
        }
    }

    public class TibiaVersions
    {
        public static TibiaVersion[] Versions = new TibiaVersion[]
        {
            new TibiaVersion("Tibia 7.00", 0, 0),
            new TibiaVersion("Tibia 7.01", 0, 0),
            new TibiaVersion("Tibia 7.02", 0, 0),
            new TibiaVersion("Tibia 7.10",  0, 0),
            new TibiaVersion("Tibia 7.13",  0, 0),
            new TibiaVersion("Tibia 7.21",  0, 0),
            new TibiaVersion("Tibia 7.26",  0, 0),
            new TibiaVersion("Tibia 7.27",  0, 0),
            new TibiaVersion("Tibia 7.30",  0, 0),
            new TibiaVersion("Tibia 7.40",  0, 0),
            new TibiaVersion("Tibia 7.41",  0, 0),
            new TibiaVersion("Tibia 7.50",  0, 0),
            new TibiaVersion("Tibia 7.60",  0, 1134385715),
            new TibiaVersion("Tibia 7.70",  0, 0),
            new TibiaVersion("Tibia 7.72",  0, 0),
            new TibiaVersion("Tibia 7.80",  0, 0),
            new TibiaVersion("Tibia 7.81",  0, 0),
            new TibiaVersion("Tibia 7.90",  0, 0),
            new TibiaVersion("Tibia 7.92",  0, 0),
            new TibiaVersion("Tibia 8.10",  0, 0),
            new TibiaVersion("Tibia 8.11",  0, 0),
            new TibiaVersion("Tibia 8.20",  0, 0),
            new TibiaVersion("Tibia 8.21",  0, 0),
            new TibiaVersion("Tibia 8.22",  0, 0),
            new TibiaVersion("Tibia 8.30",  0, 0),
            new TibiaVersion("Tibia 8.31",  0, 0),
            new TibiaVersion("Tibia 8.40",  0, 0),
            new TibiaVersion("Tibia 8.41",  0, 0),
            new TibiaVersion("Tibia 8.42",  0, 0),
            new TibiaVersion("Tibia 8.50",  0, 0),
            new TibiaVersion("Tibia 8.52",  0, 0),
            new TibiaVersion("Tibia 8.53",  0, 0),
            new TibiaVersion("Tibia 8.54",  0, 0),
            new TibiaVersion("Tibia 8.55",  0, 0),
            new TibiaVersion("Tibia 8.56",  0, 0),
            new TibiaVersion("Tibia 8.57",  0, 0),
            new TibiaVersion("Tibia 8.60",  0, 0),
            new TibiaVersion("Tibia 8.61",  0, 0),
            new TibiaVersion("Tibia 8.62",  0, 0),
            new TibiaVersion("Tibia 8.70",  0, 0),
            new TibiaVersion("Tibia 8.71",  0, 0),
            new TibiaVersion("Tibia 9.00",  0, 0),
            new TibiaVersion("Tibia 9.10",  0, 0),
            new TibiaVersion("Tibia 9.20",  0, 0),
            new TibiaVersion("Tibia 9.31",  0, 0),
            new TibiaVersion("Tibia 9.40",  0, 0),
            new TibiaVersion("Tibia 9.41",  0, 0),
            new TibiaVersion("Tibia 9.42",  0, 0),
            new TibiaVersion("Tibia 9.43",  0, 0),
            new TibiaVersion("Tibia 9.44",  0, 0),
            new TibiaVersion("Tibia 9.52",  0, 0),
            new TibiaVersion("Tibia 9.60",  0, 0),
            new TibiaVersion("Tibia 9.70",  0, 0),
            new TibiaVersion("Tibia 9.80",  0, 0),
            new TibiaVersion("Tibia 9.81",  0, 0),
            new TibiaVersion("Tibia 10.00",  0, 0),
            new TibiaVersion("Tibia 10.10",  0, 0),
            new TibiaVersion("Tibia 10.20",  0, 0),
            new TibiaVersion("Tibia 10.35",  0, 0),
            new TibiaVersion("Tibia 10.36",  0, 0),
            new TibiaVersion("Tibia 10.37",  0, 0),
            new TibiaVersion("Tibia 10.38",  0, 0),
            new TibiaVersion("Tibia 10.41",  0, 0),
            new TibiaVersion("Tibia 10.50",  0, 0),
            new TibiaVersion("Tibia 10.55",  0, 0),
            new TibiaVersion("Tibia 10.57",  0, 0),
            new TibiaVersion("Tibia 10.59",  0, 0),
            new TibiaVersion("Tibia 10.61",  0, 0),
            new TibiaVersion("Tibia 10.63",  0, 0),
            new TibiaVersion("Tibia 10.64",  0, 0),
            new TibiaVersion("Tibia 10.70",  0, 0),
            new TibiaVersion("Tibia 10.71",  0, 0),
            new TibiaVersion("Tibia 10.73",  0, 0),
            new TibiaVersion("Tibia 10.75",  0, 0),
            new TibiaVersion("Tibia 10.76",  0, 0),
            new TibiaVersion("Tibia 10.77",  0, 0),
            new TibiaVersion("Tibia 10.78",  0, 0),
            new TibiaVersion("Tibia 10.79",  0, 0),
            new TibiaVersion("Tibia 10.80",  0, 0),
            new TibiaVersion("Tibia 10.81",  0, 0),
            new TibiaVersion("Tibia 10.82",  0, 0),

            //We don't support these versions yet.
            //new TibiaVersion("Tibia 10.90",  0, 0),
            //new TibiaVersion("Tibia 10.91",  0, 0),
            //new TibiaVersion("Tibia 10.92",  0, 0),
            //new TibiaVersion("Tibia 10.93",  0, 0),
            //new TibiaVersion("Tibia 10.94",  0, 0),
            //new TibiaVersion("Tibia 10.95",  0, 0),
            //new TibiaVersion("Tibia 10.96",  0, 0),
            //new TibiaVersion("Tibia 10.97",  0, 0),
            //new TibiaVersion("Tibia 10.98",  0, 0),
            //new TibiaVersion("Tibia 10.99",  0, 0),
            //new TibiaVersion("Tibia 11.00",  0, 0),
        };

        public static bool IsVersionSupported(string shortCode)
        {
            foreach (TibiaVersion tibVer in Versions)
            {
                if (tibVer.Shortname.Equals(shortCode, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsDatVersionSupported(uint signature)
        {
            foreach (TibiaVersion tibVer in Versions)
            {
                if (tibVer.DatSignature == signature)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsSprVersionSupported(uint signature)
        {
            foreach (TibiaVersion tibVer in Versions)
            {
                if (tibVer.DatSignature == signature)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
