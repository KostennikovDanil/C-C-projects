using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curves
{
    public class Hyperbola : Curve
    {
        public override string Name => "Парабола";
        protected override double Formula(double x)
        {
            return 3 + 2 * x * x / 3;
        }
    }
}
