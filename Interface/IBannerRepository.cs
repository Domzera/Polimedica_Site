using Polimedica.Models;

namespace Polimedica.Interface
{
    public interface IBannerRepository
    {
        Task<bool> Add(Banner banner);
        Task<bool> Update(Banner banner);
        Task<Banner> GetBanner(int id);
    }
}
