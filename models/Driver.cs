using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entidades.models
{
    public class Driver : BaseEntity
    {
        public int TeamId { get; set; }
        public string Name { get; set; } = "Default Driver";
        public int RacingNumber { get; set; }

        public virtual Team Team { get; set; }
        public virtual DriverMedia DriverMedia { get; set; }
    }
}