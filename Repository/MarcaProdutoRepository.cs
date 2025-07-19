using Microsoft.EntityFrameworkCore;
using Polimedica.Data;
using Polimedica.Interface;
using Polimedica.Models;

namespace Polimedica.Repository
{
    public class MarcaProdutoRepository : IMarcaProdutoRepository
    {
        private readonly PolimedicaDbContext _context;
        public MarcaProdutoRepository(PolimedicaDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(MarcaProduto marcaProduto)
        {
             await _context.Database.ExecuteSqlRawAsync("INSERT INTO MarcaProdutoDb (MarcaId, ProdutoId) VALUES ({0}, {1})",
                marcaProduto.MarcaId, marcaProduto.ProdutoId);
            return await SaveAsync();
        }

        public async Task<bool> DeleteByMarcaId(int id)
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM MarcaProdutoDb WHERE MarcaId = {0}", id);
            return await SaveAsync();
        }
        public async Task<bool> DeleteByProdutoId(int id)
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM MarcaProdutoDb WHERE ProdutoId = {0}", id);
            return await SaveAsync();
        }

        public async Task<IEnumerable<MarcaProduto>> getByMarcaId(int id)
        {
            return await _context.MarcaProdutoDb
                .FromSqlRaw("SELECT * FROM MarcaProdutoDb WHERE MarcaId = {0}", id)
                .ToListAsync();
        }
        public async Task<IEnumerable<MarcaProduto>> getByProdutoId(int id)
        {
            return await _context.MarcaProdutoDb
                .FromSqlRaw("SELECT * FROM MarcaProdutoDb WHERE ProdutoId = {0}", id)
                .ToListAsync();
        }

        private async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateByMarcaId(MarcaProduto marcaProduto)
        {
             await _context.Database.ExecuteSqlRawAsync("UPDATE MarcaProdutoDb SET ProdutoId = {0} WHERE MarcaId = {1}",
                marcaProduto.ProdutoId, marcaProduto.MarcaId);
            return await SaveAsync();
        }
        public async Task<bool> UpdateByProdutoId(MarcaProduto marcaProduto)
        {
            await _context.Database.ExecuteSqlRawAsync("UPDATE MarcaProdutoDb SET MarcaId = {0} WHERE ProdutoId = {1}",
               marcaProduto.MarcaId, marcaProduto.ProdutoId);
            return await SaveAsync();
        }
    }
}
