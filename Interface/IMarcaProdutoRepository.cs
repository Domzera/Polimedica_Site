using Polimedica.Models;

namespace Polimedica.Interface
{
    public interface IMarcaProdutoRepository
    {
        Task<bool> Add(MarcaProduto marcaProduto);
        Task<bool> UpdateByMarcaId(MarcaProduto marcaProduto);
        Task<bool> UpdateByProdutoId(MarcaProduto marcaProduto);
        Task<bool> DeleteByMarcaId(int id);
        Task<bool> DeleteByProdutoId(int id);
        Task<IEnumerable<MarcaProduto>> getByMarcaId(int id);
        Task<IEnumerable<MarcaProduto>> getByProdutoId(int id);
    }
}
