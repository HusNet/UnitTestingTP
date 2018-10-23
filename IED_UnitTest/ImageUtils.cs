using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace IED_UnitTest
{
    public class ImageUtils
    {

        //Load the image
        public static Bitmap loadOriginalPicture()
        {
            Bitmap bitmapOriginal = null;
            string pathBitmapOriginal = "..\\..\\..\\Assets\\Images\\ImageTesting.jpg";
            StreamReader streamReader = new StreamReader(pathBitmapOriginal);
            bitmapOriginal = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
            streamReader.Close();

            return bitmapOriginal;

        }

        //compare all pixel of two images
        public static bool comparePixelImages(Bitmap bitmapOrignal, Bitmap bitmapExpected)
        {
            if (bitmapOrignal.Size == bitmapExpected.Size)
            {
                for (int x = 0; x < bitmapOrignal.Width; ++x)
                {
                    for (int y = 0; y < bitmapOrignal.Height; ++y)
                    {
                        if (bitmapOrignal.GetPixel(x, y) != bitmapExpected.GetPixel(x, y))
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                return false;

            }

            return true;

        }

    }
}
