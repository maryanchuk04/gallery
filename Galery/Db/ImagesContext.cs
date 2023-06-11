using System;
using Galery.Contracts;
using Galery.Models;
using MongoDB.Driver;

namespace Galery.Db;

public class ImagesContext
{
	protected IMongoCollection<Image> _db;

	public ImagesContext(DbSettings settings)
	{
		var client = new MongoClient(settings.ConnectionString);
		var db = client.GetDatabase(settings.Name);
		_db = db.GetCollection<Image>(nameof(_db));
	}
}

