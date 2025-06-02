using Polimedica.Models;

namespace Polimedica.Interface
{
    public interface ICategoriaRepository
    {
        bool Add(Categoria categoria);
        bool Update(Categoria categoria);
        bool Save();
        Task<Categoria> GetById(int id);
        Task<IEnumerable<Categoria>> GetAllAsync();
    }
}
