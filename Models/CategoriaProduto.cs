using Microsoft.EntityFrameworkCore;

namespace Polimedica.Models
{
    [Keyless]
    public class CategoriaProduto
    {
        public int CategoriaId { get; set; }
        public int ProdutoId {  get; set; }
    }
}
