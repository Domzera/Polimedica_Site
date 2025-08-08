namespace Polimedica.ViewModel
{
    public class ProdutoCategoriaViewModel
    {
        public int? ProdutoId { get; set; }
        public string? NomeProduto { get; set; }
        public string? DescricaoProduto { get; set; }
        public int[]? MarcaId { get; set; }
        public Decimal? Preco { get; set; }
        public string? Imagem1 { get; set; }
        public string? Imagem2 { get; set; }
        public string? Imagem3 { get; set; }
        public string? Imagem4 { get; set; }
        public string? Imagem5 { get; set; }
        public Boolean Ativo { get; set; }
        public Boolean? Promocao { get; set; }
        public float? PrecoPromocional { get; set; }
        public DateOnly? DataInicioPromocao { get; set; }
        public DateOnly? DataFinalPromocao { get; set; }
    }
}
