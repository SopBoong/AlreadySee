namespace AlreadySee
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.resultImageList = new System.Windows.Forms.ImageList(this.components);
            this.resultImageView = new System.Windows.Forms.ListView();
            this.openFolderButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.searchButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.clearButton = new System.Windows.Forms.Button();
            this.eraserButton = new System.Windows.Forms.Button();
            this.pencilButton = new System.Windows.Forms.Button();
            this.paint = new System.Windows.Forms.PictureBox();
            this.redColor = new System.Windows.Forms.Panel();
            this.orangeColor = new System.Windows.Forms.Panel();
            this.yellowColor = new System.Windows.Forms.Panel();
            this.greenColor = new System.Windows.Forms.Panel();
            this.deepSkyBlueColor = new System.Windows.Forms.Panel();
            this.blueColor = new System.Windows.Forms.Panel();
            this.pinkColor = new System.Windows.Forms.Panel();
            this.purpleColor = new System.Windows.Forms.Panel();
            this.brightGreenColor = new System.Windows.Forms.Panel();
            this.grayColor = new System.Windows.Forms.Panel();
            this.brightBlueColor = new System.Windows.Forms.Panel();
            this.whiteColor = new System.Windows.Forms.Panel();
            this.brightYellowColor = new System.Windows.Forms.Panel();
            this.brightOrangeColor = new System.Windows.Forms.Panel();
            this.brightPurpleColor = new System.Windows.Forms.Panel();
            this.brightBrownColor = new System.Windows.Forms.Panel();
            this.nowColor = new System.Windows.Forms.Panel();
            this.ImageSimilarity = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.brownColor = new System.Windows.Forms.Panel();
            this.brightGrayColor = new System.Windows.Forms.Panel();
            this.blackColor = new System.Windows.Forms.Panel();
            this.brightDeepBlueSkyColor = new System.Windows.Forms.Panel();
            this.ColorSimilarity = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.filePathBox = new System.Windows.Forms.TextBox();
            this.customPallete1 = new System.Windows.Forms.Panel();
            this.customPallete2 = new System.Windows.Forms.Panel();
            this.customPallete3 = new System.Windows.Forms.Panel();
            this.customPallete4 = new System.Windows.Forms.Panel();
            this.customPallete5 = new System.Windows.Forms.Panel();
            this.customPallete6 = new System.Windows.Forms.Panel();
            this.customPallete7 = new System.Windows.Forms.Panel();
            this.customPallete8 = new System.Windows.Forms.Panel();
            this.customPallete9 = new System.Windows.Forms.Panel();
            this.customPallete10 = new System.Windows.Forms.Panel();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.searchSubFolder = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.paint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageSimilarity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorSimilarity)).BeginInit();
            this.SuspendLayout();
            // 
            // resultImageList
            // 
            this.resultImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.resultImageList.ImageSize = new System.Drawing.Size(100, 100);
            this.resultImageList.TransparentColor = System.Drawing.Color.Gainsboro;
            // 
            // resultImageView
            // 
            this.resultImageView.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.resultImageView.HideSelection = false;
            this.resultImageView.LargeImageList = this.resultImageList;
            this.resultImageView.Location = new System.Drawing.Point(6, 6);
            this.resultImageView.MultiSelect = false;
            this.resultImageView.Name = "resultImageView";
            this.resultImageView.Size = new System.Drawing.Size(600, 594);
            this.resultImageView.TabIndex = 0;
            this.resultImageView.UseCompatibleStateImageBehavior = false;
            this.resultImageView.DoubleClick += new System.EventHandler(this.resultImageView_DoubleClick);
            this.resultImageView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.resultImageView_MouseClick);
            // 
            // openFolderButton
            // 
            this.openFolderButton.Location = new System.Drawing.Point(617, 35);
            this.openFolderButton.Name = "openFolderButton";
            this.openFolderButton.Size = new System.Drawing.Size(81, 23);
            this.openFolderButton.TabIndex = 1;
            this.openFolderButton.Text = "폴더 선택";
            this.openFolderButton.UseVisualStyleBackColor = true;
            this.openFolderButton.Click += new System.EventHandler(this.openFolderButton_Click);
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(617, 6);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(81, 23);
            this.searchButton.TabIndex = 3;
            this.searchButton.Text = "검색 시작";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(704, 6);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(257, 23);
            this.progressBar.TabIndex = 5;
            // 
            // clearButton
            // 
            this.clearButton.Image = global::AlreadySee.Properties.Resources.clearReRe;
            this.clearButton.Location = new System.Drawing.Point(619, 123);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(52, 23);
            this.clearButton.TabIndex = 8;
            this.clearButton.UseVisualStyleBackColor = true;
            // 
            // eraserButton
            // 
            this.eraserButton.Image = global::AlreadySee.Properties.Resources.eraser20;
            this.eraserButton.Location = new System.Drawing.Point(648, 152);
            this.eraserButton.Name = "eraserButton";
            this.eraserButton.Size = new System.Drawing.Size(23, 23);
            this.eraserButton.TabIndex = 7;
            this.eraserButton.UseVisualStyleBackColor = true;
            // 
            // pencilButton
            // 
            this.pencilButton.Enabled = false;
            this.pencilButton.Image = global::AlreadySee.Properties.Resources.pencilRe;
            this.pencilButton.Location = new System.Drawing.Point(619, 152);
            this.pencilButton.Name = "pencilButton";
            this.pencilButton.Size = new System.Drawing.Size(23, 23);
            this.pencilButton.TabIndex = 6;
            this.pencilButton.UseVisualStyleBackColor = true;
            // 
            // paint
            // 
            this.paint.BackColor = System.Drawing.SystemColors.ControlDark;
            this.paint.Location = new System.Drawing.Point(611, 180);
            this.paint.Name = "paint";
            this.paint.Size = new System.Drawing.Size(360, 420);
            this.paint.TabIndex = 2;
            this.paint.TabStop = false;
            // 
            // redColor
            // 
            this.redColor.BackColor = System.Drawing.Color.Red;
            this.redColor.Location = new System.Drawing.Point(764, 94);
            this.redColor.Name = "redColor";
            this.redColor.Size = new System.Drawing.Size(23, 23);
            this.redColor.TabIndex = 9;
            // 
            // orangeColor
            // 
            this.orangeColor.BackColor = System.Drawing.Color.Orange;
            this.orangeColor.Location = new System.Drawing.Point(793, 94);
            this.orangeColor.Name = "orangeColor";
            this.orangeColor.Size = new System.Drawing.Size(23, 23);
            this.orangeColor.TabIndex = 10;
            // 
            // yellowColor
            // 
            this.yellowColor.BackColor = System.Drawing.Color.Yellow;
            this.yellowColor.Location = new System.Drawing.Point(822, 94);
            this.yellowColor.Name = "yellowColor";
            this.yellowColor.Size = new System.Drawing.Size(23, 23);
            this.yellowColor.TabIndex = 11;
            // 
            // greenColor
            // 
            this.greenColor.BackColor = System.Drawing.Color.LimeGreen;
            this.greenColor.Location = new System.Drawing.Point(851, 94);
            this.greenColor.Name = "greenColor";
            this.greenColor.Size = new System.Drawing.Size(23, 23);
            this.greenColor.TabIndex = 12;
            // 
            // deepSkyBlueColor
            // 
            this.deepSkyBlueColor.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.deepSkyBlueColor.Location = new System.Drawing.Point(880, 94);
            this.deepSkyBlueColor.Name = "deepSkyBlueColor";
            this.deepSkyBlueColor.Size = new System.Drawing.Size(23, 23);
            this.deepSkyBlueColor.TabIndex = 11;
            // 
            // blueColor
            // 
            this.blueColor.BackColor = System.Drawing.Color.Blue;
            this.blueColor.Location = new System.Drawing.Point(909, 94);
            this.blueColor.Name = "blueColor";
            this.blueColor.Size = new System.Drawing.Size(23, 23);
            this.blueColor.TabIndex = 11;
            // 
            // pinkColor
            // 
            this.pinkColor.BackColor = System.Drawing.Color.Pink;
            this.pinkColor.Location = new System.Drawing.Point(764, 123);
            this.pinkColor.Name = "pinkColor";
            this.pinkColor.Size = new System.Drawing.Size(23, 23);
            this.pinkColor.TabIndex = 11;
            // 
            // purpleColor
            // 
            this.purpleColor.BackColor = System.Drawing.Color.Purple;
            this.purpleColor.Location = new System.Drawing.Point(938, 94);
            this.purpleColor.Name = "purpleColor";
            this.purpleColor.Size = new System.Drawing.Size(23, 23);
            this.purpleColor.TabIndex = 11;
            // 
            // brightGreenColor
            // 
            this.brightGreenColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(230)))), ((int)(((byte)(29)))));
            this.brightGreenColor.Location = new System.Drawing.Point(851, 123);
            this.brightGreenColor.Name = "brightGreenColor";
            this.brightGreenColor.Size = new System.Drawing.Size(23, 23);
            this.brightGreenColor.TabIndex = 13;
            // 
            // grayColor
            // 
            this.grayColor.BackColor = System.Drawing.Color.Gray;
            this.grayColor.Location = new System.Drawing.Point(706, 94);
            this.grayColor.Name = "grayColor";
            this.grayColor.Size = new System.Drawing.Size(23, 23);
            this.grayColor.TabIndex = 12;
            // 
            // brightBlueColor
            // 
            this.brightBlueColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(146)))), ((int)(((byte)(190)))));
            this.brightBlueColor.Location = new System.Drawing.Point(909, 123);
            this.brightBlueColor.Name = "brightBlueColor";
            this.brightBlueColor.Size = new System.Drawing.Size(23, 23);
            this.brightBlueColor.TabIndex = 12;
            // 
            // whiteColor
            // 
            this.whiteColor.BackColor = System.Drawing.Color.White;
            this.whiteColor.Location = new System.Drawing.Point(677, 123);
            this.whiteColor.Name = "whiteColor";
            this.whiteColor.Size = new System.Drawing.Size(23, 23);
            this.whiteColor.TabIndex = 12;
            // 
            // brightYellowColor
            // 
            this.brightYellowColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(228)))), ((int)(((byte)(176)))));
            this.brightYellowColor.Location = new System.Drawing.Point(822, 123);
            this.brightYellowColor.Name = "brightYellowColor";
            this.brightYellowColor.Size = new System.Drawing.Size(23, 23);
            this.brightYellowColor.TabIndex = 12;
            // 
            // brightOrangeColor
            // 
            this.brightOrangeColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(201)))), ((int)(((byte)(14)))));
            this.brightOrangeColor.Location = new System.Drawing.Point(793, 123);
            this.brightOrangeColor.Name = "brightOrangeColor";
            this.brightOrangeColor.Size = new System.Drawing.Size(23, 23);
            this.brightOrangeColor.TabIndex = 12;
            // 
            // brightPurpleColor
            // 
            this.brightPurpleColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(191)))), ((int)(((byte)(231)))));
            this.brightPurpleColor.Location = new System.Drawing.Point(938, 123);
            this.brightPurpleColor.Name = "brightPurpleColor";
            this.brightPurpleColor.Size = new System.Drawing.Size(23, 23);
            this.brightPurpleColor.TabIndex = 12;
            // 
            // brightBrownColor
            // 
            this.brightBrownColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(122)))), ((int)(((byte)(87)))));
            this.brightBrownColor.Location = new System.Drawing.Point(735, 123);
            this.brightBrownColor.Name = "brightBrownColor";
            this.brightBrownColor.Size = new System.Drawing.Size(23, 23);
            this.brightBrownColor.TabIndex = 12;
            // 
            // nowColor
            // 
            this.nowColor.BackColor = System.Drawing.Color.Black;
            this.nowColor.Location = new System.Drawing.Point(619, 94);
            this.nowColor.Name = "nowColor";
            this.nowColor.Size = new System.Drawing.Size(52, 23);
            this.nowColor.TabIndex = 14;
            // 
            // ImageSimilarity
            // 
            this.ImageSimilarity.DecimalPlaces = 2;
            this.ImageSimilarity.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.ImageSimilarity.Location = new System.Drawing.Point(619, 64);
            this.ImageSimilarity.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ImageSimilarity.Name = "ImageSimilarity";
            this.ImageSimilarity.Size = new System.Drawing.Size(60, 21);
            this.ImageSimilarity.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(681, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "이미지 유사도 최솟값";
            // 
            // brownColor
            // 
            this.brownColor.BackColor = System.Drawing.Color.Brown;
            this.brownColor.Location = new System.Drawing.Point(735, 94);
            this.brownColor.Name = "brownColor";
            this.brownColor.Size = new System.Drawing.Size(23, 23);
            this.brownColor.TabIndex = 16;
            // 
            // brightGrayColor
            // 
            this.brightGrayColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.brightGrayColor.Location = new System.Drawing.Point(706, 123);
            this.brightGrayColor.Name = "brightGrayColor";
            this.brightGrayColor.Size = new System.Drawing.Size(23, 23);
            this.brightGrayColor.TabIndex = 11;
            // 
            // blackColor
            // 
            this.blackColor.BackColor = System.Drawing.Color.Black;
            this.blackColor.Location = new System.Drawing.Point(677, 94);
            this.blackColor.Name = "blackColor";
            this.blackColor.Size = new System.Drawing.Size(23, 23);
            this.blackColor.TabIndex = 17;
            // 
            // brightDeepBlueSkyColor
            // 
            this.brightDeepBlueSkyColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(217)))), ((int)(((byte)(234)))));
            this.brightDeepBlueSkyColor.Location = new System.Drawing.Point(880, 123);
            this.brightDeepBlueSkyColor.Name = "brightDeepBlueSkyColor";
            this.brightDeepBlueSkyColor.Size = new System.Drawing.Size(23, 23);
            this.brightDeepBlueSkyColor.TabIndex = 18;
            // 
            // ColorSimilarity
            // 
            this.ColorSimilarity.DecimalPlaces = 2;
            this.ColorSimilarity.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ColorSimilarity.Location = new System.Drawing.Point(808, 64);
            this.ColorSimilarity.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.ColorSimilarity.Name = "ColorSimilarity";
            this.ColorSimilarity.Size = new System.Drawing.Size(60, 21);
            this.ColorSimilarity.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(871, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "색 유사도 최댓값";
            // 
            // filePathBox
            // 
            this.filePathBox.Location = new System.Drawing.Point(705, 36);
            this.filePathBox.MaxLength = 0;
            this.filePathBox.Name = "filePathBox";
            this.filePathBox.ReadOnly = true;
            this.filePathBox.Size = new System.Drawing.Size(158, 21);
            this.filePathBox.TabIndex = 21;
            // 
            // customPallete1
            // 
            this.customPallete1.BackColor = System.Drawing.Color.White;
            this.customPallete1.Location = new System.Drawing.Point(677, 152);
            this.customPallete1.Name = "customPallete1";
            this.customPallete1.Size = new System.Drawing.Size(23, 23);
            this.customPallete1.TabIndex = 22;
            // 
            // customPallete2
            // 
            this.customPallete2.BackColor = System.Drawing.Color.White;
            this.customPallete2.Location = new System.Drawing.Point(706, 152);
            this.customPallete2.Name = "customPallete2";
            this.customPallete2.Size = new System.Drawing.Size(23, 23);
            this.customPallete2.TabIndex = 23;
            // 
            // customPallete3
            // 
            this.customPallete3.BackColor = System.Drawing.Color.White;
            this.customPallete3.Location = new System.Drawing.Point(735, 152);
            this.customPallete3.Name = "customPallete3";
            this.customPallete3.Size = new System.Drawing.Size(23, 23);
            this.customPallete3.TabIndex = 23;
            // 
            // customPallete4
            // 
            this.customPallete4.BackColor = System.Drawing.Color.White;
            this.customPallete4.Location = new System.Drawing.Point(764, 152);
            this.customPallete4.Name = "customPallete4";
            this.customPallete4.Size = new System.Drawing.Size(23, 23);
            this.customPallete4.TabIndex = 23;
            // 
            // customPallete5
            // 
            this.customPallete5.BackColor = System.Drawing.Color.White;
            this.customPallete5.Location = new System.Drawing.Point(793, 152);
            this.customPallete5.Name = "customPallete5";
            this.customPallete5.Size = new System.Drawing.Size(23, 23);
            this.customPallete5.TabIndex = 23;
            // 
            // customPallete6
            // 
            this.customPallete6.BackColor = System.Drawing.Color.White;
            this.customPallete6.Location = new System.Drawing.Point(822, 152);
            this.customPallete6.Name = "customPallete6";
            this.customPallete6.Size = new System.Drawing.Size(23, 23);
            this.customPallete6.TabIndex = 23;
            // 
            // customPallete7
            // 
            this.customPallete7.BackColor = System.Drawing.Color.White;
            this.customPallete7.Location = new System.Drawing.Point(851, 152);
            this.customPallete7.Name = "customPallete7";
            this.customPallete7.Size = new System.Drawing.Size(23, 23);
            this.customPallete7.TabIndex = 23;
            // 
            // customPallete8
            // 
            this.customPallete8.BackColor = System.Drawing.Color.White;
            this.customPallete8.Location = new System.Drawing.Point(880, 152);
            this.customPallete8.Name = "customPallete8";
            this.customPallete8.Size = new System.Drawing.Size(23, 23);
            this.customPallete8.TabIndex = 23;
            // 
            // customPallete9
            // 
            this.customPallete9.BackColor = System.Drawing.Color.White;
            this.customPallete9.Location = new System.Drawing.Point(909, 152);
            this.customPallete9.Name = "customPallete9";
            this.customPallete9.Size = new System.Drawing.Size(23, 23);
            this.customPallete9.TabIndex = 23;
            // 
            // customPallete10
            // 
            this.customPallete10.BackColor = System.Drawing.Color.White;
            this.customPallete10.Location = new System.Drawing.Point(938, 152);
            this.customPallete10.Name = "customPallete10";
            this.customPallete10.Size = new System.Drawing.Size(23, 23);
            this.customPallete10.TabIndex = 23;
            // 
            // searchSubFolder
            // 
            this.searchSubFolder.AutoSize = true;
            this.searchSubFolder.Location = new System.Drawing.Point(870, 40);
            this.searchSubFolder.Name = "searchSubFolder";
            this.searchSubFolder.Size = new System.Drawing.Size(100, 16);
            this.searchSubFolder.TabIndex = 24;
            this.searchSubFolder.Text = "하위폴더 검색";
            this.searchSubFolder.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(976, 605);
            this.Controls.Add(this.searchSubFolder);
            this.Controls.Add(this.customPallete10);
            this.Controls.Add(this.customPallete9);
            this.Controls.Add(this.customPallete8);
            this.Controls.Add(this.customPallete7);
            this.Controls.Add(this.customPallete6);
            this.Controls.Add(this.customPallete5);
            this.Controls.Add(this.customPallete4);
            this.Controls.Add(this.customPallete3);
            this.Controls.Add(this.customPallete2);
            this.Controls.Add(this.customPallete1);
            this.Controls.Add(this.filePathBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ColorSimilarity);
            this.Controls.Add(this.brightDeepBlueSkyColor);
            this.Controls.Add(this.blackColor);
            this.Controls.Add(this.brownColor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ImageSimilarity);
            this.Controls.Add(this.nowColor);
            this.Controls.Add(this.brightBrownColor);
            this.Controls.Add(this.brightPurpleColor);
            this.Controls.Add(this.brightOrangeColor);
            this.Controls.Add(this.brightYellowColor);
            this.Controls.Add(this.whiteColor);
            this.Controls.Add(this.brightBlueColor);
            this.Controls.Add(this.grayColor);
            this.Controls.Add(this.brightGreenColor);
            this.Controls.Add(this.purpleColor);
            this.Controls.Add(this.pinkColor);
            this.Controls.Add(this.blueColor);
            this.Controls.Add(this.deepSkyBlueColor);
            this.Controls.Add(this.brightGrayColor);
            this.Controls.Add(this.greenColor);
            this.Controls.Add(this.yellowColor);
            this.Controls.Add(this.orangeColor);
            this.Controls.Add(this.redColor);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.eraserButton);
            this.Controls.Add(this.pencilButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.paint);
            this.Controls.Add(this.openFolderButton);
            this.Controls.Add(this.resultImageView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "이미본거야 - 1.1.0";
            ((System.ComponentModel.ISupportInitialize)(this.paint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageSimilarity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorSimilarity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView resultImageView;
        private System.Windows.Forms.Button openFolderButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.PictureBox paint;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button pencilButton;
        private System.Windows.Forms.Button eraserButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Panel redColor;
        private System.Windows.Forms.Panel orangeColor;
        private System.Windows.Forms.Panel yellowColor;
        private System.Windows.Forms.Panel greenColor;
        private System.Windows.Forms.Panel deepSkyBlueColor;
        private System.Windows.Forms.Panel blueColor;
        private System.Windows.Forms.Panel pinkColor;
        private System.Windows.Forms.Panel purpleColor;
        private System.Windows.Forms.Panel brightGreenColor;
        private System.Windows.Forms.Panel grayColor;
        private System.Windows.Forms.Panel brightBlueColor;
        private System.Windows.Forms.Panel whiteColor;
        private System.Windows.Forms.Panel brightYellowColor;
        private System.Windows.Forms.Panel brightOrangeColor;
        private System.Windows.Forms.Panel brightPurpleColor;
        private System.Windows.Forms.Panel brightBrownColor;
        private System.Windows.Forms.Panel nowColor;
        private System.Windows.Forms.NumericUpDown ImageSimilarity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel brownColor;
        private System.Windows.Forms.Panel brightGrayColor;
        private System.Windows.Forms.Panel blackColor;
        private System.Windows.Forms.Panel brightDeepBlueSkyColor;
        public System.Windows.Forms.ImageList resultImageList;
        private System.Windows.Forms.NumericUpDown ColorSimilarity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox filePathBox;
        private System.Windows.Forms.Panel customPallete1;
        private System.Windows.Forms.Panel customPallete2;
        private System.Windows.Forms.Panel customPallete3;
        private System.Windows.Forms.Panel customPallete4;
        private System.Windows.Forms.Panel customPallete5;
        private System.Windows.Forms.Panel customPallete6;
        private System.Windows.Forms.Panel customPallete7;
        private System.Windows.Forms.Panel customPallete8;
        private System.Windows.Forms.Panel customPallete9;
        private System.Windows.Forms.Panel customPallete10;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.CheckBox searchSubFolder;
    }
}

