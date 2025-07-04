using Polimedica.Models;

namespace Polimedica.Interface
{
    public interface ICategoriaProdutoRepository
    {
        bool Add(CategoriaProduto categoriaProduto);
        bool UpdateByCategoriaId(CategoriaProduto categoriaProduto);
        bool UpdateByProdutoId(CategoriaProduto categoriaProduto);
        bool DeleteByCategoriaId(CategoriaProduto categoriaProduto);
        bool DeleteByProdutoId(CategoriaProduto categoriaProduto);
        Task<IEnumerable<CategoriaProduto>> GetByCategoriaId(CategoriaProduto categoriaProduto);
        Task<IEnumerable<CategoriaProduto>> GetByProdutoId(CategoriaProduto categoriaProduto);
        bool Save();
    }
}
