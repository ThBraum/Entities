using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entidades.models
{
    public class DriverMedia : BaseEntity
    {
        public int DriverId { get; set; }
        public string Media { get; set; } = "";

        public virtual Driver? Driver { get; set; }
    }
}