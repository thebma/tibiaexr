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
            Shortname = fullname.Replace("Tibia ", "").Replace(".", "");
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
            new TibiaVersion("Tibia 7.0", 0, 0),
            new TibiaVersion("Tibia 7.01", 0, 0),
            new TibiaVersion("Tibia 7.02", 0, 0),
            new TibiaVersion("Tibia 7.1",  0, 0),
            new TibiaVersion("Tibia 7.13",  0, 0),
            new TibiaVersion("Tibia 7.21",  0, 0),
            new TibiaVersion("Tibia 7.26",  0, 0),
            new TibiaVersion("Tibia 7.27",  0, 0),
            new TibiaVersion("Tibia 7.3",  0, 0),
            new TibiaVersion("Tibia 7.4",  0, 0),
            new TibiaVersion("Tibia 7.41",  0, 0),
            new TibiaVersion("Tibia 7.5",  0, 0),
            new TibiaVersion("Tibia 7.6",  0, 0),
            new TibiaVersion("Tibia 7.7",  0, 0),
            new TibiaVersion("Tibia 7.72",  0, 0),
            new TibiaVersion("Tibia 7.8",  0, 0),
            new TibiaVersion("Tibia 7.81",  0, 0),
            new TibiaVersion("Tibia 7.9",  0, 0),
            new TibiaVersion("Tibia 7.92",  0, 0),
            new TibiaVersion("Tibia 8.1",  0, 0),
            new TibiaVersion("Tibia 8.11",  0, 0),
            new TibiaVersion("Tibia 8.2",  0, 0),
            new TibiaVersion("Tibia 8.21",  0, 0),
            new TibiaVersion("Tibia 8.22",  0, 0),
            new TibiaVersion("Tibia 8.3",  0, 0),
            new TibiaVersion("Tibia 8.31",  0, 0),
            new TibiaVersion("Tibia 8.40",  0, 0),
            new TibiaVersion("Tibia 8.41",  0, 0),
            new TibiaVersion("Tibia 8.42",  0, 0),
            new TibiaVersion("Tibia 8.5",  0, 0),
            new TibiaVersion("Tibia 8.52",  0, 0),
            new TibiaVersion("Tibia 8.53",  0, 0),
            new TibiaVersion("Tibia 8.54",  0, 0),
            new TibiaVersion("Tibia 8.55",  0, 0),
            new TibiaVersion("Tibia 8.56",  0, 0),
            new TibiaVersion("Tibia 8.57",  0, 0),
            new TibiaVersion("Tibia 8.60",  0, 0),
            new TibiaVersion("Tibia 8.61",  0, 0),
            new TibiaVersion("Tibia 8.62",  0, 0),
            new TibiaVersion("Tibia 8.7",  0, 0),
            new TibiaVersion("Tibia 8.71",  0, 0),
            new TibiaVersion("Tibia 9.0",  0, 0),
            new TibiaVersion("Tibia 9.1",  0, 0),
            new TibiaVersion("Tibia 9.2",  0, 0),
            new TibiaVersion("Tibia 9.31",  0, 0),
            new TibiaVersion("Tibia 9.4",  0, 0),
            new TibiaVersion("Tibia 9.41",  0, 0),
            new TibiaVersion("Tibia 9.42",  0, 0),
            new TibiaVersion("Tibia 9.43",  0, 0),
            new TibiaVersion("Tibia 9.44",  0, 0),
            new TibiaVersion("Tibia 9.52",  0, 0),
            new TibiaVersion("Tibia 9.6",  0, 0),
            new TibiaVersion("Tibia 9.7",  0, 0),
            new TibiaVersion("Tibia 9.8",  0, 0),
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
            new TibiaVersion("Tibia 10.90",  0, 0),
            new TibiaVersion("Tibia 10.91",  0, 0),
            new TibiaVersion("Tibia 10.92",  0, 0),
            new TibiaVersion("Tibia 10.93",  0, 0),
            new TibiaVersion("Tibia 10.94",  0, 0),
            new TibiaVersion("Tibia 10.95",  0, 0),
            new TibiaVersion("Tibia 10.96",  0, 0),
            new TibiaVersion("Tibia 10.97",  0, 0),
            new TibiaVersion("Tibia 10.98",  0, 0),
            new TibiaVersion("Tibia 10.99",  0, 0),
            new TibiaVersion("Tibia 11.00",  0, 0),
        };
    }
}
