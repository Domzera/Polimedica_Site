using CloudinaryDotNet.Actions;

namespace Polimedica.Interface
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddBannerAsync(IFormFile file);
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
        Task<GetResourceResult> GetResource(string url);
    }
}
