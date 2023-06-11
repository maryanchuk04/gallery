using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Galery.Models;

public class Image
{
    public Image(string id, string link)
    {
        Id = id;
        Link = link;
    }

    public Image(string link)
    {
        Link = link;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Link")]
    public string Link { get; set; }
}

