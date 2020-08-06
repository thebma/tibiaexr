using System;

namespace tibexr
{
    public class SprFileComparer
    {
        private SprFileHandler SpriteFileA;
        private SprFileHandler SpriteFileB;
        private SprFileHandler ExportTarget;

        public SprFileComparer(SprFileHandler a, SprFileHandler b, SprFileHandler exportTarget)
        {
            SpriteFileA = a;
            SpriteFileB = b;
            ExportTarget = exportTarget;
        }

        public void SaveChanged(int pixelChangedThreshold = 0)
        {
            int spriteCount = Math.Min(SpriteFileA.SpriteCount(), SpriteFileB.SpriteCount());

            for (int i = 0; i < spriteCount; i++)
            {
                SprData spriteA = SpriteFileA.GetSprite(i);
                SprData spriteB = SpriteFileB.GetSprite(i);

                //Continue if we cannot find the id in one of the sprite files.
                if ((spriteA.MetaInfo.Id == 0 && spriteA.MetaInfo.Address == 0) ||
                   (spriteB.MetaInfo.Id == 0 && spriteB.MetaInfo.Address == 0))
                {
                    Console.WriteLine("Skipping " + i);
                    continue;
                }

                if (spriteA.MetaInfo.Id != spriteB.MetaInfo.Id)
                {
                    Console.WriteLine("Comparison out of sync!");
                    break;
                }

                int pixelsChanged = 0;
                int pixelCount = Math.Min(spriteA.Pixels.Count, spriteB.Pixels.Count);

                for (int p = 0; p < pixelCount; p++)
                {
                    SprDataPixel pixelA = spriteA.Pixels[p];
                    SprDataPixel pixelB = spriteB.Pixels[p];

                    int diff = Math.Abs(pixelA.Color.Red - pixelB.Color.Red) +
                               Math.Abs(pixelA.Color.Blue - pixelB.Color.Blue) +
                               Math.Abs(pixelA.Color.Green - pixelB.Color.Green);

                    if (diff > 0)
                    {
                        pixelsChanged++;

                        //Early out if possible.
                        if (pixelsChanged > pixelChangedThreshold) break;
                    }
                }

                if (pixelsChanged > pixelChangedThreshold)
                {
                    ExportTarget.SaveSpecific(spriteA.MetaInfo.Id);
                }
            }

        }
    }
}
