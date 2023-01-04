using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.Domain.ValueObjects;

namespace DDD.Domain.Repositories.FieldOfView
{
    public class FovRepositoryImpl : IFovRepository
    {
        public double GetCcdSize(CameraType cameraType)
        {
            return 2.304;
        }

        public double GetFocalLength(LensType lensType)
        {
            return 12.4;
        }

        public double GetPrimaryPosition(LensType lensType)
        {
            return 29.49+3.84;
        }

        public List<double> GetSelectableCloseUpRingThick(CameraType cameraType, LensType lensType)
        {
            return new List<double>() { 0.5, 3};
        }

        public double GetMaxExtension(LensType lensType)
        {
            return 1.2716;
        }

    }
}
