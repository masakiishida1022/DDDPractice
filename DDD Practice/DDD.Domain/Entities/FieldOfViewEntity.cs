using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.Domain.ValueObjects;

namespace DDD.Domain.Entities
{
    public class FieldOfViewEntity
    {
        public  LensModelId LensModelId { get; }
        public  ResolutionId ResolutionId { get; }

        public Dictionary<int, FovData> DataDic;
        public FieldOfViewEntity(LensModelId lensModelId, ResolutionId resolutionId, Dictionary<int, FovData> fovDataDic)
        {
            this.LensModelId = lensModelId;
            this.ResolutionId = resolutionId;
        }

        public class FovData
        {
        }
    }
}
