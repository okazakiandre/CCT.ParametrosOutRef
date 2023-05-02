namespace CCT.ParametrosOutRef.App.Domain
{
    public class Vendedor
    {
        public long NumeroCpf { get; set; }
        public string Nome { get; set; } = "";
        public double PercentualComissao { get; set; }
        public bool VendedorPrincipal { get; set; }
    }
}
