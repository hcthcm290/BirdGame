using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ConsoleApp1
{
    class Square
    {
        public Color color { get; }
        int X;
        int Y;
        int Width;
        int Height;

        public Square(int x, int y, int w, Color c)
        {
            X = x;
            Y = y;
            Width = Height = w;
            color = c;
        }

        public Rectangle Rectangle()
        {
            return new Rectangle(X, Y, Width, Height);
        }

        public void Location(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
