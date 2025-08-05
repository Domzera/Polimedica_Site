using Microsoft.EntityFrameworkCore;
using Polimedica.Data;
using Polimedica.Interface;
using Polimedica.Models;

namespace Polimedica.Repository
{
    public class BannerRepository : IBannerRepository
    {
        private readonly PolimedicaDbContext _context;
        public BannerRepository(PolimedicaDbContext context)
        {
            _context = context;
        }

        public PolimedicaDbContext Context { get; }


        public async Task<bool> Add(Banner banner)
        {

            _context.Add(banner);
            return await Save();
        }

        public async Task<bool> Update(Banner banner)
        {
            _context.Update(banner);
            return await Save();
        }

        public async Task<Banner> GetBanner(int id)
        {
            return await _context.BannerDb.FirstOrDefaultAsync(b => b.Id == id);
        }

        private async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
