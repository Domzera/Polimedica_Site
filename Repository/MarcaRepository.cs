using Microsoft.EntityFrameworkCore;
using Polimedica.Data;
using Polimedica.Interface;
using Polimedica.Models;

namespace Polimedica.Repository
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly PolimedicaDbContext _context;

        public MarcaRepository(PolimedicaDbContext context)
        {
            _context = context;
        }
        public bool Add(Marca marca)
        {
            _context.Add(marca);
            return Save();
        }

        public async Task<IEnumerable<Marca>?> GetAllAsync()
        {
           var testa = await _context.MarcaDb.ToListAsync();

            if(testa != null)
            {
                return testa;
            }
            else
            {
                return null;
            }

        }

        public async Task<Marca> GetById(int id)
        {
            return await _context.MarcaDb.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Marca marca)
        {
            throw new NotImplementedException();
        }
    }
}
