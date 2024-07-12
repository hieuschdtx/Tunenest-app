using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using tunenest.Domain.Extensions;
using tunenest.Domain.Helpers;
using tunenest.Infrastructure.Options;

namespace tunenest.Application.Services.CloudinaryServices
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IOptions<CloudinaryOption> options)
        {
            var cloudinaryOption = options.Value;
            _cloudinary = new Cloudinary(new Account(
                cloudinaryOption.CloudName,
                cloudinaryOption.ApiKey,
                cloudinaryOption.ApiSecret));
        }

        public async Task<RawUploadResult> UploadImageAsync(IFormFile file, string folderName)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length <= 0) return uploadResult;

            await using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                PublicId =
                   $"{file.FileName[..file.FileName.LastIndexOf('.')].ConvertAlias()}_{DateTimeExtension.GetCurrentTimestampString()}",
                Folder = folderName,
                Overwrite = true,
                Transformation = new Transformation()
                   .Quality("auto")
                   .FetchFormat("auto")
            };

            uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return uploadResult;
        }
    }
}
