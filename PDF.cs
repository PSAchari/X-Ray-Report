using System;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;


namespace XRAY_REPORT_IMAGE
{

    internal class PDF
    {
        private Image<Bgr, byte> _image;
        public static void GeneratePDF(string imagePath, Image<Gray, byte> grayImage, CircleF[] circles, string outputPath, PictureBox pictureBox1,int numThreshold)
        {
            string userInputName = Microsoft.VisualBasic.Interaction.InputBox(
                "Please enter the name:", "Enter Name", "Default Name");
              outputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), userInputName+".pdf");

            if (string.IsNullOrEmpty(userInputName))
            {
                MessageBox.Show("Name cannot be empty.");
                return;
            }

            using (FileStream stream = new FileStream(outputPath, FileMode.Create))
            {
                Document doc = new Document(PageSize.A4);
                PdfWriter.GetInstance(doc, stream);
                doc.Open();

                iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                Paragraph title = new Paragraph("BGA Inspection Report", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                doc.Add(title);
                doc.Add(new Paragraph("\n"));

             int thresholdValue = (int)numThreshold; // Get the threshold from NumericUpDown
            Mat binaryImage = new Mat();
        //    double threshold = CvInvoke.Threshold(grayImage, binaryImage, 0, 255, Emgu.CV.CvEnum.ThresholdType.Otsu);

            // Convert binary image to Bitmap and display it
            pictureBox1.Image = binaryImage.ToBitmap();
            List<CircleF> sortedCircles = circles
             .OrderBy(c => Math.Round(c.Center.Y / 10) * 10)  // Group by rows (adjust 10 based on spacing)
             .ThenBy(c => c.Center.X)                         // Sort within each row by X
             .ToList();

            CvInvoke.Threshold(grayImage, binaryImage, thresholdValue, 200, ThresholdType.Binary);
                Image<Bgr, byte> resultImage = grayImage.Convert<Bgr, byte>();

                Image<Gray, byte> binaryGrayImage = binaryImage.ToImage<Gray, byte>();
            int circleCount = 1;
                // Clone original image to draw outlines
             
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
                
                for (int y = Math.Max(0, cy - innerRadius); y < Math.Min(grayImage.Height, cy + innerRadius); y++)
                {
                    for (int x = Math.Max(0, cx - innerRadius); x < Math.Min(grayImage.Width, cx + innerRadius); x++)
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

                circleCount++;
            }

            
            pictureBox1.Image = resultImage.ToBitmap();

                // ** Convert Processed Image to Bitmap and Add to PDF **
                using (var bmp = resultImage.ToBitmap())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(ms.ToArray());
                        pdfImage.ScaleToFit(400f, 400f);
                        doc.Add(pdfImage);
                    }
                }

                doc.Add(new Paragraph("\n"));

                // ** Create Table for BGA Analysis Data **
                PdfPTable table = new PdfPTable(7);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 1f, 1f, 2f, 1f, 1f, 1f, 1f });
                table.HeaderRows = 1;

                string[] headers = { "ID", "Type", "Name", "Outline Area (mm²)", "Total Void (%)", "Largest Void (%)", "Status" };

                foreach (string header in headers)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(header, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BackgroundColor = BaseColor.LIGHT_GRAY
                    };
                    table.AddCell(cell);
                }

                // ** Fill Table Rows with Processed Data **
                for (int i = 0; i < sortedCircles.Count; i++)
                {
                    double areaInPixels = Math.PI * Math.Pow(sortedCircles[i].Radius, 2);
                    double areaInMM = areaInPixels * 0.2646;
                    double totalVoid = CalculateWhitePixelPercentage(grayImage, sortedCircles[i], binaryGrayImage,resultImage); // Using thresholded image
                    double largestVoid = totalVoid / 2; // Placeholder calculation

                    string condition = totalVoid > 5 ? "NG" : "Good";
                    iTextSharp.text.Font conditionFont = (totalVoid > 5)
                        ? new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.RED)
                        : new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.GREEN);

                    table.AddCell(new PdfPCell(new Phrase((i + 1).ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase("Circle")) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase(userInputName)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase(areaInMM.ToString("F2"))) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase(totalVoid.ToString("F2") + "%")) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase(largestVoid.ToString("F2") + "%")) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase(condition, conditionFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                }

                doc.Add(table);
                doc.Add(new Paragraph("\n"));
                doc.Close();
            }

            MessageBox.Show("PDF generated successfully.");
        }

 

        private static double CalculateWhitePixelPercentage(Image<Gray, byte> grayImage, CircleF circle, Image<Gray, byte> binaryGrayImage, Image<Bgr, byte> resultImage)
        {
            int cx = (int)circle.Center.X;
            int cy = (int)circle.Center.Y;
            int r = (int)circle.Radius;
            int innerRadius = (int)(r * 0.85); // Keeps 85% of the center area, ignoring 15% of edges

            int totalPixels = 0, whitePixels = 0;

 
            for (int y = Math.Max(0, cy - innerRadius); y < Math.Min(grayImage.Height, cy + innerRadius); y++)
            {
                for (int x = Math.Max(0, cx - innerRadius); x < Math.Min(grayImage.Width, cx + innerRadius); x++)
                {
                    double distance = Math.Sqrt((x - cx) * (x - cx) + (y - cy) * (y - cy));

                    if (distance <= innerRadius)  // Inside the circle
                    {
                        totalPixels++;
                        byte intensity = binaryGrayImage.Data[y, x, 0];

                        if (intensity == 255) // White pixel detected
                        {
                            whitePixels++;
                            resultImage[y, x] = new Bgr(36, 51, 235); // Mark white pixels with RED outline
                        }
                    }
                }
            }

            // Calculate percentage of white pixels
          return  (totalPixels == 0) ? 0 : (whitePixels / (double)totalPixels) * 100;
        }
    }
}
