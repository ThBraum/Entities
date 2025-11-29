namespace Entidades.DTOs
{
    public class DriverDto
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int RacingNumber { get; set; }
        public int TeamId { get; set; }
        public string? TeamName { get; set; }
    }
}
