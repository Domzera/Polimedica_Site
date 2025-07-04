using System.ComponentModel.DataAnnotations.Schema;

namespace Polimedica.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public string DescricaoProduto {  get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public Decimal Preco {  get; set; }
        public int QuantidadeEmEstoque {  get; set; }
        public string? Imagem1 {  get; set; }
        public string? Imagem2 { get; set; }
        public string? Imagem3 { get; set; }
        public string? Imagem4 { get; set; }
        public string? Imagem5 { get; set; }
        public DateOnly DataAdicionado { get; set; }
        public Boolean Ativo { get; set; }
    }
}
