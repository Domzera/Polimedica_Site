using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Polimedica.Data;
using Polimedica.Interface;

namespace Polimedica.Services
{
    public class PhotoServie : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoServie(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret);
            _cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = _cloudinary.Upload(uploadParams);
            }
            return await Task.FromResult(uploadResult);
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            return await _cloudinary.DestroyAsync(deleteParams);
        }


        public async Task<GetResourceResult> GetResource(string url)
        {
            var getResource = new GetResourceParams(url)
            {
                Type = ResourceType.Image.ToString()
            };
            return await _cloudinary.GetResourceAsync(getResource);
        }
    }
}
