using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.Domain.ValueObjects;

namespace DDD.Domain.Repositories.FieldOfView
{
    public interface IFovRepository
    {
        double GetCcdSize(CameraType cameraType);

        double GetFocalLength(LensType lensType);

        double GetPrimaryPosition(LensType lensType);

        List<double> GetSelectableCloseUpRingThick(CameraType cameraType, LensType lensType);

        double GetMaxExtension(LensType lensType);
    }
}
