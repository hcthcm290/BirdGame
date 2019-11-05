using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ConsoleApp1
{
    class Bird
    {
        static private Bitmap myImg = new Bitmap(Environment.CurrentDirectory + "\\bird-2.png");

        public PointF location { get; set; }
        float currentFrame;
        public int width { get; }
        public int height { get; }
        public PointF vel { get; set; }
        public PointF acce { get; set; }

        public Bird()
        {
            myImg.MakeTransparent();
            location = new PointF(0, 0);
            currentFrame = 0;
            width = 343;
            height = 300;
            vel = new PointF(0, 0);
        }

        public Bird(int start_X, int start_Y)
        {
            myImg.MakeTransparent();
            location = new PointF(start_X, start_Y);
            currentFrame = 0;
            width = 343;
            height = 300;
            vel = new PointF(0, 0);
        }

        public void Draw(PaintEventArgs e)
        {
            e.Graphics.ResetTransform();
            e.Graphics.TranslateTransform(-Camera.Location.X, -Camera.Location.Y);
            e.Graphics.TranslateTransform(Constant.ScreenWidth / 2, Constant.ScreenHeight / 2);
            e.Graphics.ScaleTransform(Camera.Zoom, Camera.Zoom);
            e.Graphics.TranslateTransform(-Constant.ScreenWidth / 2, -Constant.ScreenHeight / 2);
            e.Graphics.DrawImage(myImg,location.X, location.Y, new Rectangle((int)currentFrame%3*width, (int)currentFrame/3*height, width, height), GraphicsUnit.Pixel);
        }

        public void UpdateFrame()
        {
            currentFrame = (currentFrame + 0.5f) % 9;
        }
    }
}
