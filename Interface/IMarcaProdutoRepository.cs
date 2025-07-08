using Polimedica.Models;

namespace Polimedica.Interface
{
    public interface IMarcaProdutoRepository
    {
        bool Add(MarcaProduto marcaProduto);
        bool UpdateByMarcaId(MarcaProduto marcaProduto);
        bool UpdateByProdutoId(MarcaProduto marcaProduto);
        bool DeleteByMarcaId(MarcaProduto marcaProduto);
        bool DeleteByProdutoId(MarcaProduto marcaProduto);
        Task<IEnumerable<MarcaProduto>> getByMarcaId(int id);
        Task<IEnumerable<MarcaProduto>> getByProdutoId(int id);
        bool Save();
    }
}
