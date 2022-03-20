using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace RetailManagementService.DataContext.Modal
{
    public class RetailProduct
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime EntryDate { get; set; }
        public double Price { get; set; }
    }
}
