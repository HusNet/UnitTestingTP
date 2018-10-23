using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using ImageEdgeDetection;
using System.IO;

namespace IED_UnitTest
{
    [TestClass]
    public class ImageFilterTest
    {
        Bitmap bitmapOriginal;
       
        public Bitmap loadOriginalPicture()
        {
            bitmapOriginal = null;
            string pathBitmapOriginal = "Assets\\Images\\ImageTesting.jpg";
            StreamReader streamReader = new StreamReader(pathBitmapOriginal);
            bitmapOriginal = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
            streamReader.Close();

            return bitmapOriginal;
     
        }

        public bool comparePixelImages(Bitmap bitmapOrignal, Bitmap bitmapExpected)
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


        [TestMethod]
        public void FilterTestBlackAndWhite(Bitmap bitmapImg)
        {
            //load the original bitmap
            bitmapOriginal = loadOriginalPicture();
         
            //apply Image Filter filter
            Bitmap bitmapOriginalWithImageFilters = ImageFilters.BlackWhite(bitmapOriginal);

            //apply manually filter on the bitmap expected     
            Bitmap bitmapExpected = bitmapOriginal;

            int rgb;
            Color c;

            for (int y = 0; y < bitmapExpected.Height; y++)
            {
                 for (int x = 0; x < bitmapExpected.Width; x++)
                {
                    c = bitmapExpected.GetPixel(x, y);
                    rgb = (int)((c.R + c.G + c.B) / 3);
                    bitmapExpected.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb));
                }
            }

            bool result = comparePixelImages(bitmapOriginalWithImageFilters, bitmapExpected);
            Assert.IsTrue(result);



        }
    }
}
