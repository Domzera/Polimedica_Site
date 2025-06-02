using System.ComponentModel.DataAnnotations.Schema;

namespace Polimedica.Models
{
    public class Avaliacao
    {
        public Guid Id { get; set; }
        [ForeignKey("Produto")]
        public int? ProdutoId {  get; set; }
        public Produto? Produto { get; set; }
        [ForeignKey("Usuario")]
        public string? UsuarioId {  get; set; }
        public Usuario? Usuario { get; set; }
        public char Nota {  get; set; }
        public string? Comentario {  get; set; }
        public DateOnly DataAvaliacao { get; set; }
    }
}
