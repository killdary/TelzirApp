using Telzir.Models;

namespace Telzir.Negocio
{
    public class CalculoTarifa
    {
        public decimal Valor { get; set; }
        public decimal Porcentagem { get; set; }
        public int MinutosPacote { get; set; }
        public int MinutosConsumidos { get; set; }
        public decimal ValorComPlano { get; set; }
        public decimal ValorSemPlano { get; set; }

        public CalculoTarifa ValorTarifa(){

            ValorSemPlano = MinutosConsumidos * Valor;

            ValorComPlano = ((MinutosConsumidos - MinutosPacote) <= 0) ? 0 : (MinutosConsumidos - MinutosPacote) * (Valor * (1 + Porcentagem/100));

            return this;
        }

    }
}