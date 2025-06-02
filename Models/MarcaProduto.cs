namespace Polimedica.Models
{
    public class MarcaProduto
    {
        public int Id { get; set; }
        public int MarcaId {  get; set; }
        public Marca? Marca { get; set; }
        public int ProdutoId {  get; set; }
        public Produto? Produto { get; set; }
    }
}
