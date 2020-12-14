using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Curves
{
    public struct Region
    {
        public Point UpperLeft;
        public Point LowerRight;

        public Region(Point left, Point right)
        {
            UpperLeft = left;
            LowerRight = right;
        }

        public int Width => Math.Abs(UpperLeft.X - LowerRight.X);
        public int Height => Math.Abs(UpperLeft.Y - LowerRight.Y);
    }
}
