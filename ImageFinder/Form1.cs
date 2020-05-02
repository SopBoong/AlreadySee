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
            try
            {
                var bitmap = drawBox.GetBitMap();

                if (!ImageCompareSystem.SetOriginalImage(bitmap))
                {
                    MessageBox.Show("알 수 없는 오류 입니다");
                    return;
                }

                if (!Directory.Exists(dirPath))
                {
                    MessageBox.Show("이미지를 찾을 폴더를 선택해 주세요");
                    return;
                }

                resultImageList.Images.Clear();
                resultImageView.Items.Clear();

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

                    progressBar.Minimum = 0;
                    progressBar.Maximum = fileQueue.Count;
                    progressBar.Value = 0;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "에러", MessageBoxButtons.OK);
                return;
            }

            ExcuteCompare();
        }

        private Bitmap TransparentProcessing(Bitmap dstBitmap)
        {// 검색이 끝난 이미지를 표시하기 위해 알맞은 비율로 조절하고 빈자리는 투명화 시킴 이 작업이 없으면 이미지가 늘어남
            var targetSize = resultImageList.ImageSize;

            ImageCompareSystem.ImageRatioCalculation(dstBitmap.Width, dstBitmap.Height, targetSize.Width, targetSize.Height, out var multiple, out var widthRatio, out var heightRatio);

            var size = new Size((int)(widthRatio * multiple), (int)(heightRatio * multiple));
            var resizeBitmap = new Bitmap(dstBitmap, size);
            var transparentBitmap = new Bitmap(targetSize.Width, targetSize.Height);
            transparentBitmap.MakeTransparent();

            using (Graphics g = Graphics.FromImage(transparentBitmap))
            {
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                g.DrawImage(resizeBitmap, (transparentBitmap.Width - size.Width) * 0.5f, (transparentBitmap.Height - size.Height) * 0.5f, size.Width, size.Height);
            }

            return transparentBitmap;
        }

        private async void ExcuteCompare()
        {
            while (true)
            {
                if (!fileQueue.TryDequeue(out var fileName))
                    break;

                progressBar.Value++;

                var path = $@"{dirPath}\{fileName}";

                try
                {
                    using (var dstBitmap = (Bitmap)Image.FromFile(path))
                    {
                        var simul = await ImageCompareSystem.CompareWithBitmapAsync(dstBitmap);

                        if (simul > (float)MinSimilarity.Value)
                        {
                            var transparentBitmap = TransparentProcessing(dstBitmap);
                            dstBitmap.Dispose();
                            resultImageList.Images.Add(fileName, transparentBitmap);
                            resultImageView.Items.Add(fileName, resultImageList.Images.Count - 1);
                        }
                    }
                }
                catch (OutOfMemoryException)
                {// 갤에서 받은 특이한 이미지 들이 여기서 걸러짐 ex) 솦갤 전통, 깨진 파일, 잘못된 확장자 등
                    continue;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OpenImageFile(string fileName)
        {
            try
            {
                System.Diagnostics.Process.Start($@"{dirPath}\{fileName}");
            }
            catch
            {
                MessageBox.Show("존재하지 않는 폴더 입니다");
            }
        }

        private void OpenFolderAndSelectImageFile(string fileName)
        {
            try
            {
                System.Diagnostics.Process.Start("explorer.exe", string.Format("/select, \"{0}\\{1}\"", dirPath, fileName));
            }
            catch
            {
                MessageBox.Show("존재하지 않는 폴더 입니다");
            }
        }

        private void LoadImageAndSaveAs(string fileName)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "다른 이름으로 저장";
                dlg.DefaultExt = "jpg";
                dlg.Filter = "JPEG (*.jpg)|*.jpg|Bitmap (*.bmp)|*.bmp|PNG (*.png)|*.png";
                dlg.FilterIndex = 0;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Bitmap image = new Bitmap($@"{dirPath}\{fileName}"))
                    {
                        image.Save(dlg.FileName);
                    }
                }
            }
            catch
            {
                MessageBox.Show("존재하지 않는 폴더 입니다");
            }
        }

        private void resultImageView_DoubleClick(object sender, EventArgs e)
        {
            ListView listView = sender as ListView;
            var chosenImage = listView.FocusedItem;

            OpenImageFile(chosenImage.Text);
        }

        private void resultImageView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu menu = new ContextMenu();
                MenuItem item1 = new MenuItem();
                MenuItem item2 = new MenuItem();
                MenuItem item3 = new MenuItem();

                menu.MenuItems.Add(item1);
                menu.MenuItems.Add(item2);
                menu.MenuItems.Add(item3);
                
                item1.Text = "이미지 파일 열기";
                item2.Text = "파일 위치 열기";
                item3.Text = "다른 이름으로 저장";

                item1.Click += (s, ea) => 
                {
                    ListView listView = sender as ListView;
                    var chosenImage = listView.FocusedItem;

                    OpenImageFile(chosenImage.Text);
                };

                item2.Click += (s, ea) =>
                {
                    ListView listView = sender as ListView;
                    var chosenImage = listView.FocusedItem;

                    OpenFolderAndSelectImageFile(chosenImage.Text);
                };

                item3.Click += (s, es) =>
                {
                    ListView listView = sender as ListView;
                    var chosenImage = listView.FocusedItem;

                    LoadImageAndSaveAs(chosenImage.Text);
                };

                menu.Show(resultImageView, new Point(e.X, e.Y));
            }
        }
    }
}