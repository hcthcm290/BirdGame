using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ConsoleApp1
{
    class pnlGame: Panel
    {
        Square[,] s;
        int W;
        Pen p;
        int level;
        Point trap;
        int count;
        public pnlGame(int xLocation, int yLocation, int width)
        {
            this.Location = new Point(xLocation, yLocation);
            W = width;
            this.Size = new Size(width, width);
            this.BackColor = Color.White;
            this.MouseClick += PnlGame_MouseClick;
            this.Paint += PnlGame_Paint;
            level = 2;
            count = 0;
            p = new Pen(Color.Red);

            // generate color matrix with the difference color cell
            s = new Square[level, level];
            Color c;
            Random rnd = new Random();
            int r, g, b;
            r = rnd.Next(0, 255);
            g = rnd.Next(0, 255);
            b = rnd.Next(0, 255);
            c = Color.FromArgb(r, g, b);
            trap = new Point(rnd.Next(0, level), rnd.Next(0, level));

            int sWidth = W / level;
            for (int i=0; i<level; i++)
            {
                for (int j=0; j<level; j++)
                {
                    if (i == trap.X && j == trap.Y)
                    {
                        Color temp = new Color();
                        if (b + 20 > 255)
                        {
                            temp = Color.FromArgb(r, g, b - 20);
                        }
                        else
                        {
                            temp = Color.FromArgb(r, g, b + 20);
                        }
                        
                        s[i, j] = new Square(sWidth * i, sWidth * j, sWidth, temp);
                    }
                    else
                    {
                        s[i, j] = new Square(sWidth * i, sWidth * j, sWidth, c);
                    }
                }
            }
        }

        private void PnlGame_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);
            for (int i = 0; i < level; i++)
            {
                for (int j = 0; j < level; j++)
                {
                    SolidBrush brush = new SolidBrush(s[i,j].color);

                    g.FillRectangle(brush, s[i,j].Rectangle());
                    g.DrawRectangle(p, s[i,j].Rectangle());
                }
            }
        }

        private void PnlGame_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            int sWidth = W / level;
            //MessageBox.Show("X:" + (e.X / sWidth).ToString() + " Y:" + (e.Y / sWidth).ToString());
            if (e.X/sWidth == trap.X && e.Y/sWidth == trap.Y)
            {
                count++;
                if (count == level)
                {
                    level = level + 1;
                    count = 0;
                }
                sWidth = W / level;
                s = new Square[level, level];
                Color c;
                Random rnd = new Random();
                int r, g, b;
                r = rnd.Next(0, 255);
                g = rnd.Next(0, 255);
                b = rnd.Next(0, 255);
                c = Color.FromArgb(r, g, b);
                trap = new Point(rnd.Next(0, level), rnd.Next(0, level));
                
                for (int i = 0; i < level; i++)
                {
                    for (int j = 0; j < level; j++)
                    {
                        if (i == trap.X && j == trap.Y)
                        {
                            Color temp = new Color();
                            if (b + 20 > 255)
                            {
                                temp = Color.FromArgb(r, g, b - 20);
                            }
                            else
                            {
                                temp = Color.FromArgb(r, g, b + 20);
                            }
            
                            s[i, j] = new Square(sWidth * i, sWidth * j, sWidth, temp);
                        }
                        else
                        {
                            s[i, j] = new Square(sWidth * i, sWidth * j, sWidth, c);
                        }
                    }
                }
                this.Invalidate();
            }
        }


    }
}
