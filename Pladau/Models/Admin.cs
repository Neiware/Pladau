using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace Pladau.Models;

public class Admin:BaseModel
{

    public string username { get; set; } = null!;

    //[BsonElement("items")]
    public List<string> nicknames { get; set; } = null!;
}
