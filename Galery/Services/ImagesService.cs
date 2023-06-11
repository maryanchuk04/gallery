using Galery.Contracts;
using Galery.Db;
using Galery.Exceptions;
using Galery.Models;
using MongoDB.Driver;

namespace Galery.Services;

public class ImagesService : ImagesContext, IImagesService
{
    private readonly DbSettings settings;

    public ImagesService(DbSettings settings) : base(settings) {}

    public async Task DeleteAsync(string id)
    {
        await Images.DeleteOneAsync(img => img.Id == id);
    }

    public async Task<IEnumerable<Image>> GetAsync()
        => await Images.Find(img => true).ToListAsync();


    public async Task<Image> GetAsync(string id)
    {
        return await Images
            .Find(img => img.Id == id)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(id);
    }

    public async Task<Image> InsertAsync(string link, string category)
    {
        if (string.IsNullOrWhiteSpace(link))
            throw new ArgumentException();

        await Images.InsertOneAsync(new Image(link, category));

        return await Images.Find(x => x.Link == link).FirstOrDefaultAsync();
    }
}

