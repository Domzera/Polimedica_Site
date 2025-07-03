using Microsoft.EntityFrameworkCore;

namespace Polimedica.Models
{
    [Keyless]
    public class MarcaProduto
    {
        public int MarcaId {  get; set; }
        public int ProdutoId {  get; set; }
    }
}
