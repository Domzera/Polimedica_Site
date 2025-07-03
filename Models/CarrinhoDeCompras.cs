namespace Polimedica.Models
{
    public class CarrinhoDeCompras
    {
        public int Id { get; set; }
        public string? UsuarioId {  get; set; }
        public int ProdutoId {  get; set; }
        public int Quantidade {  get; set; }
        public DateOnly DataDaInclusao { get; set; }
    }
}
