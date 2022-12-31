using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DDD.Domain.ValueObjects;
using System.Windows;

namespace DDD.Domain.Entities
{
    public class FieldOfViewEntity
    {
        public  LensType LensType { get; }
        public  ResolutionId ResolutionId { get; }

        

        //最大繰り出し量
        private double ccdSize;
        
        private double CalcViewSize(LensFeature lensFeature, double extensionAmount)
        {
            return (lensFeature.FocalLength * ccdSize) / (extensionAmount + lensFeature.ThicknessOfRing);
        }

        public Point CalcFieldViewPoint(LensFeature lensFeature, double extensionAmount)
        {
            double v = CalcViewSize(lensFeature, extensionAmount);
            double wd =  v * (lensFeature.FocalLength + extensionAmount + lensFeature.ThicknessOfRing) / ccdSize - lensFeature.UnknownConstant;
            return new Point(v, wd);
        }


        public class LensFeature
        {
            public LensFeature(double f, double t, double u, double maxExt)
            {
                this.FocalLength = f;
                this.ThicknessOfRing = t;
                this.UnknownConstant = u;
                this.MaxExtensionAmount = maxExt;
            }
            public double FocalLength { get; }
            public double ThicknessOfRing { get; }
            public double UnknownConstant { get; }
            public double MaxExtensionAmount { get; }
        }
        
        public class FovPoint
        {
            public FovPoint(double v, double wd)
            {
                this.View = v;
                this.WorkingDistance = wd;
            }
            public double View { get; }
            public double WorkingDistance { get; }
        }
    }
}
