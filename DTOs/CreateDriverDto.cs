namespace Entidades.DTOs
{
    public class CreateDriverDto
    {
        public string Name { get; set; } = string.Empty;
        public int RacingNumber { get; set; }
        public int TeamId { get; set; }
    }
}
