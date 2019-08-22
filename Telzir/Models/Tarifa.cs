namespace Telzir.Models
{
    public class Tarifa
    {
        public int Id { get; set; }
        public int DdOrigem { get; set; }
        public int DdDestino { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Valor { get; set; }
    }
}