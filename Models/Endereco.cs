using Polimedica.Data.Enum;

namespace Polimedica.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public string? NomeLogradouro {  get; set; }
        public int Numero {  get; set; }
        public string? Bairro {  get; set; }
        public string? Cidade {  get; set; }
        public Estados EstadoSigla { get; set; }
        public long CEP {  get; set; }
        public string? Pais {  get; set; }
    }
}
