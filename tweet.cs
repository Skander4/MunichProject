using System;
using System.Collections.Generic;
using System.Text;


using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MunichService
{
    public class tweet
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("tweet")]
        public string Tweet { get; set; }

    }
}

