using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Windows.Forms;
//using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Printing;
using System.Reflection.Metadata;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Drawing;
using Document = iTextSharp.text.Document;
using Image = iTextSharp.text.Image;
using Rectangle = System.Drawing.Rectangle;
using iTextSharp.text.pdf.codec;

using static XRAY_REPORT_IMAGE.PDF;

namespace XRAY_REPORT_IMAGE
{
    public partial class MainForm : Form
    {
        private Image<Bgr, byte> _image;
        private Image<Gray, byte> _grayImage;
        string imagePath = "";
        private CircleF[] circles;
        private int circleIndex;
     
        public MainForm()
        {
            InitializeComponent();


        }
        private void btnLoadImage_Click(object sender, EventArgs e)
            

        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _image = new Image<Bgr, byte>(openFileDialog.FileName);
                    _grayImage = _image.Convert<Gray, byte>(); // Convert to grayscale
                    imagePath = openFileDialog.FileName;
                    pictureBox1.Image = _image.ToBitmap();
                }
            }
        }

        private void DetectCircles(int i, int j, int k, int l, int m, int n, IInputOutputArray colorImage)

        {
            if (_grayImage == null)
            {
                MessageBox.Show("Please load an image first.");
                return;
            }
       //   Mat image = CvInvoke.Imread(_grayImage, ImreadModes.Grayscale);

            // Apply Gaussian Blur to reduce noise
          //  CvInvoke.GaussianBlur(image, image, new System.Drawing.Size(5, 5), 1.5);

            // Convert to grayscale again to ensure the latest image is used
            _grayImage = _image.Convert<Gray, byte>();
            CvInvoke.MedianBlur(_grayImage, _grayImage, 3);
            // Apply Gaussian blur to reduce noise
           // CvInvoke.GaussianBlur(_grayImage, _grayImage, new Size(5, 5), 1.5);// _grayImage.SmoothGaussian(9);

            // Detect circles using the HoughCircles method
            circles = CvInvoke.HoughCircles(
                _grayImage,                 // Input grayscale image
                HoughModes.Gradient,          // Circle detection method
                i > 0 ? i : 1,  // Ensure valid values
                j > 0 ? j : 1,
                k > 0 ? k : 1,
                l > 0 ? l : 1,
                m,
                n
            );

            // **Create a new copy of the original image** before drawing
            Image<Bgr, byte> tempImage = _image.Clone();

            // Sort detected circles
            List<CircleF> sortedCircles = circles
                .OrderBy(c => Math.Round(c.Center.Y / 10) * 10)  // Group by rows
                .ThenBy(c => c.Center.X)                         // Sort within each row
                .ToList();

            // Draw detected circles
            int circleCount = 1;
            foreach (CircleF circle in sortedCircles)
            {
                CvInvoke.Circle(tempImage, new Point((int)circle.Center.X, (int)circle.Center.Y),
                                (int)circle.Radius, new MCvScalar(213, 6, 1), 2); // Green circle

                // Draw the circle number
                string circleNumberText = circleCount.ToString();
                Rectangle labelBox = new Rectangle((int)circle.Center.X - 15, (int)circle.Center.Y - 25, 30, 15);
                CvInvoke.Rectangle(tempImage, labelBox, new MCvScalar(85, 253, 255), -1); // Black background
                CvInvoke.PutText(tempImage, circleNumberText,
                                 new Point((int)circle.Center.X - 10, (int)circle.Center.Y - 15),
                                 FontFace.HersheyComplex, 0.5, new MCvScalar(37, 135, 59), 1);
                //CvInvoke.PutText(tempImage, circleNumberText,
                // new Point((int)circle.Center.X + 10, (int)circle.Center.Y - 10), // Adjusted position
                // FontFace.HersheyComplex, 0.5, new MCvScalar(255, 0, 0), 1);

                //CvInvoke.PutText(tempImage, circleNumberText,
                //                 new Point((int)circle.Center.X - 10, (int)circle.Center.Y - 20),
                //                 FontFace.HersheyComplex, 0.5, new MCvScalar(255, 0, 0), 1);

                circleCount++;
            }

            // **Update the PictureBox with the new image (clearing old circles)**
            pictureBox1.Image = tempImage.ToBitmap();
            pictureBox1.Refresh();
        }
        //public static void GeneratePDF(string imagePath, Image<Gray, byte> grayImage, CircleF[] circles, string outputPath)
            public static void GeneratePDF(string imagePath, Image<Gray, byte> grayImage, CircleF[] circles, string outputPath)

        {
            using (FileStream stream = new FileStream(outputPath, FileMode.Create))
            {
                Document doc = new Document(PageSize.A4);
                PdfWriter.GetInstance(doc, stream);
                doc.Open();

                iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                Paragraph title = new Paragraph("BGA Analysis Report", titleFont);
                title.Alignment = Element.ALIGN_CENTER; // Works in iTextSharp 5
                doc.Add(title);
                doc.Add(new Paragraph("\n"));

                // Convert grayscale to color image
                Image<Bgr, byte> colorImage = grayImage.Convert<Bgr, byte>();

                // Sort circles from left to right
                //                List<CircleF> sortedCircles = circles.OrderBy(c => c.Center.X).ToList();
                List<CircleF> sortedCircles = circles
                    .OrderBy(c => Math.Round(c.Center.Y / 10) * 10)  // Group by rows (adjust 10 based on spacing)
                    .ThenBy(c => c.Center.X)                         // Sort within each row by X
                    .ToList();

                int circleCount = 1;

                foreach (var circle in sortedCircles)
                {
                    CvInvoke.Circle(colorImage, new Point((int)circle.Center.X, (int)circle.Center.Y),
                                    (int)circle.Radius, new MCvScalar(0, 255, 0), 2); // Green color

                    string circleNumberText = circleCount.ToString();
                    CvInvoke.PutText(colorImage, circleNumberText,
                                    new Point((int)circle.Center.X - 10, (int)circle.Center.Y - 25),
                                    Emgu.CV.CvEnum.FontFace.HersheyComplex, 0.5,
                                    new MCvScalar(0, 255, 0), 1);

                    circleCount++;
                }

                using (Bitmap bmp = colorImage.ToBitmap())
                {
                    MemoryStream ms = new MemoryStream();
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(ms.ToArray());
                    pdfImage.ScaleToFit(400f, 400f);
                    doc.Add(pdfImage);
                }

                doc.Add(new Paragraph("\n"));

                // Create Table for Results
                PdfPTable table = new PdfPTable(4);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 1f, 1f, 1f, 1f });
                table.HeaderRows = 1;

                PdfPCell cell = new PdfPCell(new Phrase("ID"));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("BGA Ball Area (in mm²)"));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Void Area in %"));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Result"));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                // Use sorted circles for table so IDs match labels
                for (int i = 0; i < sortedCircles.Count; i++)
                {
                    CircleF circle = sortedCircles[i]; // Use sorted list
                    double areaInPixels = Math.PI * Math.Pow(circle.Radius, 2);
                    double areaInMM = areaInPixels * 0.2646;
                    double whitePercentage = CalculateWhitePixelPercentage(grayImage, circle);

                    // Add ID (same as label in the image)
                    table.AddCell(new Phrase((i + 1).ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 12)));
                    table.AddCell(new Phrase(areaInMM.ToString("F2"), FontFactory.GetFont(FontFactory.HELVETICA, 12)));
                    table.AddCell(new Phrase(whitePercentage.ToString("F2") + "%", FontFactory.GetFont(FontFactory.HELVETICA, 12)));

                    string condition = whitePercentage > 5 ? "NG" : "Good";
                    iTextSharp.text.Font conditionFont = (whitePercentage > 5)
                        ? new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.RED)
                        : new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.GREEN);

                    table.AddCell(new Paragraph(condition, conditionFont));
                }

                doc.Add(table);
                doc.Add(new Paragraph("\n"));
                doc.Close();
            }
        }
        public static double CalculateWhitePixelPercentage(Image<Gray, byte> grayImage, CircleF circle)
        {
            int cx = (int)circle.Center.X;
            int cy = (int)circle.Center.Y;
            int r = (int)circle.Radius;

            int totalPixels = 0, whitePixels = 0;

            // Create a copy of the grayscale image to avoid modifying the original
            Image<Gray, byte> tempGray = grayImage.Clone();

            // Compute Otsu threshold separately

            Mat binaryImage = new Mat();

            double threshold = CvInvoke.Threshold(tempGray, binaryImage, 0, 255, ThresholdType.Otsu);

            Console.WriteLine($"Otsu Threshold Value: {threshold}");



            for (int y = cy - r; y <= cy + r; y++)
            {
                for (int x = cx - r; x <= cx + r; x++)
                {
                    if (x >= 0 && y >= 0 && x < grayImage.Width && y < grayImage.Height)
                    {
                        double distance = Math.Sqrt((x - cx) * (x - cx) + (y - cy) * (y - cy));

                        if (distance <= r)  // Ensure inside the circle
                        {
                            byte intensity = (byte)grayImage[y, x].Intensity;
                            totalPixels++;

                            if (intensity >= threshold)  // White pixel check
                            {
                                whitePixels++;
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Total Pixels: {totalPixels}, White Pixels: {whitePixels}");

            return (totalPixels == 0) ? 0 : (whitePixels / (double)totalPixels) * 100;
        }

        private void btnExcludeCircles_Click(object sender, EventArgs e)
        {
            //DetectCircles(trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value, trackBar5.Value);
            DetectCircles((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value, (int)numericUpDown4.Value, (int)numericUpDown5.Value, (int)numericUpDown6.Value, _image);
            MessageBox.Show("Feature not implemented yet.");
        }

        //private void DetectCircles(int value1, int value2, int value3, int value4, int value5, int value6)
        //{
        //    throw new NotImplementedException();
        //}

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)

        {
            DetectCircles((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value, (int)numericUpDown4.Value, (int)numericUpDown5.Value, (int)numericUpDown6.Value, _image);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            DetectCircles((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value, (int)numericUpDown4.Value, (int)numericUpDown5.Value, (int)numericUpDown6.Value, _image);

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            DetectCircles((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value, (int)numericUpDown4.Value, (int)numericUpDown5.Value, (int)numericUpDown6.Value, _image);

        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            DetectCircles((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value, (int)numericUpDown4.Value, (int)numericUpDown5.Value, (int)numericUpDown6.Value, _image);

        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            DetectCircles((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value, (int)numericUpDown4.Value, (int)numericUpDown5.Value, (int)numericUpDown6.Value, _image);

        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            DetectCircles((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value, (int)numericUpDown4.Value, (int)numericUpDown5.Value, (int)numericUpDown6.Value, _image);

        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            {
                //SaveImage();  // Call the method to save the image

               PDF.GeneratePDF(imagePath, _grayImage, circles, @"C:\Documents", pictureBox1,(int)numThreshold.Value);
               //PDF.GeneratePDF(imagePath, _grayImage, circles, outputPath, pictureBox1);
                //   GeneratePDF(imagePath, _grayImage, circles, "Circle_Data.pdf");

            }

        }
        private void SaveImage()
        {
            // Use SaveFileDialog to let the user choose the file name and location
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Set default extension and filter
                saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png|BMP Image|*.bmp";
                saveFileDialog.DefaultExt = "jpg";

                // Show the dialog and check if the user selected a file
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Save the image to the specified file path
                    _image.Save(saveFileDialog.FileName);
                    MessageBox.Show("Image saved successfully!");
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btnApplyThreshold_ValueChanged(object sender, EventArgs e)
        {
            if (_grayImage == null || pictureBox1 == null)
            {
                MessageBox.Show("Error: Image or PictureBox is null!");
                return;
            }



            int thresholdValue = (int)numThreshold.Value; // Get the threshold from NumericUpDown
            Mat binaryImage = new Mat();
         //   double threshold = CvInvoke.Threshold(_grayImage, binaryImage, 0, 255, Emgu.CV.CvEnum.ThresholdType.Otsu);

            // Convert binary image to Bitmap and display it
            pictureBox1.Image = binaryImage.ToBitmap();
            List<CircleF> sortedCircles = circles
             .OrderBy(c => Math.Round(c.Center.Y / 10) * 10)  // Group by rows (adjust 10 based on spacing)
             .ThenBy(c => c.Center.X)                         // Sort within each row by X
             .ToList();

            //  CvInvoke.Threshold(_grayImage, binaryImage, thresholdValue, 200, ThresholdType.Binary);
            Mat thresholded = new Mat();
            CvInvoke.Threshold(_grayImage, binaryImage, thresholdValue, 255, ThresholdType.Binary); // Adjust threshold as needed

            Image<Gray, byte> binaryGrayImage = binaryImage.ToImage<Gray, byte>();
           
            int circleCount = 1;
            // Clone original image to draw outlines
            Image<Bgr, byte> resultImage = _image.Clone();

            foreach (CircleF circle in sortedCircles)
            {
                int cx = (int)circle.Center.X;
                int cy = (int)circle.Center.Y;
                int r = (int)circle.Radius;
                int innerRadius = (int)(r * 0.85); // Keeps 85% of the center area, ignoring 15% of edges

                int totalPixels = 0, whitePixels = 0;

                string circleNumberText = circleCount.ToString();
                //Rectangle labelBox = new Rectangle((int)circle.Center.X - 15, (int)circle.Center.Y - 25, 30, 15);
                //CvInvoke.Rectangle(resultImage, labelBox, new MCvScalar(0, 0, 0), -1); // Black background
                //                                                                       //CvInvoke.PutText(resultImage, circleNumberText,
                //                 new Point((int)circle.Center.X - 10, (int)circle.Center.Y - 15),
                //                 FontFace.HersheyComplex, 0.5, new MCvScalar(255, 255, 255), 1);
                ////CvInvoke.PutText(tempImage, circleNumberText,
                CvInvoke.PutText(resultImage, circleNumberText,
                                 new Point((int)circle.Center.X + 20, (int)circle.Center.Y + 15),
                                 FontFace.HersheyComplex, 0.5, new MCvScalar(139, 0, 0), 1);
                // Always draw the detected circle in GREEN
                CvInvoke.Circle(resultImage, new Point(cx, cy), r, new MCvScalar(255, 0, 0), 2);
                
                for (int y = Math.Max(0, cy - innerRadius); y < Math.Min(_grayImage.Height, cy + innerRadius); y++)
                {
                    for (int x = Math.Max(0, cx - innerRadius); x < Math.Min(_grayImage.Width, cx + innerRadius); x++)
                    {
                        double distance = Math.Sqrt((x - cx) * (x - cx) + (y - cy) * (y - cy));

                        if (distance <= innerRadius)  // Inside the circle
                        {
                            totalPixels++;
                            byte intensity = binaryGrayImage.Data[y, x, 0];

                            if (intensity >= 200) // White pixel detected
                            {
                                whitePixels++;
                                resultImage[y, x] = new Bgr(36, 51, 235); // Mark white pixels with RED outline
                            }
                        }
                    }
                }

                // Calculate percentage of white pixels
                double whitePercentage = (totalPixels == 0) ? 0 : (whitePixels / (double)totalPixels) * 100;

                // **Always keep the text visible**
                // CvInvoke.PutText(resultImage, $"#{circleIndex}", new Point(cx - 10, cy + 10),
                // FontFace.HersheyComplex, 0.8, new MCvScalar(0, 0, 255), 1);

                // Get pixel color at label position to decide text color
                MCvScalar textColor = (thresholdValue > 127) ? new MCvScalar(0, 255, 113) : new MCvScalar(0, 255, 113); // Red on light, Blue on dark
                
                CvInvoke.PutText(resultImage, $"{whitePercentage:F1}%",
                                 new Point((int)circle.Center.X , (int)circle.Center.Y ),
                                 FontFace.HersheyComplex, 0.6, textColor, 1);

                //CvInvoke.PutText(resultImage, $"{whitePercentage:F2}%", new Point(cx - 20, cy - 20),
                //                 FontFace.HersheyComplex, 0.6, new MCvScalar(170, 51, 106), 2);

                circleIndex++; // Increment for next circle
                circleCount++;
            }

            
            pictureBox1.Image = resultImage.ToBitmap();
            pictureBox1.Refresh();
            
        }
    }
}

