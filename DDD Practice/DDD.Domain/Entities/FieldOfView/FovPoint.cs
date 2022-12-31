using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Entities.FieldOfView
{
    public class FovPoint
    {
        public double V { get; }
        public double Wd { get; }
        public FovPoint(double v, double wd)
        {
            this.V = v;
            this.Wd = wd;
        }
    }
}
