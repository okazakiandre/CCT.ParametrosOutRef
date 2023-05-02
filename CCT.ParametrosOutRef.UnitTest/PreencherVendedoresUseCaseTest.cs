using CCT.ParametrosOutRef.App.Dto;
using CCT.ParametrosOutRef.App.UseCases;

namespace CCT.ParametrosOutRef.UnitTest
{
    [TestClass]
    [TestCategory("Com out")]
    public class PreencherVendedoresUseCaseTest
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
            var usc = new PreencherVendedoresUseCase();

            var res = usc.Executar(vendedoresElegiveis,
                                   comissaoProd,
                                   out var percentualComissaoUtilizada);

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
            var usc = new PreencherVendedoresUseCase();

            var res = usc.Executar(vendedoresElegiveis,
                                   comissaoProd,
                                   out var percentualComissaoUtilizada);

            Assert.AreEqual(5, res[0].PercentualComissao);
            Assert.IsTrue(res[1].VendedorPrincipal);
            Assert.AreEqual(10, res[1].PercentualComissao);
            Assert.AreEqual(10, percentualComissaoUtilizada);
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
            var usc = new PreencherVendedoresUseCase();

            var res = usc.Executar(vendedoresElegiveis,
                                   comissaoProd,
                                   out var percentualComissaoUtilizada);

            Assert.AreEqual(5, res[0].PercentualComissao);
            Assert.AreEqual(5, percentualComissaoUtilizada);
        }
    }
}