using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private bool IsImageExtension(string extension)// 파일 확장자 검사
        {
            return
                extension.Equals(".png") ||
                extension.Equals(".PNG") ||
                extension.Equals(".jpg") ||
                extension.Equals(".jpeg") ||
                extension.Equals(".jpe") ||
                extension.Equals(".jp2") ||
                extension.Equals(".webp") ||
                extension.Equals(".bmp") ||
                extension.Equals(".dib");
        }

        private void openFolderButton_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                dirPath = folderBrowserDialog.SelectedPath;
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            var bitmap = drawBox.GetBitMap();

            ImageCompareSystem.SetOriginalImage(bitmap);

            try
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(dirPath);

                foreach (var item in di.GetFiles())
                {
                    var fileName = item.Name;

                    if (String.IsNullOrEmpty(fileName))
                        continue;

                    var extension = System.IO.Path.GetExtension(fileName);

                    if (IsImageExtension(extension))
                    {
                        fileQueue.Enqueue($"{dirPath}/{fileName}");
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "에러", MessageBoxButtons.OK);
                return;
            }


            while (true)
            {
                if(!fileQueue.TryDequeue(out var path))
                    break;

                var dstBitmap = (Bitmap)Image.FromFile(path);
                
                var simul = ImageCompareSystem.CompareWithBitmap(dstBitmap);

                if(simul > (float)MinSimilarity.Value)
                {
                    try
                    {
                        resultImageList.Images.Add(path, dstBitmap);
                        resultImageView.Items.Add(path, resultImageList.Images.Count - 1);
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


