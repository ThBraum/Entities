namespace Entidades.DTOs
{
    public class DriverMediaDto
    {
        public int? Id { get; set; }
        public int DriverId { get; set; }
        public string Media { get; set; } = string.Empty;
        public string? DriverName { get; set; }
    }
}
