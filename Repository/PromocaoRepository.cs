using Microsoft.EntityFrameworkCore;
using Polimedica.Data;
using Polimedica.Interface;
using Polimedica.Models;

namespace Polimedica.Repository
{
    public class PromocaoRepository : IPromocaoRepository
    {
        private readonly PolimedicaDbContext _context;

        public PromocaoRepository(PolimedicaDbContext context)
        {
            _context = context;
        }
        public Task<bool> Add(Promocao promocao)
        {
            _context.Add(promocao);
            return SaveAsync();
        }

        public Task<bool> Delete(Promocao promocao)
        {
            _context.Remove(promocao);
            return SaveAsync();
        }

        public async Task<IEnumerable<Promocao>> GetAll()
        {
            return await _context.PromocaoDb.ToListAsync();
        }

        public Task<bool> Update(Promocao promocao)
        {
            _context.Update(promocao);
            return SaveAsync();
        }
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
