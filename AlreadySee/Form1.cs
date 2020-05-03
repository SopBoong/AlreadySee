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
using System.Reflection;

namespace AlreadySee
{
    public partial class MainForm : Form
    {
        DrawBox drawBox = null;
        static string dirPath = "";
        bool isSearching = false;
        //bool autoScrolling = true;

        ConcurrentQueue<string> fileQueue = new ConcurrentQueue<string>();// 혹시 몰라서 ConcurrentQueue 사용

        public MainForm()
        {
            InitializeComponent();
            Closed += MainForm_Closed;

            var prop = resultImageView.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            prop.SetValue(resultImageView, true, null);// 상속하기 귀찮아서 강제로 protected값 변경

            drawBox = new DrawBox(paint, pencilButton, eraserButton, clearButton, colorDialog);

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

            drawBox.AddCustomPallete(customPallete1);
            drawBox.AddCustomPallete(customPallete2);
            drawBox.AddCustomPallete(customPallete3);
            drawBox.AddCustomPallete(customPallete4);
            drawBox.AddCustomPallete(customPallete5);
            drawBox.AddCustomPallete(customPallete6);
            drawBox.AddCustomPallete(customPallete7);
            drawBox.AddCustomPallete(customPallete8);
            drawBox.AddCustomPallete(customPallete9);
            drawBox.AddCustomPallete(customPallete10);

            RegistryManager.LoadRegistry();
            LoadUI();
        }

        private void MainForm_Closed(object sender, EventArgs e)
        {
            SaveUI();
        }

        private void LoadUI()
        {
            try
            {
                if (Directory.Exists(RegistryManager.FilePath))
                {
                    SetDirPath(RegistryManager.FilePath);
                    folderBrowserDialog.SelectedPath = RegistryManager.FilePath;
                }
                folderBrowserDialog.SelectedPath = dirPath;
                ImageSimilarity.Value = (decimal)RegistryManager.ImageSimilarity;
                ColorSimilarity.Value = (decimal)RegistryManager.ColorSimilarity;
                drawBox.SetNowColor(RegistryManager.NowColor);
                var colorList = RegistryManager.CustomPalleteColorList;

                customPallete1.BackColor = colorList[0];
                customPallete2.BackColor = colorList[1];
                customPallete3.BackColor = colorList[2];
                customPallete4.BackColor = colorList[3];
                customPallete5.BackColor = colorList[4];
                customPallete6.BackColor = colorList[5];
                customPallete7.BackColor = colorList[6];
                customPallete8.BackColor = colorList[7];
                customPallete9.BackColor = colorList[8];
                customPallete10.BackColor = colorList[9];

                drawBox.PenWidth = RegistryManager.PenWidth;
            }
            catch
            {

            }
        }

        private void SaveUI()
        {
            RegistryManager.SaveRegistry
            (
                new List<Color>
                {
                    customPallete1.BackColor,
                    customPallete2.BackColor,
                    customPallete3.BackColor,
                    customPallete4.BackColor,
                    customPallete5.BackColor,
                    customPallete6.BackColor,
                    customPallete7.BackColor,
                    customPallete8.BackColor,
                    customPallete9.BackColor,
                    customPallete10.BackColor
                },
                drawBox.GetNowColor(),
                (float)ImageSimilarity.Value,
                (float)ColorSimilarity.Value,
                dirPath,
                drawBox.PenWidth
            );
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

        private void SetDirPath(string path)
        {
            dirPath = path;
            filePathBox.Text = dirPath;
        }

        private void openFolderButton_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                if(!Directory.Exists(folderBrowserDialog.SelectedPath))
                {
                    dirPath = "";
                    MessageBox.Show("존재하지 않는 폴더 입니다");
                }
                SetDirPath(folderBrowserDialog.SelectedPath);
            }
        }

        private void UIstart()
        {
            drawBox.Enabled = false;
            openFolderButton.Enabled = false;
            ImageSimilarity.Enabled = false;
            ColorSimilarity.Enabled = false;
            label1.Enabled = false;
            label2.Enabled = false;
            isSearching = true;
            searchButton.Text = "검색 중지";
            filePathBox.Enabled = false;
        }

        private void UIend()
        {
            drawBox.Enabled = true;
            openFolderButton.Enabled = true;
            ImageSimilarity.Enabled = true;
            ColorSimilarity.Enabled = true;
            label1.Enabled = true;
            label2.Enabled = true;
            isSearching = false;
            searchButton.Text = "검색 시작";
            progressBar.Value = 0;
            progressBar.Minimum = 0;
            progressBar.Maximum = 0;
            filePathBox.Enabled = true;
        }

        private void ClearResult()
        {
            resultImageList.Images.Clear();
            resultImageView.Items.Clear();
        }

        private void SearchStart()
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

            ClearResult();

            DirectoryInfo di = new DirectoryInfo(dirPath);

            var files = di.GetFiles();

            while (fileQueue.TryDequeue(out var v)) { }

            foreach (var file in files)
            {
                var fileName = file.Name;

                if (String.IsNullOrEmpty(fileName))
                    continue;

                var extension = Path.GetExtension(fileName);

                if (IsImageExtension(extension))
                {
                    fileQueue.Enqueue(fileName);
                }

                progressBar.Minimum = 0;
                progressBar.Maximum = fileQueue.Count;
                progressBar.Value = 0;
            }
            UIstart();
            ExcuteCompare();
        }

        private void SearchEnd()
        {
            UIend();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (isSearching)
                {
                    SearchEnd();
                }
                else
                {
                    SearchStart();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "에러", MessageBoxButtons.OK);
                return;
            }
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
            ImageCompareSystem.colorSimilarity = (float)ColorSimilarity.Value;
            float minsim = (float)ImageSimilarity.Value;
            while (true)
            {
                if (!fileQueue.TryDequeue(out var fileName) || isSearching == false)
                    break;

                progressBar.Value++;

                var path = $@"{dirPath}\{fileName}";

                try
                {
                    using (var dstBitmap = (Bitmap)Image.FromFile(path))
                    {
                        var simul = await ImageCompareSystem.CompareWithBitmapAsync(dstBitmap);

                        if (simul > minsim)
                        {
                            var transparentBitmap = TransparentProcessing(dstBitmap);
                            dstBitmap.Dispose();
                            resultImageList.Images.Add(fileName, transparentBitmap);
                            resultImageView.Items.Add(fileName, resultImageList.Images.Count - 1);
                            if (resultImageView.SelectedItems.Count == 0 && resultImageView.FocusedItem != null)
                            {
                                resultImageView.FocusedItem.Focused = false;// focus 취소가 안되서 강제로 취소시킴
                            }
                            //if (autoScrolling)
                            //    resultImageView.Items[resultImageView.Items.Count - 1].EnsureVisible();
                        }
                    }
                }
                catch (OutOfMemoryException)
                {// 갤에서 받은 특이한 이미지들이 여기서 걸러짐 ex) 솦갤 전통, 깨진 파일, 잘못된 확장자 등
                    continue;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            UIend();
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
                saveFileDialog.Title = "다른 이름으로 저장";
                saveFileDialog.DefaultExt = "jpg";
                saveFileDialog.Filter = "JPEG (*.jpg)|*.jpg|Bitmap (*.bmp)|*.bmp|PNG (*.png)|*.png";
                saveFileDialog.FilterIndex = 0;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (Bitmap image = new Bitmap($@"{dirPath}\{fileName}"))
                    {
                        image.Save(saveFileDialog.FileName);
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
            if (chosenImage != null)
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