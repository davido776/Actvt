using System.Threading.Tasks;
using Actvt.Application.Photos;
using Microsoft.AspNetCore.Http;

namespace Actvt.Application.Interfaces
{
    public interface IPhotoAccessor
    {
        Task<PhotoUploadResult> AddPhoto(IFormFile file);
        Task<string> DeletePhoto(string publicId);
    }
}