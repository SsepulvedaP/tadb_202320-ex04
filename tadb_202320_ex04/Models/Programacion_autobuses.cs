using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using System.Numerics;

namespace tadb_202320_ex04.Models
{
    public class Programacion_autobuses
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string? id { get; set; } = string.Empty;

        [BsonElement("hora")]
        [BsonRepresentation(BsonType.Int32)]
        [JsonPropertyName("hora")]
        public int hora { get; set; } = -1;

        [BsonElement("id_autobus")]
        [BsonRepresentation(BsonType.String)]
        [JsonPropertyName("id_autobus")]
        public string id_autobus { get; set; } = string.Empty;

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            var otraOperacion = (Programacion_autobuses)obj;

            return id == otraOperacion.id
                && hora == otraOperacion.hora
                && id_autobus == otraOperacion.id_autobus;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 3;
                hash = hash * 5 + (id?.GetHashCode() ?? 0);
                hash = hash * 5 + hora.GetHashCode();
                hash = hash * 5 + id_autobus.GetHashCode();

                return hash;
            }
        }
    }
}
