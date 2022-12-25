using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.ValueObjects
{
    public class ResolutionId : ValueObject<ResolutionId>
    {
        public static readonly ResolutionId Resolution1024X1024 = new ResolutionId(1024, 1024);
        public static readonly ResolutionId Resolution5024X2024 = new ResolutionId(5024, 2024);

        private readonly int xResolution;
        private readonly int yResolution;


        public ResolutionId(int x, int y)
        {
            this.xResolution = x;
            this.yResolution = y;
        }

        protected override bool EqualsCore(ResolutionId other)
        {
            return (this.xResolution == other.xResolution) && (this.xResolution == other.xResolution);
        }

        public string DisplayValue()
        {
            return $"{xResolution}x{yResolution}";
        }
    }
}
