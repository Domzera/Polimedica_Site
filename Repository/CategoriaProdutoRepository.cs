using Microsoft.EntityFrameworkCore;
using Polimedica.Data;
using Polimedica.Interface;
using Polimedica.Models;

namespace Polimedica.Repository
{
    public class CategoriaProdutoRepository : ICategoriaProdutoRepository
    {
        private readonly PolimedicaDbContext _context;
        public CategoriaProdutoRepository(PolimedicaDbContext context)
        {
            _context = context;
        }
        public bool Add(CategoriaProduto categoriaProduto)
        {
            _context.Database.ExecuteSqlRawAsync("INSERT INTO CategoriaProdutoDb (CategoriaId, ProdutoId) VALUES ({0}, {1})",
                categoriaProduto.CategoriaId, categoriaProduto.ProdutoId);
            return Save();
        }
        public bool UpdateByCategoriaId(CategoriaProduto categoriaProduto)
        {
            _context.Database.ExecuteSqlRawAsync("UPDATE CategoriaProdutoDb SET ProdutoId = {0} WHERE CategoriaId = {1}",
                categoriaProduto.ProdutoId, categoriaProduto.CategoriaId);
            return Save();
        }
        public bool UpdateByProdutoId(CategoriaProduto categoriaProduto)
        {
            _context.Database.ExecuteSqlRawAsync("UPDATE CategoriaProdutoDb SET CategoriaId = {0} WHERE ProdutoId = {1}",
                categoriaProduto.CategoriaId, categoriaProduto.ProdutoId);
            return Save();
        }
        public bool DeleteByCategoriaId(CategoriaProduto categoriaProduto)
        {
            _context.Database.ExecuteSqlRawAsync("DELETE FROM CategoriaProdutoDb WHERE CategoriaId = {0}", categoriaProduto.CategoriaId);
            return Save();
        }
        public bool DeleteByProdutoId(CategoriaProduto categoriaProduto)
        {
            _context.Database.ExecuteSqlRawAsync("DELETE FROM CategoriaProdutoDb WHERE ProdutoId = {0}", categoriaProduto.ProdutoId);
            return Save();
        }
        public async Task<IEnumerable<CategoriaProduto>> GetByCategoriaId(int id)
        {
            return (IEnumerable<CategoriaProduto>)_context.CategoriaProdutoDb
                .FromSqlRaw("SELECT * FROM CategoriaProdutoDb WHERE CategoriaId = {0}", id)
                .ToListAsync();
        }
        public async Task<IEnumerable<CategoriaProduto>> GetByProdutoId(int id)
        {
            return (List<CategoriaProduto>) await _context.CategoriaProdutoDb
                .FromSqlRaw("SELECT * FROM CategoriaProdutoDb WHERE ProdutoId = {0}", id)
                .ToListAsync();
        }
        public bool Save()
        {
            var saved = _context.SaveChangesAsync();
            return saved.IsCompletedSuccessfully && saved.Result > 0;
        }
    }
}
