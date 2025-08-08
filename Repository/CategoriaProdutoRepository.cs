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

        public async Task<bool> Add(CategoriaProduto categoriaProduto)
        {
            await _context.Database.ExecuteSqlRawAsync("INSERT INTO CategoriaProdutoDb (CategoriaId, ProdutoId) VALUES ({0}, {1})",
                categoriaProduto.CategoriaId, categoriaProduto.ProdutoId);
            return await Save();
        }
        public async Task<bool> UpdateByCategoriaId(CategoriaProduto categoriaProduto)
        {
            await _context.Database.ExecuteSqlRawAsync("UPDATE CategoriaProdutoDb SET ProdutoId = {0} WHERE CategoriaId = {1}",
                categoriaProduto.ProdutoId, categoriaProduto.CategoriaId);
            return await Save();
        }
        public async Task<bool> UpdateByProdutoId(CategoriaProduto categoriaProduto)
        {
            await _context.Database.ExecuteSqlRawAsync("UPDATE CategoriaProdutoDb SET CategoriaId = {0} WHERE ProdutoId = {1}",
                categoriaProduto.CategoriaId, categoriaProduto.ProdutoId);
            return await Save();
        }
        public async Task<bool> DeleteByCategoriaId(CategoriaProduto categoriaProduto)
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM CategoriaProdutoDb WHERE CategoriaId = {0}", categoriaProduto.CategoriaId);
            return await Save();
        }
        public async Task<bool> DeleteByProdutoId(int id)
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM CategoriaProdutoDb WHERE ProdutoId = {0}", id);
            return await Save();
        }
        public async Task<IEnumerable<CategoriaProduto>> GetByCategoriaId(int id)
        {
            return await _context.CategoriaProdutoDb
                .FromSqlRaw("SELECT * FROM CategoriaProdutoDb WHERE CategoriaId = {0}", id)
                .ToListAsync();
        }
        public async Task<IEnumerable<CategoriaProduto>> GetByProdutoId(int id)
        {
            return await _context.CategoriaProdutoDb
                .FromSqlRaw("SELECT * FROM CategoriaProdutoDb WHERE ProdutoId = {0}", id)
                .ToListAsync();
        }
        private async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<CategoriaProduto>> GetProdutoByCategoria(int id)
        {
            return await _context.CategoriaProdutoDb
                .FromSqlRaw("SELECT * FROM CategoriaProdutoDb WHERE CategoriaId = {0}", id)
                .ToListAsync();
        }
    }
}
