using System.ComponentModel.DataAnnotations.Schema;

namespace Polimedica.Models
{
    public class Promocao
    {
        public int Id { get; set; }
        public int ProdutoID { get; set; }
        public decimal? Preco { get; set; }
        public DateOnly Datainicio { get; set; }
        public DateOnly DataFinal { get; set; }
    }
}
