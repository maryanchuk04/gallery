using System;
using Galery.Models;

namespace Galery.Contracts;

public interface IImagesService
{
    Task<IEnumerable<Image>> GetAsync();

    Task<Image> GetAsync(string Id);

    Task DeleteAsync(string Id);

    Task InsertAsync(string Link);
}

