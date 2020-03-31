using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


using System.Collections.Concurrent;

namespace ImageFinder
{

    public partial class MainForm : Form
    {
        DrawBox drawBox = null;
        static string dirPath = "";

        ConcurrentQueue<string> fileQueue = new ConcurrentQueue<string>();

        public MainForm()
        {
            InitializeComponent();

            drawBox = new DrawBox(paint, pencilButton, eraserButton, clearButton);

            drawBox.SetNowColorViewPanel(nowColor);

            drawBox.AddPallete(blackColor);
            drawBox.AddPallete(grayColor);
            drawBox.AddPallete(brownColor);
            drawBox.AddPallete(redColor);
            drawBox.AddPallete(orangeColor);
            drawBox.AddPallete(yellowColor);
            drawBox.AddPallete(greenColor);
            drawBox.AddPallete(deepSkyBlueColor);
            drawBox.AddPallete(blueColor);
            drawBox.AddPallete(purpleColor);

            drawBox.AddPallete(whiteColor);
            drawBox.AddPallete(brightGrayColor);
            drawBox.AddPallete(brightBrownColor);
            drawBox.AddPallete(pinkColor);
            drawBox.AddPallete(brightOrangeColor);
            drawBox.AddPallete(brightYellowColor);
            drawBox.AddPallete(brightGreenColor);
            drawBox.AddPallete(brightDeepBlueSkyColor);
            drawBox.AddPallete(brightBlueColor);
            drawBox.AddPallete(brightPurpleColor);
        }

        private bool IsImageExtension(string extension)//opencv에서 지원하는 이미지 파일 확장자 검사
        {
            return
                extension.Equals(".png")    ||
                extension.Equals(".PNG")    ||
                extension.Equals(".jpg")    ||
                extension.Equals(".jpeg")   ||
                extension.Equals(".jpe")    ||
                extension.Equals(".jp2")    ||
                extension.Equals(".webp")   ||
                extension.Equals(".bmp")    ||
                extension.Equals(".dib");
        }

        private void openFolderButton_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                dirPath = folderBrowserDialog.SelectedPath;
                if(!Directory.Exists(dirPath))
                {
                    dirPath = "";
                    MessageBox.Show("존재하지 않는 폴더 입니다");
                }
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            var bitmap = drawBox.GetBitMap();

            ImageCompareSystem.SetOriginalImage(bitmap);

            if (!Directory.Exists(dirPath))
            {
                MessageBox.Show("이미지를 찾을 폴더를 선택해 주세요");
                return;
            }

            try
            {
                DirectoryInfo di = new DirectoryInfo(dirPath);

                var files = di.GetFiles();

                foreach (var file in files)
                {
                    var fileName = file.Name;

                    if (String.IsNullOrEmpty(fileName))
                        continue;

                    var extension = System.IO.Path.GetExtension(fileName);

                    if (IsImageExtension(extension))
                    {
                        fileQueue.Enqueue(fileName);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "에러", MessageBoxButtons.OK);
                return;
            }
            ExcuteCompare();
        }

        private async void ExcuteCompare()
        {
            while (true)
            {
                if (!fileQueue.TryDequeue(out var fileName))
                    break;

                var path = $"{dirPath}\\{fileName}";

                var dstBitmap = (Bitmap)Image.FromFile(path);

                var simul = await ImageCompareSystem.CompareWithBitmapAsync(dstBitmap);

                if (simul > (float)MinSimilarity.Value)
                {
                    try
                    {
                        resultImageList.Images.Add(fileName, dstBitmap);
                        resultImageView.Items.Add(fileName, resultImageList.Images.Count - 1);
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(error.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dstBitmap.Dispose();
                    }
                }
                else
                {
                    dstBitmap.Dispose();
                }
            }
        }
    }
}


