namespace Polimedica.ViewModel
{
    public class ProdutoDetalheViewModel
    {
        public string NomeProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public int MarcaId { get; set; }
        public float Preco { get; set; }
        public int CategoriaId { get; set; }
        public string? Imagem1 { get; set; }
        public string? Imagem2 { get; set; }
        public string? Imagem3 { get; set; }
        public string? Imagem4 { get; set; }
        public string? Imagem5 { get; set; }
        public Boolean Ativo { get; set; }
    }
}
