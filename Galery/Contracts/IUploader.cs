namespace Galery.Contracts;

public interface IUploader
{
    Task<string> UploadAsync(IFormFile file);
}