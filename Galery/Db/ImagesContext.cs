using System;
using Galery.Contracts;
using Galery.Models;
using MongoDB.Driver;

namespace Galery.Db;

public class ImagesContext
{
	protected IMongoCollection<Image> Images;

	public ImagesContext(DbSettings settings)
	{
		var client = new MongoClient(settings.ConnectionString);
		var db = client.GetDatabase(settings.Name);
		Images = db.GetCollection<Image>(nameof(Images));
	}
}

