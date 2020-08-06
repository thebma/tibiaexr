using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace tibexr
{
    public enum ImageFileFormat
    {
        Bitmap,
        Png
    }

    public class ImageFile
    {
        private SprData Data;

        public void SetData(SprData data)
        {
            Data = data;
        }

        public void WriteImage(ImageFileFormat format = ImageFileFormat.Bitmap)
        {
            if (Data.Equals(default)) return;
            if (Data.Pixels == null) return;
            if (Data.Pixels.Count == 0) return;

            //Generate our pixel data in the form of a byte array.
            byte[] imageByteData = new byte[Data.Pixels.Count * 3];
            foreach (SprDataPixel pixel in Data.Pixels)
            {
                imageByteData[pixel.Position] = pixel.Color.Blue;
                imageByteData[pixel.Position + 1] = pixel.Color.Green;
                imageByteData[pixel.Position + 2] = pixel.Color.Red;
            }

            //Write out the image bitmap file.
            Bitmap bmp = null;

            try
            {
                bmp = new Bitmap(32, 32, PixelFormat.Format24bppRgb);

                BitmapData bmpd = bmp.LockBits(
                    new Rectangle(0, 0, 32, 32),
                    ImageLockMode.WriteOnly, bmp.PixelFormat
                );

                Marshal.Copy(imageByteData, 0, bmpd.Scan0, imageByteData.Length);

                bmp.UnlockBits(bmpd);

                if (format == ImageFileFormat.Bitmap)
                {
                    string p = Path.Combine(Environment.CurrentDirectory, $@"output\{Data.MetaInfo.Id}.bmp");
                    File.WriteAllText(p, ""); //HACK: This is used to prevent bmp.Save() from erroring out for no apparent reason.
                    bmp.Save(p, ImageFormat.Bmp);
                }
                else if (format == ImageFileFormat.Png)
                {
                    string p = Path.Combine(Environment.CurrentDirectory, $@"output\{Data.MetaInfo.Id}.png");
                    File.WriteAllText(p, ""); //HACK: This is used to prevent bmp.Save() from erroring out for no apparent reason.
                    bmp.Save(p, ImageFormat.Png);
                }

            }
            finally
            {
                bmp?.Dispose();
            }
        }

        public void WriteAsPng()
        {
        }
    }
}
