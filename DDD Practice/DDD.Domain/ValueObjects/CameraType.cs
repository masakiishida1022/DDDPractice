using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.ValueObjects
{
    public class CameraType : ValueObject<CameraType>
    {
        private readonly string name;

        public CameraType(string name)
        {
            this.name = name;
        }

        protected override bool EqualsCore(CameraType other)
        {
            return this.name.Equals(other.name);
        }
    }
}
