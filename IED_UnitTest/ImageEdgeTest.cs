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


        [TestMethod]
        public void EdgeTestPrewittFilter()
        {
            //load the original bitmap
            bitmapOriginal = ImageUtils.loadOriginalPicture();

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

            //result if all pixels are the same
            bool result = ImageUtils.comparePixelImages(bitmapOriginalWithEdge, bitmapExpected);
            bool result_Grayscale = ImageUtils.comparePixelImages(bitmapOriginalWithEdge_GrayScale, bitmapExpected_GrayScale);

            Assert.IsTrue(result);
            Assert.IsTrue(result_Grayscale);

        }

        [TestMethod]
        public void EdgeTestSobel3x3Filter()
        {
            //load the original bitmap
            bitmapOriginal = ImageUtils.loadOriginalPicture();

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


            bool result = ImageUtils.comparePixelImages(bitmapOriginalWithEdge, bitmapExpected);
            bool result_Grayscale = ImageUtils.comparePixelImages(bitmapOriginalWithEdge_GrayScale, bitmapExpected_GrayScale);

            Assert.IsTrue(result);
            Assert.IsTrue(result_Grayscale);


        }


        [TestMethod]
        public void EdgeTestKirschFilter()
        {
            //load the original bitmap
            bitmapOriginal = ImageUtils.loadOriginalPicture();

            //apply Image Filter filter without grayscale
            Bitmap bitmapOriginalWithEdge = ExtBitmap.KirschFilter(bitmapOriginal, false);

            //apply Image Filter filter with grayscale
            Bitmap bitmapOriginalWithEdge_GrayScale = ExtBitmap.KirschFilter(bitmapOriginal, true);


            //apply manually filter on the bitmap expected without grayscale      
            Bitmap bitmapExpected = ExtBitmap.ConvolutionFilter(bitmapOriginal,
                                                Matrix.Kirsch3x3Horizontal,
                                                  Matrix.Kirsch3x3Vertical,
                                                        1.0, 0, false);


            //apply manually filter on the bitmap expected with grayscale      
            Bitmap bitmapExpected_GrayScale = ExtBitmap.ConvolutionFilter(bitmapOriginal,
                                                Matrix.Kirsch3x3Horizontal,
                                                  Matrix.Kirsch3x3Vertical,
                                                        1.0, 0, true);


            bool result = ImageUtils.comparePixelImages(bitmapOriginalWithEdge, bitmapExpected);
            bool result_Grayscale = ImageUtils.comparePixelImages(bitmapOriginalWithEdge_GrayScale, bitmapExpected_GrayScale);

            Assert.IsTrue(result);
            Assert.IsTrue(result_Grayscale);


        }


    }
}
