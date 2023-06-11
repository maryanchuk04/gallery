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

    public async Task DeleteAsync(string Id)
    {
        await _db.DeleteOneAsync(img => img.Id == Id);
    }

    public async Task<IEnumerable<Image>> GetAsync()
        => await _db.Find(img => true).ToListAsync();


    public async Task<Image> GetAsync(string Id)
    {
        return await _db
            .Find(img => img.Id == Id)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(Id);
    }

    public async Task InsertAsync(string Link)
    {
        if (string.IsNullOrWhiteSpace(Link))
            throw new ArgumentException();

        await _db.InsertOneAsync(new Image(Link));
    }
}

