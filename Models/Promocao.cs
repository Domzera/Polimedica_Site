namespace Polimedica.Models
{
    public class Promocao
    {
        public int Id { get; set; }
        public int ProdutoID { get; set; }
        public Decimal preco { get; set; }
        public DateOnly Datainicio { get; set; }
        public DateOnly DataFinal { get; set; }
    }
}
