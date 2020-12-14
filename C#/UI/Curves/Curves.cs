using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curves
{
    public abstract class Curve
    {
        public List<Point> PositivePoints;
        public List<Point> NegativePoints;

        public abstract string Name { get; }
        public override string ToString()
        {
            return Name;
        }

        protected abstract double Formula(double x);

        public void Build(Region region, double scale)
        {
            PositivePoints = new List<Point>();
            NegativePoints = new List<Point>();
            double step;
            double x;
            double formula;
            int value;

            for (int i = -region.Width / 2; i < region.Width / 2; i++)
            {
                step = region.Width * scale / 20;
                x = 0.01 * i / scale;
                formula = Formula(x);
                if (formula < 0)
                    continue;
                value = (int)(Math.Sqrt(formula) * 100 * scale);
                if ((value > region.Height / 2) || (value < -region.Height / 2))
                {
                    continue;
                }
                PositivePoints.Add(new Point(i, value));
            }
            PositivePoints.ForEach((p) => NegativePoints.Insert(0, new Point(p.X, -p.Y)));
        }
    }
}
