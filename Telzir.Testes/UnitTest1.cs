using System;
using Xunit;
using Telzir.Negocio;
using Telzir.Models;

namespace Telzir.Testes
{
    public class UnitTest1
    {
        private readonly CalculoTarifa _calculoTarifa;
        public UnitTest1()
        {
            _calculoTarifa = new CalculoTarifa();
        }

        [Fact]
        public void Test1()
        {
            Tarifa t = new Tarifa();
            t.DdDestino = 11;
            t.DdOrigem = 17;
            t.Valor = 5;

            Plano p = new Plano();


        }
    }
}
