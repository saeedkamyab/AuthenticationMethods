using Microsoft.AspNetCore.Http;
namespace AuthApi.Common
{
    public interface IFileUploader
    {
        string Upload(IFormFile file, string path);
    }
}
