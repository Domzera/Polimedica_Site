using Microsoft.EntityFrameworkCore;
using Polimedica.Data;
using Polimedica.Interface;
using Polimedica.Models;

namespace Polimedica.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly PolimedicaDbContext _context;

        public ProdutoRepository(PolimedicaDbContext context)
        {
            _context = context;
        }
        public bool Add(Produto produto)
        {
            _context.Add(produto);
            return Save();
        }

        public bool Delete(Produto produto)
        {
            _context.Remove(produto);
            return Save();
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _context.ProdutoDb.ToListAsync();

        }

        public async Task<IEnumerable<Produto>> GetAllAtivo(char ativo)
        {
            return (IEnumerable<Produto>)await _context.ProdutoDb.FindAsync(ativo);
        }

        public Task<Produto> GetByNomeAsync(string nome)
        {
            return _context.ProdutoDb.FirstOrDefaultAsync(n => n.NomeProduto == nome);
        }

        public async Task<IEnumerable<Produto>> GetAllProdutoPorMarca(int marca)
        {
            return (IEnumerable<Produto>)await _context.ProdutoDb.FindAsync(marca);
        }

        public async Task<IEnumerable<Produto>> GetAllProdutoPorCategoria(int categoria)
        {
            /*      Usar QUERY para esta pesquisa - TALVES
             *  https://learn.microsoft.com/pt-br/aspnet/core/data/ef-mvc/advanced?view=aspnetcore-9.0
             *  Chamar uma consulta para outros tipos
             */
            return (IEnumerable<Produto>)await _context.ProdutoDb.FindAsync(categoria);
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            return await _context.ProdutoDb.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Produto produto)
        {
            throw new NotImplementedException();
        }
    }
}
