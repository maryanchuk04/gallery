using Galery.Contracts;
using Newtonsoft.Json;

namespace Galery.Services;

public class ImageUploader : IUploader
{
    private const string ApiKey = "4b76823349508cfe6987b62ea7b72eb8";
    private const string ApiUrl = "https://api.imgbb.com/1/upload";

    private readonly IImagesService _imagesService;

    public ImageUploader(IImagesService imagesService)
    {
        _imagesService = imagesService;
    }

    public async Task<string> UploadAsync(IFormFile file)
    {
        try
        {
            using var httpClient = new HttpClient();
            await using var stream = new MemoryStream();
            await file.CopyToAsync(stream);

            var fileBytes = stream.ToArray();
            var base64Image = Convert.ToBase64String(fileBytes);

            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(ApiKey), "key");
            formData.Add(new StringContent(base64Image), "image");

            var response = await httpClient.PostAsync(ApiUrl, formData);
            var responseContent = await response.Content.ReadAsStringAsync();

            var responseData = JsonConvert.DeserializeObject<Response>(responseContent);
            var imageUrl = responseData?.Data?.Url;

            if (!string.IsNullOrEmpty(imageUrl))
            {
                await _imagesService.InsertAsync(imageUrl);
                return imageUrl;
            }

            throw new Exception();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private class Response
    {
        public Data Data { get; set; }
    }

    private class Data
    {
        public string Url { get; set; }
    }
}