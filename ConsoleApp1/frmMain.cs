using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ConsoleApp1
{
    class frmMain: Form
    {
        Timer timer;
        List<Bird> bird;
        DateTime time;
        Point destination;
        public frmMain()
        {
            Init();
        }
        Point mouseL;
        Tutorial tutorial;

        void Init()
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            this.DoubleBuffered = true;
            mouseL = new Point();
            bird = new List<Bird>();
            bird.Add(new Bird());
            timer = new Timer();
            timer.Interval = 30;
            timer.Tick += UpdateGame;
            timer.Start();
            DoubleBuffered = true;
            this.Paint += Draw;
            this.MouseClick += Mouse_Click;
            this.MouseWheel += Mouse_Roll;
            this.KeyDown += Key_Press;
            this.KeyUp += Key_Up;
            this.MouseMove += Mouse_Move;
            destination = new Point(10, 10);
            time = DateTime.Now;
            tutorial = new Tutorial();
        }

        void Draw(object o, PaintEventArgs e)
        {
            tutorial.Draw(e);
            for (int i = 0; i < bird.Count(); i++)
            {
                bird[i].Draw(e);
            }
            e.Graphics.DrawEllipse(new Pen(Color.Black), new Rectangle(destination, new Size(10, 10)));

        }

        void Mouse_Click(object o, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                mouseL = new Point(e.X, e.Y);
                destination = Camera.ScreenToWorldPoint(mouseL);
                if(tutorial.phase == Tutorial.Phase.RightClick)
                {
                    tutorial.ToNextPhase();
                }
            }
        }

        void Mouse_Roll(object o, MouseEventArgs e)
        {
            if(tutorial.phase == Tutorial.Phase.Zoom)
            {
                tutorial.ToNextPhase();
            }
            if(e.Delta > 0)
            {
                Camera.Zoom += 0.03f;
            }
            else if(e.Delta < 0)
            {
                Camera.Zoom -= 0.03f;
                if(Camera.Zoom < 0.2f)
                {
                    Camera.Zoom = 0.2f;
                }
            }
        }

        void Mouse_Move(object o, MouseEventArgs e)
        {
            Mouse.Location = e.Location;
        }

        void Key_Press(object o, KeyEventArgs e)
        {
            if((e.KeyCode == Keys.A || e.KeyCode == Keys.D || e.KeyCode == Keys.W || e.KeyCode == Keys.S) && tutorial.phase == Tutorial.Phase.WASD)
            {
                tutorial.ToNextPhase();
            }
            if (e.KeyCode == Keys.A)
            {
                Keyboard.AddToList("A");
            }
            if (e.KeyCode == Keys.D)
            {
                Keyboard.AddToList("D");
            }
            if (e.KeyCode == Keys.W)
            {
                Keyboard.AddToList("W");
            }
            if (e.KeyCode == Keys.S)
            {
                Keyboard.AddToList("S");
            }
            if(e.KeyCode == Keys.Space)
            {
                bird.Add(new Bird());
            }
        }

        void Key_Up(object o, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                Keyboard.DelFromList("A");
            }
            if (e.KeyCode == Keys.D)
            {
                Keyboard.DelFromList("D");
            }
            if (e.KeyCode == Keys.W)
            {
                Keyboard.DelFromList("W");
            }
            if (e.KeyCode == Keys.S)
            {
                Keyboard.DelFromList("S");
            }
        }

        void UpdateGame(object o, EventArgs e)
        {
            TimeSpan ts = DateTime.Now - time;
            float dt = (float)(ts.Milliseconds) / 1000;
            for (int i = 0; i < bird.Count(); i++)
            {
                PointF acce = new PointF(destination.X - (bird[i].location.X + bird[i].width / 2), destination.Y - (bird[i].location.Y + bird[i].height / 2));
                PointF acceFriction = new PointF(0, 0);
                if (bird[i].vel.X != 0)
                {
                    acceFriction.X = 100 * -1 * bird[i].vel.X / (float)Math.Sqrt(bird[i].vel.X * bird[i].vel.X + bird[i].vel.Y * bird[i].vel.Y);
                }
                if (bird[i].vel.Y != 0)
                {
                    acceFriction.Y = 100 * -1 * bird[i].vel.Y / (float)Math.Sqrt(bird[i].vel.X * bird[i].vel.X + bird[i].vel.Y * bird[i].vel.Y);
                }
                bird[i].acce = new PointF(acce.X + acceFriction.X, acce.Y + acceFriction.Y);
                bird[i].vel = new PointF(bird[i].vel.X + bird[i].acce.X * dt, bird[i].vel.Y + bird[i].acce.Y * dt);
                bird[i].location = new PointF(bird[i].location.X + bird[i].vel.X * dt, bird[i].location.Y + bird[i].vel.Y * dt);
                bird[i].UpdateFrame();
            }

            if (Mouse.Location.X == 0 || 
                Mouse.Location.X == Constant.ScreenWidth - 1 || 
                Mouse.Location.Y == 0 || 
                Mouse.Location.Y == Constant.ScreenHeight - 1)
            {
                if (tutorial.phase == Tutorial.Phase.MoveCamera)
                {
                    tutorial.ToNextPhase();
                }
            }

            if(Mouse.Location.X == 0)
            {
                Camera.Location = new PointF(Camera.Location.X - 700 * dt, Camera.Location.Y);
            }
            if (Mouse.Location.X == Constant.ScreenWidth - 1)
            {
                Camera.Location = new PointF(Camera.Location.X + 700 * dt, Camera.Location.Y);
            }
            if (Mouse.Location.Y == 0)
            {
                Camera.Location = new PointF(Camera.Location.X, Camera.Location.Y - 700 * dt);
            }
            if (Mouse.Location.Y == Constant.ScreenHeight - 1)
            {
                Camera.Location = new PointF(Camera.Location.X, Camera.Location.Y + 700 * dt);
            }

            if(Keyboard.KeyPress("A"))
            {
                destination.X -= (int)(100*dt);
            }
            if (Keyboard.KeyPress("D"))
            {
                destination.X += (int)(100 * dt);
            }
            if (Keyboard.KeyPress("W"))
            {
                destination.Y -= (int)(100 * dt);
            }
            if (Keyboard.KeyPress("S"))
            {
                destination.Y += (int)(100 * dt);
            }

            time = DateTime.Now;
            this.Invalidate();
        }
    }
}
