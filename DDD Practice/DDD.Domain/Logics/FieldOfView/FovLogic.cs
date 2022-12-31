using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DDD.Domain.Entities.FieldOfView;
using DDD.Domain.ValueObjects;

namespace DDD.Domain.Logics.FieldOfView
{
    public static class FovLogic
    {
        public static double CalcV(double focalLength, double ccdSize, double extension, double thickOfRing)
        {
            return (focalLength * ccdSize) / (extension + thickOfRing);
        }
        

        public static FovPoint CalcFovPoint(double focalLength, double ccdSize, double extension, double thickOfRing, double primaryPosition)
        {
            double v = CalcV(focalLength, ccdSize, extension, thickOfRing);
            double wd = v * (focalLength + extension + thickOfRing) / ccdSize - primaryPosition;
            return new FovPoint(v, wd);
        }
        
    }
}
