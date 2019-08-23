using System.Collections.Generic;

namespace Telzir.Models
{
    public class PlanoPacotes
    {
        public Plano Plano { get; set; }
        public IEnumerable<Pacote> Pacotes { get; set; }       
        public IEnumerable<Tarifa> Tarifas { get; set; }

    }
}