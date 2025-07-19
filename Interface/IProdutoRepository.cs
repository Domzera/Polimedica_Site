using Polimedica.Models;

namespace Polimedica.Interface
{
    public interface IProdutoRepository
    {
        Task<bool> Add(Produto produto);
        Task<bool> Update(Produto produto);
        Task<bool> Delete(Produto produto);
        //Task<bool> SaveAsync();
        Task<IEnumerable<Produto>> GetAll();
        Task<IEnumerable<Produto>> GetAllAtivo(char ativo);
        Task<IEnumerable<Produto>> GetAllProdutoPorMarca(int marcaId);
        Task<IEnumerable<Produto>> GetAllProdutoPorCategoria(int categoriaId);
        Task<Produto> GetById(int id);
        Task<Produto> GetByNomeAsync(string nome);
    }
}
