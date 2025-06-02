using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polimedica.Models
{
    [Keyless]
    public class CategoriaProduto
    {
        public int CategoriaId { get; set; }
        [ForeignKey("Categoria")]
        public Categoria? Categoria { get; set; }
        public int ProdutoId {  get; set; }
        public Produto? Produto { get; set; }
    }
}
