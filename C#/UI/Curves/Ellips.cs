using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curves
{
    public class Ellips : Curve
    {
        public override string Name => "Эллипс";
        protected override double Formula(double x)
        {
            return 2 - 3 * x * x / 4;
        }
    }
}
