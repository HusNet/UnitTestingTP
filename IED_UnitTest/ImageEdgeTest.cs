using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using ImageEdgeDetection;
using System.IO;



namespace IED_UnitTest
{
    [TestClass]
    public class ImageEdgeTest
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
        public void EdgeTestPrewittFilter(Bitmap bitmapImg)
        {
            //load the original bitmap
            bitmapOriginal = loadOriginalPicture();

            //apply Image Filter filter without grayscale
            Bitmap bitmapOriginalWithEdge = ExtBitmap.PrewittFilter(bitmapOriginal, false);

            //apply Image Filter filter with grayscale
            Bitmap bitmapOriginalWithEdge_GrayScale = ExtBitmap.PrewittFilter(bitmapOriginal, true);


            //apply manually filter on the bitmap expected without grayscale      
            Bitmap bitmapExpected = ExtBitmap.ConvolutionFilter(bitmapOriginal,
                                               Matrix.Prewitt3x3Horizontal,
                                                 Matrix.Prewitt3x3Vertical,
                                                        1.0, 0, false);

            //apply manually filter on the bitmap expected with grayscale      
            Bitmap bitmapExpected_GrayScale = ExtBitmap.ConvolutionFilter(bitmapOriginal,
                                              Matrix.Prewitt3x3Horizontal,
                                                Matrix.Prewitt3x3Vertical,
                                                       1.0, 0, true);


            bool result = comparePixelImages(bitmapOriginalWithEdge, bitmapExpected);
            bool result_Grayscale = comparePixelImages(bitmapOriginalWithEdge_GrayScale, bitmapExpected_GrayScale);

            Assert.IsTrue(result);
            Assert.IsTrue(result_Grayscale);

        }

        [TestMethod]
        public void EdgeTestSobel3x3Filter(Bitmap bitmapImg)
        {
            //load the original bitmap
            bitmapOriginal = loadOriginalPicture();

            //apply Image Filter filter without grayscale
            Bitmap bitmapOriginalWithEdge = ExtBitmap.Sobel3x3Filter(bitmapOriginal, false);

            //apply Image Filter filter with grayscale
            Bitmap bitmapOriginalWithEdge_GrayScale = ExtBitmap.Sobel3x3Filter(bitmapOriginal, true);


            //apply manually filter on the bitmap expected without grayscale      
            Bitmap bitmapExpected = ExtBitmap.ConvolutionFilter(bitmapOriginal,
                                                 Matrix.Sobel3x3Horizontal,
                                                   Matrix.Sobel3x3Vertical,
                                                        1.0, 0, false);


            //apply manually filter on the bitmap expected with grayscale      
            Bitmap bitmapExpected_GrayScale = ExtBitmap.ConvolutionFilter(bitmapOriginal,
                                                 Matrix.Sobel3x3Horizontal,
                                                   Matrix.Sobel3x3Vertical,
                                                        1.0, 0, true);


            bool result = comparePixelImages(bitmapOriginalWithEdge, bitmapExpected);
            bool result_Grayscale = comparePixelImages(bitmapOriginalWithEdge_GrayScale, bitmapExpected_GrayScale);

            Assert.IsTrue(result);
            Assert.IsTrue(result_Grayscale);


        }




    }
}
