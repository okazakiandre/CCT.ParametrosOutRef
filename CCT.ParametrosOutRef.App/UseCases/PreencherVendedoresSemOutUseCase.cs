using CCT.ParametrosOutRef.App.Domain;
using CCT.ParametrosOutRef.App.Dto;

namespace CCT.ParametrosOutRef.App.UseCases
{
    public class PreencherVendedoresSemOutUseCase
    {
        public List<Vendedor> Executar(List<VendedorDto> vendedoresElegiveis,
                                       ComissaoProdutoDto comissaoProd)
        {
            var vendedores = new List<Vendedor>();
            var maiorQtdVendas = vendedoresElegiveis.Max(v => int.Parse(v.QuantidadeVendas));

            foreach (var vendEleg in vendedoresElegiveis)
            {
                var vendedor = new Vendedor();
                
                try
                {
                    vendedor.NumeroCpf = long.Parse(vendEleg.NumeroCpf);
                }
                catch(Exception)
                {
                    vendedor.NumeroCpf = 0;
                }

                if (!long.TryParse(vendEleg.NumeroCpf, out long cpf))
                {
                    vendedor.NumeroCpf = 0;
                }
                vendedor.NumeroCpf = cpf;

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
            }

            return vendedores;
        }
    }
}
