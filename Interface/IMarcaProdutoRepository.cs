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
        Task<IEnumerable<MarcaProduto>> getByMarcaId(MarcaProduto marcaProduto);
        Task<IEnumerable<MarcaProduto>> getByProdutoId(MarcaProduto marcaProduto);
        bool Save();
    }
}
