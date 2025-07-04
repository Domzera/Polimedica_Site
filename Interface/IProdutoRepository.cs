using Polimedica.Models;

namespace Polimedica.Interface
{
    public interface IProdutoRepository
    {
        bool Add(Produto produto);
        bool Update(Produto produto);
        bool Delete(Produto produto);
        bool Save();
        Task<IEnumerable<Produto>> GetAll();
        Task<IEnumerable<Produto>> GetAllAtivo(char ativo);
        Task<IEnumerable<Produto>> GetAllProdutoPorMarca(int marcaId);
        Task<IEnumerable<Produto>> GetAllProdutoPorCategoria(int categoriaId);
        Task<Produto> GetById(int id);
        Task<Produto> GetByNomeAsync(string nome);
    }
}
