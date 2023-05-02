using CCT.ParametrosOutRef.App.Dto;
using CCT.ParametrosOutRef.App.UseCases;

namespace CCT.ParametrosOutRef.UnitTest
{
    [TestClass]
    [TestCategory("Sem out")]
    public class PreencherVendedoresSemOutUseCaseTest
    {
        [TestMethod]
        public void DeveriaRetornarVendedoresPreenchidos()
        {
            var vendedoresElegiveis = new List<VendedorDto>()
            {
                new VendedorDto() { NumeroCpf = "12345678901", Nome = "Fulano", QuantidadeVendas = "0" },
                new VendedorDto() { NumeroCpf = "98765432101", Nome = "Ciclano", QuantidadeVendas = "0" }
            };
            var comissaoProd = new ComissaoProdutoDto();
            var usc = new PreencherVendedoresSemOutUseCase();

            var res = usc.Executar(vendedoresElegiveis, comissaoProd);

            Assert.AreEqual(12345678901, res[0].NumeroCpf);
            Assert.AreEqual("Fulano", res[0].Nome);
            Assert.AreEqual(98765432101, res[1].NumeroCpf);
            Assert.AreEqual("Ciclano", res[1].Nome);
        }

        [TestMethod]
        public void DeveriaRetornarVendedorPrincipalComComissaoPrincipalEDemaisComComissaoBase()
        {
            var vendedoresElegiveis = new List<VendedorDto>()
            {
                new VendedorDto() { NumeroCpf = "12345678901", Nome = "Fulano", QuantidadeVendas = "3" },
                new VendedorDto() { NumeroCpf = "98765432101", Nome = "Ciclano", QuantidadeVendas = "5" }
            };
            var comissaoProd = new ComissaoProdutoDto()
            {
                PercentualComissaoPrincipal = 10,
                PercentualComissaoBase = 5
            };
            var usc = new PreencherVendedoresSemOutUseCase();

            var res = usc.Executar(vendedoresElegiveis, comissaoProd);

            Assert.AreEqual(5, res[0].PercentualComissao);
            Assert.IsTrue(res[1].VendedorPrincipal);
            Assert.AreEqual(10, res[1].PercentualComissao);
        }

        [TestMethod]
        public void DeveriaRetornarVendedorPrincipalComComissaoBaseSeTiverApenasUmVendedor()
        {
            var vendedoresElegiveis = new List<VendedorDto>()
            {
                new VendedorDto() { NumeroCpf = "12345678901", Nome = "Fulano", QuantidadeVendas = "3" }
            };
            var comissaoProd = new ComissaoProdutoDto()
            {
                PercentualComissaoPrincipal = 10,
                PercentualComissaoBase = 5
            };
            var usc = new PreencherVendedoresSemOutUseCase();

            var res = usc.Executar(vendedoresElegiveis, comissaoProd);

            Assert.AreEqual(5, res[0].PercentualComissao);
        }
    }
}