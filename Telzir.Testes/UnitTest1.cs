using System;
using Xunit;
using Telzir.Negocio;
using Telzir.Models;

namespace Telzir.Testes
{
    public class CalculoTarifasTeste
    {
        private readonly CalculoTarifa _calculoTarifa;
        public CalculoTarifasTeste()
        {
            _calculoTarifa = new CalculoTarifa();
        }

        [Fact]
        public void TestTarifaPlano30()
        {
            _calculoTarifa.Valor = 1.9M;
            _calculoTarifa.MinutosConsumidos = 20;
            _calculoTarifa.Porcentagem = 10;
            _calculoTarifa.MinutosPacote = 30;
            _calculoTarifa.ValorTarifa();
            Assert.Equal(0, _calculoTarifa.ValorComPlano);
            Assert.Equal(38, _calculoTarifa.ValorSemPlano);
        }

        [Fact]
        public void TestTarifaPlano60()
        {
            _calculoTarifa.Valor = 1.7M;
            _calculoTarifa.MinutosConsumidos = 80;
            _calculoTarifa.MinutosPacote = 60;
            _calculoTarifa.Porcentagem = 10;
            _calculoTarifa.ValorTarifa();
            Assert.Equal(37.40M, _calculoTarifa.ValorComPlano);
            Assert.Equal(136, _calculoTarifa.ValorSemPlano);
        }


        [Fact]
        public void TestTarifaPlano80()
        {
            _calculoTarifa.Valor = 1.9M;
            _calculoTarifa.MinutosConsumidos = 200;
            _calculoTarifa.MinutosPacote = 120;
            _calculoTarifa.Porcentagem = 10;
            _calculoTarifa.ValorTarifa();
            Assert.Equal(167.20M, _calculoTarifa.ValorComPlano);
            Assert.Equal(380, _calculoTarifa.ValorSemPlano);
        }


    }
}
