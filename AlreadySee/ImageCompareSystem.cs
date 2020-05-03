using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;
using OpenCvSharp;

namespace AlreadySee
{
    /// <summary>
    /// 이미지 검색기의 핵심이 되는 클래스
    /// </summary>
    static class ImageCompareSystem
    {
        static Mat originalImage = null;
        static Bitmap originalBitmap = null;

        static int originalImageWidth = -1;
        static int originalImageHeight = -1;
        public static float colorSimilarity = 0.0f;

        public static bool SetOriginalImage(Bitmap bitmap)
        {
            if (bitmap == null)
                return false;

            try
            {
                if (originalBitmap != null)
                    originalBitmap.Dispose();

                originalBitmap = new Bitmap(bitmap);
                originalImage = OpenCvSharp.Extensions.BitmapConverter.ToMat(originalBitmap);

                originalImageWidth = originalImage.Width;
                originalImageHeight = originalImage.Height;

                Cv2.CvtColor(originalImage, originalImage, ColorConversionCodes.BGR2HSV);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 이미지의 비율을 계산하고 적절한 크기를 리턴
        /// </summary>
        /// <param name="imageWith">계산할 이미지의 가로 크기</param>
        /// <param name="imageHeight">계산할 이미지의 세로 크기</param>
        /// <param name="targetWidth">원하는 적절한 가로 크기</param>
        /// <param name="targetHeight">원하는 적절한 세로 크기</param>
        /// <param name="multiple">계산 후 비율에 곱할 값</param>
        /// <param name="imageWidthRatio">계산이 끝난 가로 비율</param>
        /// <param name="imageHeightRatio">계산이 끝난 세로 비율</param>
        public static void ImageRatioCalculation(int imageWith, int imageHeight, int targetWidth, int targetHeight, out float multiple, out float imageWidthRatio, out float imageHeightRatio)
        {
            if (imageWith >= imageHeight)
            {// 이미지의 가로가 세로보다 길거나 같을 때에는 가로 픽셀을 1로 잡고 계산
                imageWidthRatio = 1.0f;
                imageHeightRatio = (float)imageHeight / (float)imageWith;// 1 : 0.xxx

                multiple = targetWidth;
            }
            else
            {// 이미지의 세로가 가로보다 길 때에는 세로 픽셀을 1로 잡고 계산
                imageWidthRatio = (float)imageWith / (float)imageHeight;// 0.xxx : 1
                imageHeightRatio = 1.0f;

                multiple = targetHeight;
            }
        }

        /// <summary>
        /// HSV를 3차원 공간 좌표로 변환후 거리 리턴
        /// </summary>
        private static float CompareHSV(float h1, float s1, float v1, float h2, float s2, float v2)
        {
            Vector3 pos1 = HSVtoVector3(h1, s1, v1);
            Vector3 pos2 = HSVtoVector3(h2, s2, v2);

            return Vector3.Distance(pos1, pos2);
        }

        /// <summary>
        /// HSV를 3차원 공간 좌표로 변환
        /// </summary>
        private static Vector3 HSVtoVector3(float h, float s, float v)
        {
            Vector3 retval = new Vector3();

            retval.X = s * (float)Math.Cos(Math.PI * 2.0f * h / 255.0f) * v / 255.0f;
            retval.Y = s * (float)Math.Sin(Math.PI * 2.0f * h / 255.0f) * v / 255.0f;
            retval.Z = v;

            return retval;
        }

        /// <summary>
        /// 두 이미지를 비교
        /// </summary>
        private unsafe static float CompareImage(Mat srcImage, Mat dstImage)
        {
            var asdfasdf = CompareHSV(0, 0, 0, 180, 255, 255);
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

            int xSpace = (int)((srcWidth - dstWidth) * 0.5f);// x 축 여백
            int ySpace = (int)((srcHeight - dstHeight) * 0.5f);// y 축 여백 srcImage의 중점을 기준으로 축소 시키기 때문에 * 0.5f

            for (int y = 0; y < dstHeight; y++)
            {
                for (int x = 0; x < dstWidth; x++)
                {
                    srcIdx = (y + ySpace) * srcWidth * srcImage.ElemSize() + (x + xSpace) * srcImage.ElemSize();
                    dstIdx = y * dstWidth * dstImage.ElemSize() + x * dstImage.ElemSize();

                    int srcA = originalBitmap.GetPixel(x + xSpace, y + ySpace).A;// HSV에서는 알파값을 표현할 수 없어서 Bitmap이미지로 부터 알파값을 가져옴

                    int srcH = srcData[srcIdx];
                    int srcS = srcData[srcIdx + 1];
                    int srcV = srcData[srcIdx + 2];

                    int dstH = dstData[dstIdx];
                    int dstS = dstData[dstIdx + 1];
                    int dstV = dstData[dstIdx + 2];

                    if (0 != srcA)
                    {// 알파값이 0이 아니면 브러쉬로 그린 부분임
                        maxCount++;

                        float value = CompareHSV(srcH, srcS, srcV, dstH, dstS, dstV);

                        if (value < colorSimilarity)// 이 값이 색의 유사도 최대값임
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

            try
            {
                using (var dstImage = OpenCvSharp.Extensions.BitmapConverter.ToMat(image))
                {
                    ImageRatioCalculation(dstImage.Cols, dstImage.Rows, originalImageWidth, originalImageHeight, out var multiple, out var dstImageWidthRatio, out var dstImageHeightRatio);

                    int dstWidth = (int)(dstImageWidthRatio * multiple);
                    int dstHeight = (int)(dstImageHeightRatio * multiple);

                    Cv2.Resize(dstImage, dstImage, new OpenCvSharp.Size(dstWidth, dstHeight));
                    Cv2.CvtColor(dstImage, dstImage, OpenCvSharp.ColorConversionCodes.BGR2HSV);

                    return CompareImage(originalImage, dstImage);
                }
            }
            catch
            {
                return 0.0f;
            }
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
