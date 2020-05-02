using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace ImageFinder
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
        enum DrawMode { Pencil, Eraser };

        DrawMode drawMode = DrawMode.Pencil;

        bool drawing = false;// 현재 마우스를 누른 채로 그리거나 지우고 있는지
        bool penPreview = false;

        Pen pen = new Pen(Color.Black);
        Pen previewPen = new Pen(Color.LightGray, 1.0f);

        Color nowColor = Color.Black;
        float penWidth = 20.0f;

        Point lastMousePoint = new Point();

        static readonly float maxPenWidth = 300.0f;
        static readonly float minPenWidth = 1.0f;

        Panel nowColorViewPanel = null;// 현재 선택된 색을 보여주는 패널

        DrawInformation nowDrawInformation = new DrawInformation();

        PictureBox pictureBox = null;
        Bitmap drawImage = null;
        Bitmap penWidthPreviewImage = null;

        Button changeToPencilButton = null;
        Button changeToEraserButton = null;
        Button clearButton = null;

        public DrawBox(PictureBox pictureBox, Button changeToPencilButton, Button changeToEraserButton, Button clearButton)
        {
            if (pictureBox == null || changeToPencilButton == null || changeToEraserButton == null || clearButton == null)
                return;

            this.pictureBox = pictureBox;
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
            }
            else
            {// 지우개에서 연필로 변경
                pen.Color = nowColor;
                drawMode = DrawMode.Pencil;
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

            palletePanel.MouseClick += PaletteClick;
        }

        private void PaletteClick(object sender, MouseEventArgs e)// 팔레트를 누르면 그리는 색을 변경
        {
            if (drawMode != DrawMode.Pencil)
                return;

            if (e.Button == MouseButtons.Left)
            {
                var colorPanel = (Panel)sender;
                nowColor = colorPanel.BackColor;
                pen.Color = nowColor;

                if (nowColorViewPanel == null)
                    return;

                nowColorViewPanel.BackColor = nowColor;
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