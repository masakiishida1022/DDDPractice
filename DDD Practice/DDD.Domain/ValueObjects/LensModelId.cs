using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.ValueObjects
{
    public class LensModelId : ValueObject<LensModelId>
    {
        private readonly string name;



        public LensModelId(string name)
        {
            this.name = name;
        }

        protected override bool EqualsCore(LensModelId other)
        {
            return this.name.Equals(other.name);
        }
    }
}
