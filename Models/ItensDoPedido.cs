using System.ComponentModel.DataAnnotations.Schema;

namespace Polimedica.Models
{
    public class ItensDoPedido
    {
        public int Id { get; set; }
        [ForeignKey("Pedido")]
        public int PedidoId {  get; set; }
        public Pedido? Pedido { get; set; }
        [ForeignKey("Produto")]
        public int ProdutoId {  get; set; }
        public Produto? Produto { get; set; }
        public int Quantidade {  get; set; }
        public long PrecoUnitario {  get; set; }
    }
}
