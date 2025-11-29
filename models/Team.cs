using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entidades.models
{
    public class Team : BaseEntity
    {
        public Team()
        {
            Drivers = new HashSet<Driver>();
        }

        public string Name { get; set; } = "Default Team";
        public int Year { get; set; } = 2025;

        public virtual ICollection<Driver> Drivers { get; set; }
    }
}