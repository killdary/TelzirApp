using Telzir.Models;

namespace Telzir.Negocio
{
    public class CalculoTarifa
    {
        public Tarifa Tarifa { get; set; }
        public Pacote Pacote { get; set; }
        public int Minutos { get; set; }
        public decimal ValorComPlano { get; set; }
        public decimal ValorSemPlano { get; set; }

        public CalculoTarifa ValorTarifa(){

            ValorSemPlano = Minutos * Tarifa.Valor;

            ValorComPlano = ((Pacote.Minutos - Minutos) <= 0) ? 0 : (Pacote.Minutos - Minutos) * (Tarifa.Valor * (1 + Pacote.Plano.Porcentagem));

            return this;
        }

    }
}