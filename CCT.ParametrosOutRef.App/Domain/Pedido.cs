namespace CCT.ParametrosOutRef.App.Domain
{
    public class Pedido
    {
        public List<Vendedor> Vendedores { get; set; } = new List<Vendedor>();

        public double ObterComissaoPrincipal()
        {
            return (Vendedores.First(v => v.VendedorPrincipal)?.PercentualComissao).GetValueOrDefault();
        }
    }
}
