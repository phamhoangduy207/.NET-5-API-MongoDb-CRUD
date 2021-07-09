using System;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
namespace BooksApi.Models
{
    public class Chapter
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string BookId { get; set; }
        public string ChapterName { get; set; }
        public int Page { get; set; }
    }
}