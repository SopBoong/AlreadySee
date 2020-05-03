using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace AlreadySee
{
    class DrawInformation
    {
        Point point = new Point();

        public void SetPoint(int x, int y)
        {
            point.X = x;
            point.Y = y;
        }

        public Point GetRecentPoint()
        {
            return point;
        }
    }

    class DrawBox
    {
        bool enabled = true;
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                if (value)
                {
                    if (drawMode == DrawMode.Eraser)
                    {
                        PalleteOff();
                        changeToPencilButton.Enabled = true;
                        changeToEraserButton.Enabled = false;
                    }
                    else
                    {
                        PalleteOn();
                        changeToPencilButton.Enabled = false;
                        changeToEraserButton.Enabled = true;
                    }
                    clearButton.Enabled = true;
                    pictureBox.Enabled = true;
                }
                else
                {
                    PalleteOff();
                    changeToPencilButton.Enabled = false;
                    changeToEraserButton.Enabled = false;
                    clearButton.Enabled = false;
                    pictureBox.Enabled = false;
                }

                enabled = value;
            }
        }
        enum DrawMode { Pencil, Eraser };

        DrawMode drawMode = DrawMode.Pencil;

        bool drawing = false;// 현재 마우스를 누른 채로 그리거나 지우고 있는지
        bool penPreview = false;

        Pen pen = new Pen(Color.Black);
        Pen previewPen = new Pen(Color.LightGray, 1.0f);

        Color nowColor = Color.Black;
        float penWidth = 20.0f;
        public float PenWidth { get { return penWidth; } set { penWidth = value; pen.Width = penWidth; } }

        Point lastMousePoint = new Point();

        static readonly float maxPenWidth = 300.0f;
        static readonly float minPenWidth = 1.0f;

        Panel nowColorViewPanel = null;// 현재 선택된 색을 보여주는 패널

        DrawInformation nowDrawInformation = new DrawInformation();
        ColorDialog colorDialog;

        PictureBox pictureBox = null;
        Bitmap drawImage = null;
        Bitmap penWidthPreviewImage = null;

        Button changeToPencilButton = null;
        Button changeToEraserButton = null;
        Button clearButton = null;

        List<Panel> palleteList = new List<Panel>();

        public DrawBox(PictureBox pictureBox, Button changeToPencilButton, Button changeToEraserButton, Button clearButton, ColorDialog colorDialog)
        {
            if (pictureBox == null || changeToPencilButton == null || changeToEraserButton == null || clearButton == null || colorDialog == null)
                return;

            this.pictureBox = pictureBox;
            this.colorDialog = colorDialog;
            drawImage = new Bitmap(pictureBox.Width, pictureBox.Height);
            penWidthPreviewImage = new Bitmap(pictureBox.Width, pictureBox.Height);

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            pen.Width = penWidth;

            this.pictureBox.MouseDown += DrawingStart;
            this.pictureBox.MouseUp += DrawingEnd;
            this.pictureBox.MouseMove += Drawing;
            this.pictureBox.MouseWheel += PenWidthControl;
            this.pictureBox.MouseEnter += PenPreviewStart;
            this.pictureBox.MouseLeave += PenPreviewEnd;

            this.changeToPencilButton = changeToPencilButton;
            this.changeToEraserButton = changeToEraserButton;
            this.clearButton = clearButton;

            this.changeToPencilButton.Click += PencilOrEraserChangeButtonClick;
            this.changeToEraserButton.Click += PencilOrEraserChangeButtonClick;
            this.clearButton.Click += ClearButtonClick;
        }

        public void SetNowColor(Color color)
        {
            nowColor = color;
            pen.Color = nowColor;

            if (nowColorViewPanel == null)
                return;

            nowColorViewPanel.BackColor = nowColor;
        }

        public Color GetNowColor()
        {
            return nowColor;
        }

        private void PencilOrEraserChangeButtonClick(object sender, EventArgs e)
        {
            ChangeDrawMode();
        }

        private void ClearButtonClick(object sender, EventArgs e)
        {
            Clear();
        }

        private void ChangeDrawMode()
        {
            if (drawMode == DrawMode.Pencil)
            {// 연필에서 지우개로 변경
                pen.Color = SystemColors.ControlDark;// 진짜 지우지 않고 배경 색으로 덮어씌움
                drawMode = DrawMode.Eraser;
                PalleteOff();// 지우개 모드 에서는 팔레트를 선택할 수 없게 변경
            }
            else
            {// 지우개에서 연필로 변경
                pen.Color = nowColor;
                drawMode = DrawMode.Pencil;
                PalleteOn();
            }

            changeToPencilButton.Enabled = !changeToPencilButton.Enabled;
            changeToEraserButton.Enabled = !changeToEraserButton.Enabled;
        }

        public void Clear()
        {
            using (var g = Graphics.FromImage(drawImage))
            {
                g.Clear(Color.Transparent);
            }

            pictureBox.BackgroundImage = null;
            pictureBox.BackgroundImage = drawImage;
        }

        public void SetNowColorViewPanel(Panel nowColorViewPanel)
        {
            if (nowColorViewPanel == null)
                return;

            this.nowColorViewPanel = nowColorViewPanel;
            nowColorViewPanel.BackColor = pen.Color;
        }

        public void AddPallete(Panel palletePanel)
        {
            if (palletePanel == null)
                return;

            palleteList.Add(palletePanel);

            palletePanel.MouseClick += PaletteClick;
        }

        public void AddCustomPallete(Panel palletePanel)
        {
            AddPallete(palletePanel);

            palletePanel.MouseDoubleClick += PaletteDoubleClick; 
        }

        public void PalleteOn()
        {
            palleteList.ForEach(p => p.Enabled = true);
            nowColorViewPanel.Enabled = true;
        }

        public void PalleteOff()
        {
            palleteList.ForEach(p => p.Enabled = false);
            nowColorViewPanel.Enabled = false;
        }

        private void PaletteClick(object sender, MouseEventArgs e)// 팔레트를 누르면 그리는 색을 변경
        {
            if (e.Button == MouseButtons.Left)
            {
                var colorPanel = (Panel)sender;
                SetNowColor(colorPanel.BackColor);
            }
        }

        private void PaletteDoubleClick(object sender, MouseEventArgs e)// 팔레트를 누르면 그리는 색을 변경
        {
            Panel panel = sender as Panel;
            if (e.Button == MouseButtons.Left)
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    panel.BackColor = colorDialog.Color;
                    SetNowColor(panel.BackColor);
                }
            }
        }

        private void DrawPenWidthPreview(int x, int y)
        {
            using (var g = Graphics.FromImage(penWidthPreviewImage))
            {
                g.Clear(Color.Transparent);
                g.DrawEllipse(previewPen, x - penWidth * 0.5f, y - penWidth * 0.5f, penWidth, penWidth);
            }
            pictureBox.Image = penWidthPreviewImage;
        }

        private void ClearPenWidthPreview()
        {
            using (var g = Graphics.FromImage(penWidthPreviewImage))
            {
                g.Clear(Color.Transparent);
            }

            pictureBox.Image = penWidthPreviewImage;
        }

        private void Drawing(object sender, MouseEventArgs e)
        {
            if (penPreview)
            {
                DrawPenWidthPreview(e.X, e.Y);
            }

            if (!drawing)
                return;

            lastMousePoint.X = e.X;
            lastMousePoint.Y = e.Y;

            var lastPoint = nowDrawInformation.GetRecentPoint();
            var nowPoint = new Point(e.X, e.Y);

            nowDrawInformation.SetPoint(e.X, e.Y);

            using (var g = Graphics.FromImage(drawImage))
            {
                g.DrawLine(pen, lastPoint, nowPoint);
            }

            pictureBox.BackgroundImage = null;
            pictureBox.BackgroundImage = drawImage;
        }

        private void DrawingStart(object sender, MouseEventArgs e)
        {
            drawing = true;

            nowDrawInformation.SetPoint(e.X, e.Y);

            using (var g = Graphics.FromImage(drawImage))
            {
                g.FillEllipse(pen.Brush, e.X - penWidth * 0.5f, e.Y - penWidth * 0.5f, penWidth, penWidth);
            }

            pictureBox.BackgroundImage = null;
            pictureBox.BackgroundImage = drawImage;
        }

        private void DrawingEnd(object sender, MouseEventArgs e)
        {
            drawing = false;
        }

        private void PenWidthControl(object sender, MouseEventArgs e)
        {
            DrawPenWidthPreview(e.X, e.Y);
            if (e.Delta > 0)
            {
                penWidth *= 1.1f;
            }

            if (e.Delta < 0)
            {
                penWidth *= 0.9f;
            }

            if (penWidth > maxPenWidth)
                penWidth = maxPenWidth;
            if (penWidth < minPenWidth)
                penWidth = minPenWidth;

            pen.Width = penWidth;   
        }

        private void PenPreviewStart(object sender, EventArgs e)
        {
            penPreview = true;
        }

        private void PenPreviewEnd(object sender, EventArgs e)
        {
            penPreview = false;
            ClearPenWidthPreview();
        }

        public Bitmap GetBitMap()
        {
            drawImage.MakeTransparent(SystemColors.ControlDark);// 이미지를 리턴할 때 지우개로 덧칠한 부분을 투명화 시킴
            return drawImage;
        }
    }
}