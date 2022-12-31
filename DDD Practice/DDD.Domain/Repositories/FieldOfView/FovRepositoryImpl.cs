using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.Domain.ValueObjects;

namespace DDD.Domain.Repositories.FieldOfView
{
    class FovRepositoryImpl : IFovRepository
    {
        public double GetCcdSize(CameraType cameraType)
        {
            return 0;
        }

        public double GetFocalLength(LensType lensType)
        {
            return 0;
        }

        public double GetPrimaryPosition(LensType lensType)
        {
            return 0;
        }

        public List<double> GetSelectableCloseUpRingThick(CameraType cameraType, LensType lensType)
        {
            return null;
        }

        public double GetMaxExtension(LensType lensType)
        {
            return 0;
        }

    }
}
