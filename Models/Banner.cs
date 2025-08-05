using Microsoft.EntityFrameworkCore;

namespace Polimedica.Models
{
    public class Banner
    {
        public int Id { get; set; }
        public string? Banner1 { get; set; }
        public string? Banner2 { get; set; }
        public string? Banner3 { get; set; }
    }
}
