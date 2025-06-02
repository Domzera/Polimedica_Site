using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polimedica.Models
{
    [Keyless]
    public class ProductMarca
    {
        [ForeignKey("Produto")]
        public int ProductId { get; set; }
        [ForeignKey("Marca")]
        public int MarcaId { get; set; }
    }
}
