using System.ComponentModel.DataAnnotations.Schema;

namespace Polimedica.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        [ForeignKey("Usuario")]
        public string? UsuarioId {  get; set; }
        public Usuario? Usuario { get; set; }
        public DateOnly? DataDoPedido { get; set; }
        public string? StatusDoPedido { get; set; }
        public long ValorTotal {  get; set; }
        public string? FormaDePagamento {  get; set; }
        [ForeignKey("Endereco")]
        public int EnderecoId {  get; set; }
        public Endereco? EnderecoDeEntrega { get; set; }
    }
}
