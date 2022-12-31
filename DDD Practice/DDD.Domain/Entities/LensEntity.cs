using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.Domain.ValueObjects;

namespace DDD.Domain.Entities
{
    public class LensEntity
    {
        public LensType LensType { get; }

        public double FocalLength { get; }
         
        public double MaxExtensionAmount { get; }
    }
}
