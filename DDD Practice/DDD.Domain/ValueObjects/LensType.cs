using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.ValueObjects
{
    public class LensType : ValueObject<LensType>
    {
        private readonly string name;



        public LensType(string name)
        {
            this.name = name;
        }

        protected override bool EqualsCore(LensType other)
        {
            return this.name.Equals(other.name);
        }
    }
}
