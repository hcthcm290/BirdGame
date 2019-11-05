using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ConsoleApp1
{
    class Camera
    {
        static public float Zoom { get; set; } = 1;
        static public PointF Location { get; set; } = new Point(0, 0);
        static public Point ScreenToWorldPoint(Point point)
        {
            Point worldPoint = new Point();
            worldPoint.X = (int)(point.X / Camera.Zoom + Camera.Location.X / Camera.Zoom + Constant.ScreenWidth  * (1 - 1 / Camera.Zoom) / 2);
            worldPoint.Y = (int)(point.Y / Camera.Zoom + Camera.Location.Y / Camera.Zoom + Constant.ScreenHeight * (1 - 1 / Camera.Zoom) / 2);
            return worldPoint;
        }
    }
}
