using Polimedica.Models;

namespace Polimedica.Interface
{
    public interface IUsuarioRepository
    {
        bool Add(Usuario usuario);
        bool Update(Usuario usuario);
        bool Delete(Usuario usuario);
        bool Save();
        Task<Usuario> GetById(int id);
        Task<Usuario> GetByName(string name);
        //Task<Usuario> GetByRole(string role);
    }
}
