using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curves
{
    public class Parabola : Curve
    {
        public override string Name => "Парабола";
        protected override double Formula(double x)
        {
            return x;
        }
    }
}
