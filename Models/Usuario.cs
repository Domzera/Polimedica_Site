using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polimedica.Models
{
    public class Usuario : IdentityUser
    {
        public string? PrimeiroNome {  get; set; }
        public string? SobreNome {  get; set; }
        [ForeignKey("Endereco")]
        public int? EnderecoId {  get; set; }
        public Endereco? Endereco {  get; set; }
        public DateOnly DataDeCadastro { get; set; }
    }
}
