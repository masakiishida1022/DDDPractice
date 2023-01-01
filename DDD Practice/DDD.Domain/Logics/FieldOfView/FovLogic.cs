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
        public static double CalcV(double focalLength, double ccdSize, double thickOfRingPlusExtension)
        {
            return (focalLength * ccdSize) / thickOfRingPlusExtension;
        }
        

        public static FovPoint CalcFovPoint(double focalLength, double ccdSize, double thickOfRingPlusExtension, double primaryPosition)
        {
            double v = CalcV(focalLength, ccdSize, thickOfRingPlusExtension);
            double wd = v * (focalLength + thickOfRingPlusExtension) / ccdSize - primaryPosition;
            return new FovPoint(v, wd);
        }
        
    }
}
