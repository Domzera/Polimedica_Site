using Polimedica.Models;

namespace Polimedica.ViewModel
{
    public class CreateProdutoViewModel
    {
        public string NomeProduto { get; set; }
        public string DescricaoProduto {get;set;}
        public List<int> Marca { get; set; } = [];
        public float Preco {  get; set; }
        public List<int> Categoria { get; set; } = [];
        public string? Imagem1 { get; set; }
        public string? Imagem2 { get; set; }
        public string? Imagem3 { get; set; }
        public string? Imagem4 { get; set; }
        public string? Imagem5 { get; set; }
        public Boolean Ativo {  get; set; }
    }
}
