using Microsoft.AspNetCore.Http;
namespace AuthAspRazorPages.Common
{
    public interface IFileUploader
    {
        string Upload(IFormFile file, string path);
    }
}
