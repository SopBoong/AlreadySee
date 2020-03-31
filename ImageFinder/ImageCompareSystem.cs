using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

using System.Drawing;
using OpenCvSharp;

namespace ImageFinder
{
    static class ImageCompareSystem
    {
        static Mat originalImage = null;

        static int originalImageWidth = -1;
        static int originalImageHeight = -1;

        public static void SetOriginalImage(Bitmap bitmap)
        {
            if (bitmap == null)
                return;

            originalImage = OpenCvSharp.Extensions.BitmapConverter.ToMat(bitmap);
            originalImageWidth = originalImage.Width;
            originalImageHeight = originalImage.Height;

            Cv2.CvtColor(originalImage, originalImage, OpenCvSharp.ColorConversionCodes.BGR2HSV);

           // MatTransparent(bitmap, ref originalImage);
        }

        //private unsafe static void MatTransparent(Bitmap bitmap, ref Mat mat)
        //{
        //    for (int y = 0; y < bitmap.Height; y++)
        //    {
        //        for (int x = 0; x < bitmap.Width; x++)
        //        {
        //            var color = bitmap.GetPixel(x, y);

        //            if (color.A != 0)
        //                continue;

        //            int matIdx = y * (int)mat.Step() + x * mat.ElemSize();

        //            var data = mat.DataPointer;


        //        }
        //    }
        //}

        private static void ImageRatioCalculation(int imageWith, int imageHeight, out float multiple, out float imageWidthRatio, out float imageHeightRatio)
        {
            if (imageWith >= imageHeight)
            {// 이미지의 가로가 세로보다 길거나 같을 때에는 가로 픽셀을 1로 잡고 계산
                imageWidthRatio = 1.0f;
                imageHeightRatio = (float)imageHeight / (float)imageWith;// 1 : 0.xxx

                multiple = originalImageWidth;
            }
            else
            {// 이미지의 세로가 가로보다 길 때에는 세로 픽셀을 1로 잡고 계산
                imageWidthRatio = (float)imageWith / (float)imageHeight;// 0.xxx : 1
                imageHeightRatio = 1.0f;

                multiple = originalImageHeight;
            }
        }

        private static float CompareHSV(float h1, float s1, float v1, float h2, float s2, float v2)
        {
            Vector3 pos1 = HSVtoVector3(h1, s1, v1);
            Vector3 pos2 = HSVtoVector3(h2, s2, v2);

            return Vector3.Distance(pos1, pos2);
        }

        private static Vector3 HSVtoVector3(float h, float s, float v)
        {
            Vector3 retval = new Vector3();

            retval.X = s * (float)Math.Cos(Math.PI * 2.0f * h / 255.0f) * v / 255.0f;
            retval.Y = s * (float)Math.Sin(Math.PI * 2.0f * h / 255.0f) * v / 255.0f;
            retval.Z = v;

            return retval;
        }

        private unsafe static float CompareImage(Mat srcImage, Mat dstImage)
        {
            var srcData = srcImage.DataPointer;
            var dstData = dstImage.DataPointer;

            int srcWidth = srcImage.Cols;
            int srcHeight = srcImage.Rows;

            int dstWidth = dstImage.Cols;
            int dstHeight = dstImage.Rows;

            int maxCount = 0;
            int count = 0;

            int dstIdx = 0;
            int srcIdx = 0;

            int xSpace = (int)((srcWidth - dstWidth) * 0.5f);
            int ySpace = (int)((srcHeight - dstHeight) * 0.5f);

            for (int y = 0; y < dstHeight; y++)
            {
                for (int x = 0; x < dstWidth; x++)
                {
                    srcIdx = (y + ySpace) * (int)srcImage.Step() + (x + xSpace) * srcImage.ElemSize();
                    dstIdx = y * (int)dstImage.Step() + x * dstImage.ElemSize();

                    int srcH = srcData[srcIdx];
                    int srcS = srcData[srcIdx + 1];
                    int srcV = srcData[srcIdx + 2];

                    int dstH = dstData[dstIdx];
                    int dstS = dstData[dstIdx + 1];
                    int dstV = dstData[dstIdx + 2];

                    if (srcH != 0 && srcS != 0 && srcV != 0)
                    {
                        maxCount++;

                        if (CompareHSV(srcH, srcS, srcV, dstH, dstS, dstV) < 150.1f)
                        {
                            count++;
                        }
                    }
                }
            }

            return (count == 0 || maxCount == 0) ? 0.0f : (float)count / maxCount;// 0 으로 나누기 방지
        }

        public static float CompareWithBitmap(Bitmap image)
        {
            if (originalImage == null || originalImageHeight < 0 || originalImageWidth < 0)
                return -1.0f;

            var dstImage = OpenCvSharp.Extensions.BitmapConverter.ToMat(image);

            ImageRatioCalculation(dstImage.Cols, dstImage.Rows, out var multiple, out var dstImageWidthRatio, out var dstImageHeightRatio);

            int dstWidth = (int)(dstImageWidthRatio * multiple);
            int dstHeight = (int)(dstImageHeightRatio * multiple);

            try
            {
                Cv2.Resize(dstImage, dstImage, new OpenCvSharp.Size(dstWidth, dstHeight));
                Cv2.CvtColor(dstImage, dstImage, OpenCvSharp.ColorConversionCodes.BGR2HSV);
            }
            catch
            {
                return 0.0f;
            }

            return CompareImage(originalImage, dstImage);
        }

        public static Task<float> CompareWithBitmapAsync(Bitmap image)
        {
            return Task.Run(() =>
            {
                return CompareWithBitmap(image);
            });
        }
    }
}
