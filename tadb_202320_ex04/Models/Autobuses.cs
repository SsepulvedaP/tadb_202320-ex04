using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace tadb_202320_ex04.Models
{
    public class Autobuses
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("Id del bus:")]
        public string? id_autobus { get; set; } = string.Empty;

        [BsonElement("Marca")]
        [BsonRepresentation(BsonType.String)]
        [JsonPropertyName("Marca")]
        public string Marca { get; set; } = string.Empty;

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            var otroBus = (Autobuses)obj;

            return id_autobus == otroBus.id_autobus && Marca == otroBus.Marca;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 3;
                hash = hash * 5 + (id_autobus?.GetHashCode() ?? 0);
                hash = hash * 5 + (Marca?.GetHashCode() ?? 0);
                return hash;
            }
        }
    }
}
