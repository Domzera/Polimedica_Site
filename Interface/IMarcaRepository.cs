using Polimedica.Models;

namespace Polimedica.Interface
{
    public interface IMarcaRepository
    {
        bool Add(Marca marca);
        bool Update(Marca marca);
        bool Save();
        Task<Marca> GetById(int id);
        Task<IEnumerable<Marca>> GetAllAsync();
    }
}
