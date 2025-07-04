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
        public bool Add(MarcaProduto marcaProduto)
        {
            _context.Database.ExecuteSqlRawAsync("INSERT INTO MarcaProdutoDb (MarcaId, ProdutoId) VALUES ({0}, {1})",
                marcaProduto.MarcaId, marcaProduto.ProdutoId);
            return Save();
        }

        public bool DeleteByMarcaId(MarcaProduto marcaProduto)
        {
            _context.Database.ExecuteSqlRawAsync("DELETE FROM MarcaProdutoDb WHERE MarcaId = {0}", marcaProduto.MarcaId);
            return Save();
        }
        public bool DeleteByProdutoId(MarcaProduto marcaProduto)
        {
            _context.Database.ExecuteSqlRawAsync("DELETE FROM MarcaProdutoDb WHERE ProdutoId = {0}", marcaProduto.ProdutoId);
            return Save();
        }

        public async Task<IEnumerable<MarcaProduto>> getByMarcaId(MarcaProduto marcaProduto)
        {
            return (IEnumerable<MarcaProduto>)_context.MarcaProdutoDb
                .FromSqlRaw("SELECT MarcaId, ProdutoId FROM MarcaProdutoDb WHERE MarcaId > {0}", marcaProduto.MarcaId)
                .ToListAsync();
        }
        public async Task<IEnumerable<MarcaProduto>> getByProdutoId(MarcaProduto marcaProduto)
        {
            return (IEnumerable<MarcaProduto>)_context.MarcaProdutoDb
                .FromSqlRaw("SELECT MarcaId, ProdutoId FROM MarcaProdutoDb WHERE ProdutoId > {0}", marcaProduto.ProdutoId)
                .ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateByMarcaId(MarcaProduto marcaProduto)
        {
             _context.Database.ExecuteSqlRawAsync("UPDATE MarcaProdutoDb SET ProdutoId = {0} WHERE MarcaId = {1}",
                marcaProduto.ProdutoId, marcaProduto.MarcaId);
            return Save();
        }
        public bool UpdateByProdutoId(MarcaProduto marcaProduto)
        {
            _context.Database.ExecuteSqlRawAsync("UPDATE MarcaProdutoDb SET MarcaId = {0} WHERE ProdutoId = {1}",
               marcaProduto.MarcaId, marcaProduto.ProdutoId);
            return Save();
        }
    }
}
