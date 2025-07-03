namespace Polimedica.Models
{
    public class Avaliacao
    {
        public Guid Id { get; set; }
        public int? ProdutoId {  get; set; }
        public string? UsuarioId {  get; set; }
        public char Nota {  get; set; }
        public string? Comentario {  get; set; }
        public DateOnly DataAvaliacao { get; set; }
    }
}
