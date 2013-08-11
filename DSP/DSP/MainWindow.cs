using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;



namespace DSP
{

    public partial class MainWindow : System.Windows.Forms.Form
    {
        FFT ImgFFT;
        FFT invFFT;
        private System.Drawing.Bitmap OpenImage;
        private System.Drawing.Bitmap BlueImage;
        private System.Drawing.Bitmap RedImage;
        private System.Drawing.Bitmap GreenImage;
        private System.Drawing.Bitmap bmp;
        private System.Drawing.Bitmap GreyScale;
        private System.Drawing.Bitmap LPF_Image;
        private System.Windows.Forms.MainMenu mainMenu1;
        private double Zoom = 1.0;
        private PictureBox OriginalImage;
        private PictureBox Inverse;
        private Label label1;
        private Label label2;
        private Label label3;
        private PictureBox LP_Filter;
        private Label label4;
        private Label label6;
        private PictureBox FourierMag;
        private PictureBox EdgeRed;
        private Label label7;
        private PictureBox RedComp;
        private PictureBox GreenComp;
        private PictureBox BlueComp;
        private PictureBox RGBEdge;
        private Label label8;
        private Label label9;
        private Label label10;
        private IContainer components;

        public Point current;
        public int rec_width, rec_height;
        private ToolStrip Open;
        private ToolStripButton toolStripButton1;
        private ToolStripButton Exit;
        private ToolStripLabel toolStripLabel1;
        private ToolStripLabel toolStripLabel2;
        private ToolStripLabel toolStripLabel3;
        private ToolStripLabel toolStripLabel4;
        private PictureBox EdgeGreen;
        private PictureBox EdgeBlue;
        private Label label11;
        private Label label12;
        private Label label13;
        public int scale = 100;
        int x, y, width, height;


        public MainWindow()
        {
            InitializeComponent();

            OpenImage = new Bitmap(2, 2);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.OriginalImage = new System.Windows.Forms.PictureBox();
            this.BlackWhite = new System.Windows.Forms.PictureBox();
            this.Inverse = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LP_Filter = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.FourierMag = new System.Windows.Forms.PictureBox();
            this.EdgeRed = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.RedComp = new System.Windows.Forms.PictureBox();
            this.GreenComp = new System.Windows.Forms.PictureBox();
            this.BlueComp = new System.Windows.Forms.PictureBox();
            this.RGBEdge = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.Open = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.Exit = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.EdgeGreen = new System.Windows.Forms.PictureBox();
            this.EdgeBlue = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.OriginalImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlackWhite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Inverse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LP_Filter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FourierMag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EdgeRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedComp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenComp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueComp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RGBEdge)).BeginInit();
            this.Open.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EdgeGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EdgeBlue)).BeginInit();
            this.SuspendLayout();
            // 
            // OriginalImage
            // 
            this.OriginalImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OriginalImage.Location = new System.Drawing.Point(670, 60);
            this.OriginalImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OriginalImage.Name = "OriginalImage";
            this.OriginalImage.Size = new System.Drawing.Size(298, 315);
            this.OriginalImage.TabIndex = 5;
            this.OriginalImage.TabStop = false;
            this.OriginalImage.Click += new System.EventHandler(this.OriginalImage_Click);
            // 
            // BlackWhite
            // 
            this.BlackWhite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BlackWhite.Enabled = false;
            this.BlackWhite.Location = new System.Drawing.Point(14, 58);
            this.BlackWhite.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BlackWhite.Name = "BlackWhite";
            this.BlackWhite.Size = new System.Drawing.Size(298, 315);
            this.BlackWhite.TabIndex = 7;
            this.BlackWhite.TabStop = false;
            // 
            // Inverse
            // 
            this.Inverse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Inverse.Enabled = false;
            this.Inverse.Location = new System.Drawing.Point(336, 58);
            this.Inverse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Inverse.Name = "Inverse";
            this.Inverse.Size = new System.Drawing.Size(298, 315);
            this.Inverse.TabIndex = 8;
            this.Inverse.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(666, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Original Image";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Black and White Image";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(332, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Inverse FFT of Filtered Image";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // LP_Filter
            // 
            this.LP_Filter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LP_Filter.Location = new System.Drawing.Point(17, 774);
            this.LP_Filter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LP_Filter.Name = "LP_Filter";
            this.LP_Filter.Size = new System.Drawing.Size(170, 179);
            this.LP_Filter.TabIndex = 12;
            this.LP_Filter.TabStop = false;
            this.LP_Filter.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 754);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(216, 16);
            this.label4.TabIndex = 13;
            this.label4.Text = "Low-pass Filter of Black/White Image";
            this.label4.Visible = false;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(332, 383);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 16);
            this.label6.TabIndex = 27;
            this.label6.Text = "FFT";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // FourierMag
            // 
            this.FourierMag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FourierMag.Location = new System.Drawing.Point(336, 407);
            this.FourierMag.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FourierMag.Name = "FourierMag";
            this.FourierMag.Size = new System.Drawing.Size(298, 315);
            this.FourierMag.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.FourierMag.TabIndex = 26;
            this.FourierMag.TabStop = false;
            // 
            // EdgeRed
            // 
            this.EdgeRed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EdgeRed.Location = new System.Drawing.Point(670, 750);
            this.EdgeRed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EdgeRed.Name = "EdgeRed";
            this.EdgeRed.Size = new System.Drawing.Size(256, 256);
            this.EdgeRed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.EdgeRed.TabIndex = 31;
            this.EdgeRed.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1000, 726);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 16);
            this.label7.TabIndex = 32;
            this.label7.Text = "RGB Edge Detection";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // RedComp
            // 
            this.RedComp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RedComp.Location = new System.Drawing.Point(671, 407);
            this.RedComp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RedComp.Name = "RedComp";
            this.RedComp.Size = new System.Drawing.Size(298, 315);
            this.RedComp.TabIndex = 33;
            this.RedComp.TabStop = false;
            this.RedComp.Click += new System.EventHandler(this.RedComp_Click);
            // 
            // GreenComp
            // 
            this.GreenComp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GreenComp.Location = new System.Drawing.Point(1004, 64);
            this.GreenComp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GreenComp.Name = "GreenComp";
            this.GreenComp.Size = new System.Drawing.Size(298, 315);
            this.GreenComp.TabIndex = 34;
            this.GreenComp.TabStop = false;
            // 
            // BlueComp
            // 
            this.BlueComp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BlueComp.Location = new System.Drawing.Point(1006, 407);
            this.BlueComp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BlueComp.Name = "BlueComp";
            this.BlueComp.Size = new System.Drawing.Size(298, 315);
            this.BlueComp.TabIndex = 35;
            this.BlueComp.TabStop = false;
            // 
            // RGBEdge
            // 
            this.RGBEdge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RGBEdge.Location = new System.Drawing.Point(1004, 750);
            this.RGBEdge.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RGBEdge.Name = "RGBEdge";
            this.RGBEdge.Size = new System.Drawing.Size(298, 315);
            this.RGBEdge.TabIndex = 36;
            this.RGBEdge.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(667, 383);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 16);
            this.label8.TabIndex = 37;
            this.label8.Text = "Red Component";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1001, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 16);
            this.label9.TabIndex = 38;
            this.label9.Text = "Green Component";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1001, 383);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 16);
            this.label10.TabIndex = 39;
            this.label10.Text = "Blue Component";
            // 
            // Open
            // 
            this.Open.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.Exit,
            this.toolStripLabel3,
            this.toolStripLabel1,
            this.toolStripLabel2,
            this.toolStripLabel4});
            this.Open.Location = new System.Drawing.Point(0, 0);
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(1639, 28);
            this.Open.TabIndex = 40;
            this.Open.Text = "toolStrip1";
            this.Open.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.Open_ItemClicked);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(123, 25);
            this.toolStripButton1.Text = "&Open Image";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // Exit
            // 
            this.Exit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exit.Image = ((System.Drawing.Image)(resources.GetObject("Exit.Image")));
            this.Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(59, 25);
            this.Exit.Text = "&Exit";
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(82, 25);
            this.toolStripLabel3.Text = "                         ";
            this.toolStripLabel3.Click += new System.EventHandler(this.toolStripLabel3_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(160, 25);
            this.toolStripLabel1.Text = "Image Dimensions: ";
            this.toolStripLabel1.Click += new System.EventHandler(this.toolStripLabel1_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(127, 25);
            this.toolStripLabel2.Text = "Width X Height";
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(55, 25);
            this.toolStripLabel4.Text = "pixels";
            // 
            // EdgeGreen
            // 
            this.EdgeGreen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EdgeGreen.Location = new System.Drawing.Point(1326, 60);
            this.EdgeGreen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EdgeGreen.Name = "EdgeGreen";
            this.EdgeGreen.Size = new System.Drawing.Size(298, 315);
            this.EdgeGreen.TabIndex = 41;
            this.EdgeGreen.TabStop = false;
            // 
            // EdgeBlue
            // 
            this.EdgeBlue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EdgeBlue.Location = new System.Drawing.Point(1328, 407);
            this.EdgeBlue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EdgeBlue.Name = "EdgeBlue";
            this.EdgeBlue.Size = new System.Drawing.Size(298, 315);
            this.EdgeBlue.TabIndex = 42;
            this.EdgeBlue.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(666, 726);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(113, 16);
            this.label11.TabIndex = 43;
            this.label11.Text = "Red Edge Detection";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1323, 34);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(124, 16);
            this.label12.TabIndex = 44;
            this.label12.Text = "Green Edge Detection";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1324, 383);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(116, 16);
            this.label13.TabIndex = 45;
            this.label13.Text = "Blue Edge Detection";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1639, 1062);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.EdgeBlue);
            this.Controls.Add(this.EdgeGreen);
            this.Controls.Add(this.Open);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.RGBEdge);
            this.Controls.Add(this.BlueComp);
            this.Controls.Add(this.GreenComp);
            this.Controls.Add(this.RedComp);
            this.Controls.Add(this.EdgeRed);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.FourierMag);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LP_Filter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Inverse);
            this.Controls.Add(this.BlackWhite);
            this.Controls.Add(this.OriginalImage);
            this.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Menu = this.mainMenu1;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EERI 413 Practical 2012 C.R van Zyl 21492204";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OriginalImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlackWhite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Inverse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LP_Filter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FourierMag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EdgeRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedComp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenComp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueComp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RGBEdge)).EndInit();
            this.Open.ResumeLayout(false);
            this.Open.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EdgeGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EdgeBlue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new MainWindow());
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.DrawImage(OpenImage, new Rectangle(this.AutoScrollPosition.X, this.AutoScrollPosition.Y, (int)(OpenImage.Width * Zoom), (int)(OpenImage.Height * Zoom)));
        }


        private void Form1_Load(object sender, System.EventArgs e)
        {
        }


        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void ForwardFFT_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void OriginalImage_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";                                                                                   //Default browsing directory

            if (DialogResult.OK == openFileDialog.ShowDialog())                                                                         //Check if valid image was opened
            {
                OpenImage = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, false);
                toolStripLabel2.Text = OpenImage.Width.ToString() + "  X " + OpenImage.Height.ToString();                               //Display original image dimensions on GUI (Width x Height)
                OpenImage = new Bitmap(OpenImage, 256, 256);                                                                            //Convert to square image of 256 x 256 before FFT and display
                OriginalImage.SizeMode = PictureBoxSizeMode.CenterImage;
                OriginalImage.Image = OpenImage;                
              
                try
                {
                    Bitmap FFTComp = (Bitmap)OpenImage.Clone();
                    bmp = new Bitmap(256, 256, FFTComp.PixelFormat);                                                                    

                    x = (int)((float)current.X * (100 / Convert.ToDouble(scale)));
                    y = (int)((float)current.Y * (100 / Convert.ToDouble(scale)));
                    width = height = (int)(rec_width * (100 / (float)scale));
       
                    Rectangle area = new Rectangle(x, y, 256, 256);
                    bmp = (Bitmap)FFTComp.Clone(area, FFTComp.PixelFormat);
                }

                catch (System.OutOfMemoryException ex)
                {
                    MessageBox.Show("Select Area Inside Image only : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //DFT of Image:
                ImgFFT = new FFT(bmp);
                ImgFFT.ForwardFFT();
                ImgFFT.FFTShift();
                ImgFFT.FFTPlot(ImgFFT.FFTShifted);
                FourierMag.SizeMode = PictureBoxSizeMode.CenterImage;
                FourierMag.Image = (Image)ImgFFT.FourierPlot;

                //Convert and Display Image to Black/White:
                GreyScale = (Bitmap)OpenImage.Clone();
                BitmapFilter.RGB(GreyScale, "Greyscale");
                GreyScale = new Bitmap(GreyScale, 256, 256);
                BlackWhite.SizeMode = PictureBoxSizeMode.CenterImage;
                BlackWhite.Image = GreyScale;
                
                //Low Pass Filter:
                LPF_Image = (Bitmap)GreyScale.Clone();
                BitmapFilter.Smooth(LPF_Image, 5);
                LPF_Image = new Bitmap(LPF_Image, 143, 143);
                
                //Inverse FFT Black and White Filtered Image:
                invFFT = new FFT(LPF_Image);
                invFFT.ForwardFFT();
                invFFT.FFTShift();
                invFFT.FFTPlot(invFFT.FFTShifted);
                invFFT.InverseFFT();
                Inverse.SizeMode = PictureBoxSizeMode.CenterImage;
                Inverse.Image = new Bitmap((Image)invFFT.Obj, 256, 256);

                //RGB Edge Detection:
                Bitmap EdgeDifference = (Bitmap)OpenImage.Clone();
                BitmapFilter.EdgeDetectDifference(EdgeDifference, 127);
                EdgeDifference = new Bitmap(EdgeDifference, 256, 256);
                RGBEdge.SizeMode = PictureBoxSizeMode.CenterImage;
                RGBEdge.Image = EdgeDifference;

                //Red Component of Colour image:
                RedImage = (Bitmap)OpenImage.Clone();
                BitmapFilter.RGB(RedImage, "Red");
                RedImage = new Bitmap(RedImage, 256, 256);
                RedComp.SizeMode = PictureBoxSizeMode.CenterImage;
                RedComp.Image = RedImage;

                //Green Component of Colour image:
                GreenImage = (Bitmap)OpenImage.Clone();
                BitmapFilter.RGB(GreenImage, "Green");
                GreenImage = new Bitmap(GreenImage, 256, 256);
                GreenComp.SizeMode = PictureBoxSizeMode.CenterImage;
                GreenComp.Image = GreenImage;

                //Blue Component of Colour image:
                BlueImage = (Bitmap)OpenImage.Clone();
                BitmapFilter.RGB(BlueImage, "Blue");
                BlueImage = new Bitmap(BlueImage, 256, 256);
                BlueComp.SizeMode = PictureBoxSizeMode.CenterImage;
                BlueComp.Image = BlueImage;

                //Red Edge Detection
                Bitmap RedEdge = (Bitmap)OpenImage.Clone();
                BitmapFilter.Color(RedEdge, -50, 255, 255);
                BitmapFilter.EdgeDetectDifference(RedEdge, 100);
                RedEdge = new Bitmap(RedEdge, 256, 256);
                EdgeRed.SizeMode = PictureBoxSizeMode.CenterImage;
                EdgeRed.Image = RedEdge;

                //Green Edge
                Bitmap GreenEdge = (Bitmap)OpenImage.Clone();
                BitmapFilter.Color(GreenEdge, 255, -50, 255);
                BitmapFilter.EdgeDetectDifference(GreenEdge, 100);
                GreenEdge = new Bitmap(GreenEdge, 256, 256);
                EdgeGreen.SizeMode = PictureBoxSizeMode.CenterImage;
                EdgeGreen.Image = GreenEdge;

                //Blue Edge
                Bitmap BlueEdge = (Bitmap)OpenImage.Clone();
                BitmapFilter.Color(BlueEdge, 255, 255, -50);
                BitmapFilter.EdgeDetectDifference(BlueEdge, 100);
                BlueEdge = new Bitmap(BlueEdge, 256, 256);
                EdgeBlue.SizeMode = PictureBoxSizeMode.CenterImage;
                EdgeBlue.Image = BlueEdge;
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void RedComp_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Open_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        
        private PictureBox BlackWhite;

        private void label7_Click(object sender, EventArgs e)
        {

        }

    }

}

