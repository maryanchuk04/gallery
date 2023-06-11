using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Galery.Models;

public class Image
{
    public Image(string id, string link, string category)
    {
        Id = id;
        Link = link;
        Category = category;
    }

    public Image(string link, string category)
    {
        Link = link;
        Category = category;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Link")]
    public string Link { get; set; }

    [BsonElement("Category")]
    public string Category { get; set; }
}

