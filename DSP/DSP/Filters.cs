//Filters
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace DSP
{
    public class ConvMatrix
    {
        public int TopLeft = 0, TopMid = 0, TopRight = 0;
        public int MidLeft = 0, Pixel = 1, MidRight = 0;
        public int BottomLeft = 0, BottomMid = 0, BottomRight = 0;
        public int Factor = 1;
        public int Offset = 0;
        public void SetAll(int nVal)
        {
            TopLeft = TopMid = TopRight = MidLeft = Pixel = MidRight = BottomLeft = BottomMid = BottomRight = nVal;
        }
    }

    public class BitmapFilter
    {

        public static bool RGB(Bitmap b, string colour)
        {
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - b.Width * 3;

                byte red, green, blue;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        blue = p[0];
                        green = p[1];
                        red = p[2];

                        if (colour == "Greyscale")
                        {
                            p[0] = p[1] = p[2] = (byte)(0.299 * red + 0.587 * green + 0.114 * blue); 
                        }

                        else if (colour == "Red")
                        {
                            p[2] = (byte)(1 * red);

                            p[0] = 0;
                            p[1] = 0;
                        }

                        else if (colour == "Green")
                        {
                            p[1] = (byte)(1 * green);

                            p[0] = 0;
                            p[2] = 0;
                        }

                        else if (colour == "Blue")
                        {
                            p[0] = (byte)(1 * blue);

                            p[1] = 0;
                            p[2] = 0;
                        }

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }

        public static bool Color (Bitmap b, int red, int green, int blue)
        {
            if (red < -255 || red > 255) return false;
            if (green < -255 || green > 255) return false;
            if (blue < -255 || blue > 255) return false;

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - b.Width * 3;
                int nPixel;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        nPixel = p[2] + red;
                        nPixel = Math.Max(nPixel, 0);
                        p[2] = (byte)Math.Min(255, nPixel);

                        
                        nPixel = p[1] + green;
                        nPixel = Math.Max(nPixel, 0);
                        p[1] = (byte)Math.Min(255, nPixel);

                        nPixel = p[0] + blue;
                        nPixel = Math.Max(nPixel, 0);
                        p[0] = (byte)Math.Min(255, nPixel);
                        

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }

        public static bool Conv3x3(Bitmap b, ConvMatrix m)
        {
            // Avoid divide by zero errors
            if (0 == m.Factor) return false;

            Bitmap bSrc = (Bitmap)b.Clone();

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            int stride2 = stride * 2;
            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;

                int nOffset = stride - b.Width * 3;
                int nWidth = b.Width - 2;
                int nHeight = b.Height - 2;

                int nPixel;

                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        nPixel = ((((pSrc[2] * m.TopLeft) + (pSrc[5] * m.TopMid) + (pSrc[8] * m.TopRight) +
                            (pSrc[2 + stride] * m.MidLeft) + (pSrc[5 + stride] * m.Pixel) + (pSrc[8 + stride] * m.MidRight) +
                            (pSrc[2 + stride2] * m.BottomLeft) + (pSrc[5 + stride2] * m.BottomMid) + (pSrc[8 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[5 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[1] * m.TopLeft) + (pSrc[4] * m.TopMid) + (pSrc[7] * m.TopRight) +
                            (pSrc[1 + stride] * m.MidLeft) + (pSrc[4 + stride] * m.Pixel) + (pSrc[7 + stride] * m.MidRight) +
                            (pSrc[1 + stride2] * m.BottomLeft) + (pSrc[4 + stride2] * m.BottomMid) + (pSrc[7 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[4 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[0] * m.TopLeft) + (pSrc[3] * m.TopMid) + (pSrc[6] * m.TopRight) +
                            (pSrc[0 + stride] * m.MidLeft) + (pSrc[3 + stride] * m.Pixel) + (pSrc[6 + stride] * m.MidRight) +
                            (pSrc[0 + stride2] * m.BottomLeft) + (pSrc[3 + stride2] * m.BottomMid) + (pSrc[6 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[3 + stride] = (byte)nPixel;

                        p += 3;
                        pSrc += 3;
                    }
                    p += nOffset;
                    pSrc += nOffset;
                }
            }

            b.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);

            return true;
        }

        public static bool Smooth(Bitmap b, int nWeight)
        {
            ConvMatrix m = new ConvMatrix();
            m.SetAll(1);
            m.Pixel = nWeight;
            m.Factor = nWeight + 10;

            return BitmapFilter.Conv3x3(b, m);
        }

        public static bool EdgeDetectDifference(Bitmap b, byte nThreshold)
        {
            // This one works by working out the greatest difference between a pixel and it's eight neighbours.
            // The threshold allows softer edges to be forced down to black, use 0 to negate it's effect.
            Bitmap b2 = (Bitmap)b.Clone();

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmData2 = b2.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr Scan02 = bmData2.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* p2 = (byte*)(void*)Scan02;

                int nOffset = stride - b.Width * 3;
                int nWidth = b.Width * 3;

                int nPixel = 0, nPixelMax = 0;

                p += stride;
                p2 += stride;

                for (int y = 1; y < b.Height - 1; ++y)
                {
                    p += 3;
                    p2 += 3;

                    for (int x = 3; x < nWidth - 3; ++x)
                    {
                        nPixelMax = Math.Abs((p2 - stride + 3)[0] - (p2 + stride - 3)[0]);
                        nPixel = Math.Abs((p2 + stride + 3)[0] - (p2 - stride - 3)[0]);
                        if (nPixel > nPixelMax) nPixelMax = nPixel;

                        nPixel = Math.Abs((p2 - stride)[0] - (p2 + stride)[0]);
                        if (nPixel > nPixelMax) nPixelMax = nPixel;

                        nPixel = Math.Abs((p2 + 3)[0] - (p2 - 3)[0]);
                        if (nPixel > nPixelMax) nPixelMax = nPixel;

                        if (nPixelMax < nThreshold) nPixelMax = 0;

                        p[0] = (byte)nPixelMax;

                        ++p;
                        ++p2;
                    }

                    p += 3 + nOffset;
                    p2 += 3 + nOffset;
                }
            }

            b.UnlockBits(bmData);
            b2.UnlockBits(bmData2);

            return true;

        }


    }
}
