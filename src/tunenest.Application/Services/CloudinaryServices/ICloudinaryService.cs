using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace tunenest.Application.Services.CloudinaryServices
{
    public interface ICloudinaryService
    {
        Task<RawUploadResult> UploadImageAsync(IFormFile file, string folderName);
    }
}
