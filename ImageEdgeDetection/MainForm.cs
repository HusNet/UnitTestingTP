/*
 * The Following Code was developed by Dewald Esterhuizen
 * View Documentation at: http://softwarebydefault.com
 * Licensed under Ms-PL 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace ImageEdgeDetection
{
    public partial class MainForm : Form
    {
        private Bitmap originalBitmap = null;
        private Bitmap previewBitmap = null;
        private Bitmap resultBitmap = null;

        public MainForm()
        {
            InitializeComponent();

            cmbEdgeDetection.SelectedIndex = 0;
        }

        private void btnOpenOriginal_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select an image file.";
            ofd.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            ofd.Filter += "|Bitmap Images(*.bmp)|*.bmp";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(ofd.FileName);
                originalBitmap = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
                streamReader.Close();

                previewBitmap = originalBitmap.CopyToSquareCanvas(picPreview.Width);
                picPreview.Image = previewBitmap;
                ;
                ApplyFilter(true);
            }
        }

        private void btnSaveNewImage_Click(object sender, EventArgs e)
        {
            ApplyFilter(false);

            if (resultBitmap != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Specify a file name and file path";
                sfd.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
                sfd.Filter += "|Bitmap Images(*.bmp)|*.bmp";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileExtension = Path.GetExtension(sfd.FileName).ToUpper();
                    ImageFormat imgFormat = ImageFormat.Png;

                    if (fileExtension == "BMP")
                    {
                        imgFormat = ImageFormat.Bmp;
                    }
                    else if (fileExtension == "JPG")
                    {
                        imgFormat = ImageFormat.Jpeg;
                    }

                    StreamWriter streamWriter = new StreamWriter(sfd.FileName, false);
                    resultBitmap.Save(streamWriter.BaseStream, imgFormat);
                    streamWriter.Flush();
                    streamWriter.Close();

                    resultBitmap = null;
                }
            }
        }

        private void ApplyFilter(bool preview)
        {
            if (previewBitmap == null || cmbEdgeDetection.SelectedIndex == -1)
                return;

            Bitmap selectedSource = null;
            Bitmap bitmapResult = null;
            Bitmap tempImage = null;

            if (preview == true)
                selectedSource = previewBitmap;
            else
                selectedSource = originalBitmap;
            
            switch (cmbImageFilter.SelectedItem.ToString())
            {
                case "None":
                    tempImage = selectedSource;
                    break;
                case "Night Filter":
                    tempImage = ImageFilters.ApplyFilter(selectedSource, 1, 1, 1, 25);
                    break;
                case "Hell Filter":
                    tempImage = ImageFilters.ApplyFilter(selectedSource, 1, 1, 10, 15);
                    break;
                case "Miami Filter":
                    tempImage = ImageFilters.ApplyFilter(selectedSource, 1, 1, 10, 1);
                    break;
                case "Zen Filter":
                    tempImage = ImageFilters.ApplyFilter(selectedSource, 1, 10, 1, 1);
                    break;
                case "Black and White":
                    tempImage = ImageFilters.BlackWhite(selectedSource);
                    break;
                case "Swap Filter":
                    tempImage = ImageFilters.ApplyFilterSwap(selectedSource);
                    break;
                case "Crazy Filter":
                    System.Drawing.Image te = ImageFilters.ApplyFilterSwapDivide(selectedSource, 1, 1, 2, 1);
                    tempImage = ImageFilters.ApplyFilterSwap(new Bitmap(te));
                    break;
                case "Mega Filter Green":
                    tempImage = ImageFilters.ApplyFilterMega(selectedSource, 230, 110, Color.Green);
                    break;
                case "Mega Filter Orange":
                    tempImage = ImageFilters.ApplyFilterMega(selectedSource, 230, 110, Color.Orange);
                    break;
                case "Mega Filter Pink":
                    tempImage = ImageFilters.ApplyFilterMega(selectedSource, 230, 110, Color.Pink);
                    break;
                case "Mega Filter Custom":
                    tempImage = ImageFilters.ApplyFilterMega(selectedSource, 230, 110, Color.Blue);
                    break;
                case "Rainbow Filter":
                    tempImage = ImageFilters.RainbowFilter(selectedSource);
                    break;
            }


            switch (cmbEdgeDetection.SelectedItem.ToString())
            {
                case "None":
                    bitmapResult = tempImage;
                    break;
                case "Laplacian 3x3":
                    bitmapResult = tempImage.Laplacian3x3Filter(false);
                    break;
                case "Laplacian 3x3 Grayscale":
                    bitmapResult = tempImage.Laplacian3x3Filter(true);
                    break;
                case "Laplacian 5x5":
                    bitmapResult = tempImage.Laplacian5x5Filter(false);
                    break;
                case "Laplacian 5x5 Grayscale":
                    bitmapResult = tempImage.Laplacian5x5Filter(true);
                    break;
                case "Laplacian of Gaussian":
                    bitmapResult = tempImage.LaplacianOfGaussianFilter();
                    break;
                case "Laplacian 3x3 of Gaussian 3x3":
                    bitmapResult = tempImage.Laplacian3x3OfGaussian3x3Filter();
                    break;
                case "Laplacian 3x3 of Gaussian 5x5 - 1":
                    bitmapResult = tempImage.Laplacian3x3OfGaussian5x5Filter1();
                    break;
                case "Laplacian 3x3 of Gaussian 5x5 - 2":
                    bitmapResult = tempImage.Laplacian3x3OfGaussian5x5Filter2();
                    break;
                case "Laplacian 5x5 of Gaussian 3x3":
                    bitmapResult = tempImage.Laplacian5x5OfGaussian3x3Filter();
                    break;
                case "Laplacian 5x5 of Gaussian 5x5 - 1":
                    bitmapResult = tempImage.Laplacian5x5OfGaussian5x5Filter1();
                    break;
                case "Laplacian 5x5 of Gaussian 5x5 - 2":
                    bitmapResult = tempImage.Laplacian5x5OfGaussian5x5Filter2();
                    break;
                case "Sobel 3x3":
                    bitmapResult = tempImage.Sobel3x3Filter(false);
                    break;
                case "Sobel 3x3 Grayscale":
                    bitmapResult = tempImage.Sobel3x3Filter();
                    break;
                case "Prewitt":
                    bitmapResult = tempImage.PrewittFilter(false);
                    break;
                case "Prewitt Grayscale":
                    bitmapResult = tempImage.PrewittFilter();
                    break;
                case "Kirsch":
                    bitmapResult = tempImage.KirschFilter(false);
                    break;
                case "Kirsch Grayscale":
                    bitmapResult = tempImage.KirschFilter();
                    break;
            }


            if (bitmapResult != null)
            {
                if (preview == true)
                    picPreview.Image = bitmapResult;
                else
                    resultBitmap = bitmapResult;
            }
        }

        private void NeighbourCountValueChangedEventHandler(object sender, EventArgs e)
        {
            ApplyFilter(true);
        }

        private void ImageFilterSelectionEventHandler(object sender, EventArgs e)
        {
            //test aure
            if (cmbImageFilter.SelectedIndex == 0)
                cmbEdgeDetection.Enabled = false;
            else
                cmbEdgeDetection.Enabled = true;

            ApplyFilter(true);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
