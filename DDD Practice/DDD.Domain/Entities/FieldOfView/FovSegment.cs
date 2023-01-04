using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DDD.Domain.Entities.FieldOfView
{
    public class FovSegment
    {
        public double ThicknessOfRing { get; }
        public FovPoint Start { get; }
        public FovPoint End { get; }
        public FovSegment(double thickness, FovPoint start, FovPoint end)
        {
            this.ThicknessOfRing = thickness;
            this.Start = start;
            this.End = end;
        }

    }
}
