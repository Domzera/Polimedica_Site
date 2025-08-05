using Polimedica.Models;

namespace Polimedica.ViewModel
{
    public class CreateProdutoViewModel
    {
        public string NomeProduto { get; set; }
        public string DescricaoProduto { get;set;}
        public int[] MarcaId { get; set; }
        public float Preco {  get; set; }
        public int[] CategoriaId { get; set; }
        public IFormFile? Imagem1 { get; set; }
        public IFormFile? Imagem2 { get; set; }
        public IFormFile? Imagem3 { get; set; }
        public IFormFile? Imagem4 { get; set; }
        public IFormFile? Imagem5 { get; set; }
        public Boolean Ativo {  get; set; }
        public Boolean Promocao { get; set; }
        public float PrecoPromocional { get; set; }
        public DateOnly DataInicioPromocao { get; set; }
        public DateOnly DataFinalPromocao { get; set; }
        public Boolean TerEmCasa { get; set; }
    }
}
