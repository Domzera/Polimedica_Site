using Microsoft.EntityFrameworkCore;
using Polimedica.Data;
using Polimedica.Interface;
using Polimedica.Models;

namespace Polimedica.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly PolimedicaDbContext _context;

        public CategoriaRepository(PolimedicaDbContext context)
        {
            _context = context;
        }
        public bool Add(Categoria categoria)
        {
            _context.Add(categoria);
            return Save();
        }

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            return await _context.CategoriaDb.ToListAsync();
        }

        public async Task<Categoria> GetById(int id)
        {
            return await _context.CategoriaDb.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Categoria categoria)
        {
            throw new NotImplementedException();
        }
    }
}
