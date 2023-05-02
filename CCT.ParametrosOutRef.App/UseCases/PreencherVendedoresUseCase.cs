using CCT.ParametrosOutRef.App.Domain;
using CCT.ParametrosOutRef.App.Dto;

namespace CCT.ParametrosOutRef.App.UseCases
{
    public class PreencherVendedoresUseCase
    {
        public List<Vendedor> Executar(List<VendedorDto> vendedoresElegiveis,
                                       ComissaoProdutoDto comissaoProd,
                                       out double percentualComissaoUtilizada)
        {
            var vendedores = new List<Vendedor>();
            var maiorQtdVendas = vendedoresElegiveis.Max(v => int.Parse(v.QuantidadeVendas));
            percentualComissaoUtilizada = comissaoProd.PercentualComissaoPrincipal;

            foreach (var vendEleg in vendedoresElegiveis)
            {
                var vendedor = new Vendedor();
                vendedor.NumeroCpf = long.Parse(vendEleg.NumeroCpf);
                vendedor.Nome = vendEleg.Nome;

                if (int.Parse(vendEleg.QuantidadeVendas) == maiorQtdVendas)
                {
                    vendedor.VendedorPrincipal = true;
                    vendedor.PercentualComissao = comissaoProd.PercentualComissaoPrincipal;
                }
                else
                {
                    vendedor.PercentualComissao = comissaoProd.PercentualComissaoBase;
                }

                vendedores.Add(vendedor);
            }

            if (vendedoresElegiveis.Count == 1)
            {
                vendedores[0].PercentualComissao = comissaoProd.PercentualComissaoBase;
                percentualComissaoUtilizada = comissaoProd.PercentualComissaoBase;
            }

            return vendedores;
        }
    }
}
