using Microsoft.EntityFrameworkCore;
using Polimedica.Data;
using Polimedica.Interface;
using Polimedica.Models;

namespace Polimedica.Repository
{
    public class TerEmCasaRepository : ITerEmCasaRepository
    {
        private readonly PolimedicaDbContext _context;

        public TerEmCasaRepository(PolimedicaDbContext context)
        {
            _context = context;
        }

        public Task<bool> Add(TerEmCasa terEmCasa)
        {
            _context.Add(terEmCasa);
            return SaveAsync();
        }

        public Task<bool> Delete(TerEmCasa terEmCasa)
        {
            _context.Remove(terEmCasa);
            return SaveAsync();
        }

        public async Task<IEnumerable<TerEmCasa>> GetAll()
        {
            return await _context.TerEmCasaDb.ToListAsync();
        }

        public Task<bool> Update(TerEmCasa terEmCasa)
        {
            _context.Update(terEmCasa);
            return SaveAsync();
        }

        private async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
