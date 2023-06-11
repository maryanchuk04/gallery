using System;
using Galery.Models;

namespace Galery.Contracts;

public interface IImagesService
{
    Task<IEnumerable<Image>> GetAsync();

    Task<Image> GetAsync(string id);

    Task DeleteAsync(string id);

    Task<Image> InsertAsync(string link, string category);
}

