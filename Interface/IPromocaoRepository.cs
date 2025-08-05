using Polimedica.Models;

namespace Polimedica.Interface
{
    public interface IPromocaoRepository
    {
        Task<bool>Add(Promocao promocao);
        Task<bool> Update(Promocao promocao);
        Task<bool> Delete(Promocao promocao);
        //Task<bool> SaveAsync();
        Task<IEnumerable<Promocao>> GetAll();
    }
}
