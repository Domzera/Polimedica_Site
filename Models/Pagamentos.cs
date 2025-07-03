namespace Polimedica.Models
{
    public class Pagamentos
    {
        public int Id { get; set; }
        public int PedidoId {  get; set; }
        public Pedido? Pedido { get; set; }
        public DateOnly DataDoPagamento { get; set; }
        public long Valor {  get; set; }
        public string? MetodoDePagamento { get; set; }
        public string? StatusDoPagamento { get; set; }
    }
}
