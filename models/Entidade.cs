using System;

namespace Entidades.Models
{
    public class Entidade
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
