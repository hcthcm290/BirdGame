using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ConsoleApp1
{
    class Tutorial
    {
        public enum Phase
        {
            RightClick,
            WASD,
            Zoom,
            MoveCamera,
            None,
        }
        public Phase phase { get; set; } = new Phase();
        public Tutorial()
        {
            phase = Phase.RightClick;
        }
        public void Draw(PaintEventArgs e)
        {
            e.Graphics.ResetTransform();
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            switch (phase)
            {
                case Phase.RightClick:
                    e.Graphics.DrawString("Tutorial: Right-Click to set Destination for the bird", 
                                          new Font("Time New Roman", 30),
                                          new SolidBrush(Color.Black), 
                                          new Rectangle(0, 600, Constant.ScreenWidth, 200),
                                          format
                                          );
                    break;
                case Phase.WASD:
                    e.Graphics.DrawString("Tutorial: Use W,A,S,D key to move the Destination",
                                          new Font("Time New Roman", 30),
                                          new SolidBrush(Color.Black),
                                          new Rectangle(0, 600, Constant.ScreenWidth, 200),
                                          format
                                          );
                    break;
                case Phase.Zoom:
                    e.Graphics.DrawString("Tutorial: Use mouse scrolling to ZOOM IN and ZOOM OUT",
                                          new Font("Time New Roman", 30),
                                          new SolidBrush(Color.Black),
                                          new Rectangle(0, 600, Constant.ScreenWidth, 200),
                                          format
                                          );
                    break;
                case Phase.MoveCamera:
                    e.Graphics.DrawString("Tutorial: Have you ever played Dota? Now try to move your camera like that",
                                          new Font("Time New Roman", 30),
                                          new SolidBrush(Color.Black),
                                          new Rectangle(0, 600, Constant.ScreenWidth, 200),
                                          format
                                          );
                    break;
                case Phase.None:
                    break;
            }
        }
        public void ToNextPhase()
        {
            phase += 1;
        }
    }
}
