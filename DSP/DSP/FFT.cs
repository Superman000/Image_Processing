//FFT
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace DSP
{
    struct COMPLEX
    {
        public double real, imag;

        public COMPLEX(double x, double y)
        {
            real = x;
            imag = y;
        }

        public float Magnitude()
        {
            return ((float)Math.Sqrt(real*real + imag*imag));
        }

        public float Phase()
        {
            return ((float)Math.Atan(imag/real));
        }

    }

    class FFT
    {
        public Bitmap Obj;               // Input Object Image
        public Bitmap FourierPlot;       // Generated Fouruer Magnitude Plot

        public int[,] GreyImage;         //GreyScale Image Array Generated from input Image
        public float[,] FourierMagnitude;
        public float[,] FourierPhase;

        float[,] FFTLog;                 // Log of Fourier Magnitude
        float[,] FFTPhaseLog;            // Log of Fourier Phase
        public int[,] FFTNormalized;     // Normalized FFT Magnitude : Scale 0-1
        public int[,] FFTPhaseNormalized;// Normalized FFT Phase : Scale 0-1
        int nx, ny;                      //Number of Points in Width & height
        private int Width, Height;
        COMPLEX[,] Fourier;              //Fourier Magnitude  Array Used for Inverse FFT
        public COMPLEX[,] FFTShifted;    // Shifted FFT 
        public COMPLEX[,] Output;        // FFT Normal
        public COMPLEX[,] FFTNormal;     // FFT Shift Removed - required for Inverse FFT 

        public FFT(Bitmap Input)
        {
            Obj = Input;
            Width = nx = Input.Width;
            Height = ny = Input.Height;
            ReadImage();
        }
       
        public FFT(int[,] Input)
        {
            GreyImage = Input;
            Width = nx = Input.GetLength(0);
            Height = ny = Input.GetLength(1);
        }
        
        public FFT(COMPLEX[,] Input)
        {
            nx = Width = Input.GetLength(0);
            ny = Height = Input.GetLength(1);
            Fourier = Input;

        }

        private void ReadImage()
        {
            int i, j;
            GreyImage = new int[Width, Height];                                                                 //i = row, j = column
            Bitmap image = Obj;
            BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer = (byte*)bitmapData.Scan0;                                                  //Pointer to matrix elements

                for (i = 0; i < bitmapData.Height; i++)
                {
                    for (j = 0; j < bitmapData.Width; j++)
                    {
                        GreyImage[j, i] = (int)((imagePointer[0] + imagePointer[1] + imagePointer[2])/3.0);
                        imagePointer += 4;
                    }
                    
                    imagePointer += bitmapData.Stride - (bitmapData.Width*4);

                }
            }
            image.UnlockBits(bitmapData);

            return;
        }

        public Bitmap Displayimage(int[,] image)
        {
            int i, j;
            Bitmap output = new Bitmap(image.GetLength(0), image.GetLength(1));
            BitmapData bitmapData1 = output.LockBits(new Rectangle(0, 0, image.GetLength(0), image.GetLength(1)),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                for (i = 0; i < bitmapData1.Height; i++)
                {
                    for (j = 0; j < bitmapData1.Width; j++)
                    {
                        imagePointer1[0] = (byte)image[j, i];
                        imagePointer1[1] = (byte)image[j, i];
                        imagePointer1[2] = (byte)image[j, i];
                        imagePointer1[3] = 255;
                        //4 bytes per pixel
                        imagePointer1 += 4;
                    }//end for j
                    //4 bytes per pixel
                    imagePointer1 += (bitmapData1.Stride - (bitmapData1.Width * 4));
                }//end for i
            }//end unsafe
            output.UnlockBits(bitmapData1);
            return output;// col;

        }

        public void ForwardFFT()
        {
            //Initializing Fourier Transform Array
            int i, j;
            Fourier = new COMPLEX[Width, Height];
            Output = new COMPLEX[Width, Height];
            //Copy Image Data to the Complex Array
            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    Fourier[i, j].real = (double)GreyImage[i, j];
                    Fourier[i, j].imag = 0;
                }
            //Calling Forward Fourier Transform
            Output = FFT2D(Fourier, nx, ny, 1);
            return;
        }
 
        public void FFTShift()
        {
            int i, j;
            FFTShifted = new COMPLEX[nx, ny];

            for (i = 0; i <= (nx / 2) - 1; i++)
                for (j = 0; j <= (ny / 2) - 1; j++)
                {
                    FFTShifted[i + (nx / 2), j + (ny / 2)] = Output[i, j];
                    FFTShifted[i, j] = Output[i + (nx / 2), j + (ny / 2)];
                    FFTShifted[i + (nx / 2), j] = Output[i, j + (ny / 2)];
                    FFTShifted[i, j + (nx / 2)] = Output[i + (nx / 2), j];
                }

            return;
        }

        public void RemoveFFTShift()
        {
            int i, j;
            FFTNormal = new COMPLEX[nx, ny];

            for (i = 0; i <= (nx / 2) - 1; i++)
                for (j = 0; j <= (ny / 2) - 1; j++)
                {
                    FFTNormal[i + (nx / 2), j + (ny / 2)] = FFTShifted[i, j];
                    FFTNormal[i, j] = FFTShifted[i + (nx / 2), j + (ny / 2)];
                    FFTNormal[i + (nx / 2), j] = FFTShifted[i, j + (ny / 2)];
                    FFTNormal[i, j + (nx / 2)] = FFTShifted[i + (nx / 2), j];
                }
            return;
        }

        public void FFTPlot(COMPLEX[,] Output)
        {
            int i, j;
            float max;

            FFTLog = new float[nx, ny];
            FFTPhaseLog = new float[nx, ny];

            FourierMagnitude = new float[nx, ny];
            FourierPhase = new float[nx, ny];

            FFTNormalized = new int[nx, ny];
            FFTPhaseNormalized = new int[nx, ny];

            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    FourierMagnitude[i, j] = Output[i, j].Magnitude();
                    FourierPhase[i, j] = Output[i, j].Phase();
                    FFTLog[i, j] = (float)Math.Log(1 + FourierMagnitude[i, j]);
                    FFTPhaseLog[i, j] = (float)Math.Log(1 + Math.Abs(FourierPhase[i, j]));
                }
            //Generating Magnitude Bitmap
            max = FFTLog[0, 0];
            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    if (FFTLog[i, j] > max)
                        max = FFTLog[i, j];
                }
            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    FFTLog[i, j] = FFTLog[i, j] / max;
                }
            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    FFTNormalized[i, j] = (int)(2000 * FFTLog[i, j]);
                }

            FourierPlot = Displayimage(FFTNormalized);
        }
       
        public void InverseFFT()
        {
            int i, j;

            Output = new COMPLEX[nx, ny];
            Output = FFT2D(Fourier, nx, ny, -1);

            Obj = null;  // Setting Object Image to Null
            //Copying Real Image Back to Greyscale
            //Copy Image Data to the Complex Array
            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    GreyImage[i, j] = (int)Output[i, j].Magnitude();

                }
            Obj = Displayimage(GreyImage);
            return;

        }

        public void InverseFFT(COMPLEX[,] Fourier)
        {
            //Initializing Fourier Transform Array
            int i, j;

            //Calling Forward Fourier Transform
            Output = new COMPLEX[nx, ny];
            Output = FFT2D(Fourier, nx, ny, -1);


            //Copying Real Image Back to Greyscale
            //Copy Image Data to the Complex Array
            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    GreyImage[i, j] = (int)Output[i, j].Magnitude();

                }
            Obj = Displayimage(GreyImage);
            return;

        }
        /*-------------------------------------------------------------------------
            Perform a 2D FFT inplace given a complex 2D array
            The direction dir, 1 for forward, -1 for reverse
            The size of the array (nx,ny)
            Return false if there are memory problems or
            the dimensions are not powers of 2
        */
        public COMPLEX[,] FFT2D(COMPLEX[,] c, int nx, int ny, int dir)
        {
            int i, j;
            int m;//Power of 2 for current number of points
            double[] real;
            double[] imag;
            COMPLEX[,] output;//=new COMPLEX [nx,ny];
            output = c; // Copying Array
            // Transform the Rows 
            real = new double[nx];
            imag = new double[nx];

            for (j = 0; j < ny; j++)
            {
                for (i = 0; i < nx; i++)
                {
                    real[i] = c[i, j].real;
                    imag[i] = c[i, j].imag;
                }
                // Calling 1D FFT Function for Rows
                m = (int)Math.Log((double)nx, 2);//Finding power of 2 for current number of points e.g. for nx=512 m=9
                FFT1D(dir, m, ref real, ref imag);

                for (i = 0; i < nx; i++)
                {
                    output[i, j].real = real[i];
                    output[i, j].imag = imag[i];
                }
            }
            // Transform the columns  
            real = new double[ny];
            imag = new double[ny];

            for (i = 0; i < nx; i++)
            {
                for (j = 0; j < ny; j++)
                {
                    real[j] = output[i, j].real;
                    imag[j] = output[i, j].imag;
                }
                // Calling 1D FFT Function for Columns
                m = (int)Math.Log((double)ny, 2);//Finding power of 2 for current number of points e.g. for nx=512 m=9
                FFT1D(dir, m, ref real, ref imag);
                for (j = 0; j < ny; j++)
                {
                    output[i, j].real = real[j];
                    output[i, j].imag = imag[j];
                }
            }
            return (output);
        }
        
        private void FFT1D(int dir, int m, ref double[] x, ref double[] y)
        {
            long nn, i, i1, j, k, i2, l, l1, l2;
            double c1, c2, tx, ty, t1, t2, u1, u2, z;
            /* Calculate the number of points */
            nn = 1;
            for (i = 0; i < m; i++)
                nn *= 2;
            /* Do the bit reversal */
            i2 = nn >> 1;
            j = 0;
            for (i = 0; i < nn - 1; i++)
            {
                if (i < j)
                {
                    tx = x[i];
                    ty = y[i];
                    x[i] = x[j];
                    y[i] = y[j];
                    x[j] = tx;
                    y[j] = ty;
                }
                k = i2;
                while (k <= j)
                {
                    j -= k;
                    k >>= 1;
                }
                j += k;
            }
            /* Compute the FFT */
            c1 = -1.0;
            c2 = 0.0;
            l2 = 1;
            for (l = 0; l < m; l++)
            {
                l1 = l2;
                l2 <<= 1;
                u1 = 1.0;
                u2 = 0.0;
                for (j = 0; j < l1; j++)
                {
                    for (i = j; i < nn; i += l2)
                    {
                        i1 = i + l1;
                        t1 = u1 * x[i1] - u2 * y[i1];
                        t2 = u1 * y[i1] + u2 * x[i1];
                        x[i1] = x[i] - t1;
                        y[i1] = y[i] - t2;
                        x[i] += t1;
                        y[i] += t2;
                    }
                    z = u1 * c1 - u2 * c2;
                    u2 = u1 * c2 + u2 * c1;
                    u1 = z;
                }
                c2 = Math.Sqrt((1.0 - c1) / 2.0);
                if (dir == 1)
                    c2 = -c2;
                c1 = Math.Sqrt((1.0 + c1) / 2.0);
            }
            /* Scaling for forward transform */
            if (dir == 1)
            {
                for (i = 0; i < nn; i++)
                {
                    x[i] /= (double)nn;
                    y[i] /= (double)nn;

                }
            }

            return;
        }

    }
}
