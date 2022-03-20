using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace RetailManagementService.DataContext.Modal
{
    public class PriceReduction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string PriceReductionName { get; set; }
        public List<PriceReductionDayOfWeek> PriceReductionDayOfWeek { get; set; }
    }
}
