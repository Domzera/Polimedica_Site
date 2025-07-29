using Polimedica.Models;

namespace Polimedica.Interface
{
    public interface ICategoriaProdutoRepository
    {
        Task<bool> Add(CategoriaProduto categoriaProduto);
        Task<bool> UpdateByCategoriaId(CategoriaProduto categoriaProduto);
        Task<bool> UpdateByProdutoId(CategoriaProduto categoriaProduto);
        Task<bool> DeleteByCategoriaId(CategoriaProduto categoriaProduto);
        Task<bool> DeleteByProdutoId(int id);
        Task<IEnumerable<CategoriaProduto>> GetByCategoriaId(int id);
        Task<IEnumerable<CategoriaProduto>> GetByProdutoId(int id);
    }
}
