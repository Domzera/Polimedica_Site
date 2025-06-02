using System.ComponentModel.DataAnnotations.Schema;

namespace Polimedica.Models
{
    public class CarrinhoDeCompras
    {
        public int Id { get; set; }
        [ForeignKey("Usuario")]
        public string? UsuarioId {  get; set; }
        public Usuario? Usuario { get; set; }
        [ForeignKey("Produto")]
        public int ProdutoId {  get; set; }
        public Produto? Produto { get; set; }
        public int Quantidade {  get; set; }
        public DateOnly DataDaInclusao { get; set; }
    }
}
