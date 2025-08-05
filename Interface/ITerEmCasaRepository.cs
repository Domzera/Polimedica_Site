using Polimedica.Models;

namespace Polimedica.Interface
{
    public interface ITerEmCasaRepository
    {
        Task<bool> Add(TerEmCasa terEmCasa);
        Task<bool> Update(TerEmCasa terEmCasa);
        Task<bool> Delete(TerEmCasa terEmCasa);
        Task<IEnumerable<TerEmCasa>> GetAll();
        
    }
}
