namespace Polimedica.Models
{
    public class CuponDesconto
    {
        public string? Id { get; set; }
        public string? Codigo {  get; set; }
        public string? Descricao {  get; set; }
        public long DescontoPercentual {  get; set; }
        public DateOnly DataDaExpiracao { get; set; }
        public int Quantidade {  get; set; }
    }
}
