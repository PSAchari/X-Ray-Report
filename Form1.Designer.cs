
namespace XRAY_REPORT_IMAGE
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            pictureBox1 = new PictureBox();
            groupBox2 = new GroupBox();
            numThreshold = new NumericUpDown();
            btnSave = new Button();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            numericUpDown6 = new NumericUpDown();
            numericUpDown5 = new NumericUpDown();
            numericUpDown4 = new NumericUpDown();
            numericUpDown3 = new NumericUpDown();
            numericUpDown2 = new NumericUpDown();
            numericUpDown1 = new NumericUpDown();
            btnExcludeCircles = new Button();
            btnLoadImage = new Button();
            trackBar5 = new TrackBar();
            trackBar4 = new TrackBar();
            trackBar3 = new TrackBar();
            trackBar2 = new TrackBar();
            trackBar1 = new TrackBar();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numThreshold).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.BackColor = SystemColors.ControlLight;
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.Font = new Font("Arial Narrow", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(640, 581);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "BGA IMAGE";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.Location = new Point(6, 32);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(628, 541);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox2.BackColor = SystemColors.ControlLight;
            groupBox2.Controls.Add(numThreshold);
            groupBox2.Controls.Add(btnSave);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(numericUpDown6);
            groupBox2.Controls.Add(numericUpDown5);
            groupBox2.Controls.Add(numericUpDown4);
            groupBox2.Controls.Add(numericUpDown3);
            groupBox2.Controls.Add(numericUpDown2);
            groupBox2.Controls.Add(numericUpDown1);
            groupBox2.Controls.Add(btnExcludeCircles);
            groupBox2.Controls.Add(btnLoadImage);
            groupBox2.Font = new Font("Arial", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(658, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(291, 581);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "PARAMETERS";
            // 
            // numThreshold
            // 
            numThreshold.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numThreshold.Location = new Point(16, 508);
            numThreshold.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numThreshold.Name = "numThreshold";
            numThreshold.Size = new Size(64, 22);
            numThreshold.TabIndex = 15;
            numThreshold.Value = new decimal(new int[] { 127, 0, 0, 0 });
            numThreshold.ValueChanged += btnApplyThreshold_ValueChanged;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.FlatStyle = FlatStyle.Popup;
            btnSave.Font = new Font("Arial Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(132, 466);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(145, 37);
            btnSave.TabIndex = 14;
            btnSave.Text = "Save Image";
            btnSave.UseVisualStyleBackColor = true;
            //btnSave.Click += btnSave_Click_1;
            btnSave.Click += (sender, e) => btnSave_Click_1(sender, e);
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(4, 415);
            label6.Name = "label6";
            label6.Size = new Size(174, 16);
            label6.TabIndex = 13;
            label6.Text = "Maximum radius of circles";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 353);
            label5.Name = "label5";
            label5.Size = new Size(170, 16);
            label5.TabIndex = 12;
            label5.Text = "Minimum radius of circles";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(4, 290);
            label4.Name = "label4";
            label4.Size = new Size(278, 16);
            label4.TabIndex = 11;
            label4.Text = "Accumulator threshold for center detection";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(4, 228);
            label3.Name = "label3";
            label3.Size = new Size(209, 16);
            label3.TabIndex = 10;
            label3.Text = "Canny edge detection threshold";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 173);
            label2.Name = "label2";
            label2.Size = new Size(271, 16);
            label2.TabIndex = 9;
            label2.Text = "Minimum distance between circle centers";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 117);
            label1.Name = "label1";
            label1.Size = new Size(197, 16);
            label1.TabIndex = 8;
            label1.Text = "Inverse ratio of resolution (dp)";
            // 
            // numericUpDown6
            // 
            numericUpDown6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numericUpDown6.Location = new Point(16, 442);
            numericUpDown6.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown6.Name = "numericUpDown6";
            numericUpDown6.Size = new Size(64, 22);
            numericUpDown6.TabIndex = 7;
            numericUpDown6.Value = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown6.ValueChanged += numericUpDown6_ValueChanged;
            // 
            // numericUpDown5
            // 
            numericUpDown5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numericUpDown5.Location = new Point(16, 378);
            numericUpDown5.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown5.Name = "numericUpDown5";
            numericUpDown5.Size = new Size(64, 22);
            numericUpDown5.TabIndex = 6;
            numericUpDown5.Value = new decimal(new int[] { 8, 0, 0, 0 });
            numericUpDown5.ValueChanged += numericUpDown5_ValueChanged;
            // 
            // numericUpDown4
            // 
            numericUpDown4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numericUpDown4.Location = new Point(16, 317);
            numericUpDown4.Minimum = new decimal(new int[] { 15, 0, 0, 0 });
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(64, 22);
            numericUpDown4.TabIndex = 5;
            numericUpDown4.Value = new decimal(new int[] { 30, 0, 0, 0 });
            numericUpDown4.ValueChanged += numericUpDown4_ValueChanged;
            // 
            // numericUpDown3
            // 
            numericUpDown3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numericUpDown3.Location = new Point(16, 256);
            numericUpDown3.Minimum = new decimal(new int[] { 25, 0, 0, 0 });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(64, 22);
            numericUpDown3.TabIndex = 4;
            numericUpDown3.Value = new decimal(new int[] { 25, 0, 0, 0 });
            numericUpDown3.ValueChanged += numericUpDown3_ValueChanged;
            // 
            // numericUpDown2
            // 
            numericUpDown2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numericUpDown2.Location = new Point(16, 197);
            numericUpDown2.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(64, 22);
            numericUpDown2.TabIndex = 3;
            numericUpDown2.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown2.ValueChanged += numericUpDown2_ValueChanged;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numericUpDown1.Location = new Point(16, 140);
            numericUpDown1.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(64, 22);
            numericUpDown1.TabIndex = 2;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // btnExcludeCircles
            // 
            btnExcludeCircles.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExcludeCircles.FlatStyle = FlatStyle.Popup;
            btnExcludeCircles.Font = new Font("Arial Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExcludeCircles.Location = new Point(45, 64);
            btnExcludeCircles.Name = "btnExcludeCircles";
            btnExcludeCircles.Size = new Size(205, 37);
            btnExcludeCircles.TabIndex = 1;
            btnExcludeCircles.Text = "Exclude Circles";
            btnExcludeCircles.UseVisualStyleBackColor = true;
            btnExcludeCircles.Click += btnExcludeCircles_Click;
            // 
            // btnLoadImage
            // 
            btnLoadImage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLoadImage.FlatStyle = FlatStyle.Popup;
            btnLoadImage.Font = new Font("Arial Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLoadImage.Location = new Point(45, 21);
            btnLoadImage.Name = "btnLoadImage";
            btnLoadImage.Size = new Size(205, 37);
            btnLoadImage.TabIndex = 0;
            btnLoadImage.Text = "Load Image";
            btnLoadImage.UseVisualStyleBackColor = true;
            btnLoadImage.Click += btnLoadImage_Click;
            // 
            // trackBar5
            // 
            trackBar5.BackColor = Color.Navy;
            trackBar5.Location = new Point(755, 427);
            trackBar5.Name = "trackBar5";
            trackBar5.Size = new Size(205, 45);
            trackBar5.TabIndex = 6;
            trackBar5.Visible = false;
            // 
            // trackBar4
            // 
            trackBar4.BackColor = Color.Navy;
            trackBar4.Location = new Point(755, 344);
            trackBar4.Name = "trackBar4";
            trackBar4.Size = new Size(205, 45);
            trackBar4.TabIndex = 5;
            trackBar4.Visible = false;
            // 
            // trackBar3
            // 
            trackBar3.BackColor = Color.Navy;
            trackBar3.Location = new Point(755, 279);
            trackBar3.Name = "trackBar3";
            trackBar3.Size = new Size(205, 45);
            trackBar3.TabIndex = 4;
            trackBar3.Visible = false;
            // 
            // trackBar2
            // 
            trackBar2.BackColor = Color.Navy;
            trackBar2.Location = new Point(755, 210);
            trackBar2.Name = "trackBar2";
            trackBar2.Size = new Size(205, 45);
            trackBar2.TabIndex = 3;
            trackBar2.Visible = false;
            // 
            // trackBar1
            // 
            trackBar1.BackColor = Color.Navy;
            trackBar1.Location = new Point(755, 129);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(205, 45);
            trackBar1.TabIndex = 2;
            trackBar1.Visible = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(956, 605);
            Controls.Add(groupBox2);
            Controls.Add(trackBar5);
            Controls.Add(groupBox1);
            Controls.Add(trackBar1);
            Controls.Add(trackBar4);
            Controls.Add(trackBar2);
            Controls.Add(trackBar3);
            ForeColor = SystemColors.ActiveCaptionText;
            Name = "MainForm";
            Text = "Form1";
            Load += MainForm_Load;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numThreshold).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar5).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar4).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar3).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void BtnSave_Click_1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private PictureBox pictureBox1;
        private Button btnLoadImage;
        private Button btnExcludeCircles;
        private TrackBar trackBar5;
        private TrackBar trackBar4;
        private TrackBar trackBar3;
        private TrackBar trackBar2;
        private TrackBar trackBar1;
        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown2;
        private NumericUpDown numericUpDown3;
        private NumericUpDown numericUpDown4;
        private NumericUpDown numericUpDown5;
        private NumericUpDown numericUpDown6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label6;
        private Button btnSave;
        private NumericUpDown numThreshold;
    }
}
