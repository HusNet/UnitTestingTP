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
        
        [TestMethod]
        public void FilterTestBlackAndWhite()
        {
            //load the original bitmap
            bitmapOriginal = ImageUtils.loadOriginalPicture();
         
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

            //check if the pixel of the two images are the same
            bool result = ImageUtils.comparePixelImages(bitmapOriginalWithImageFilters, bitmapExpected);

            Assert.IsTrue(result);

        }
    }
}
