using System.Collections.Generic;
using Entidades.models;

namespace Entidades.DTOs
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Year { get; set; }
        public List<DriverDto> Drivers { get; set; } = new();
    }
}
