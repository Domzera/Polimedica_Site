using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polimedica.Models
{
    [Keyless]
    public class ProductCategoria
    {
        [ForeignKey("Produto")]
        public int ProdutoId { get; set; }
        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }
    }
}
