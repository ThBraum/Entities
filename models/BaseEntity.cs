using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entidades.models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}