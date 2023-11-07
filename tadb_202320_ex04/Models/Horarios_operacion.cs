using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace tadb_202320_ex04.Models
{
    public class Horarios_operacion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id_hora")]
        public string? id_hora { get; set; } = string.Empty;

        [BsonElement("hora")]
        [BsonRepresentation(BsonType.Int32)]
        [JsonPropertyName("hora")]
        public int hora { get; set; } = -1;

        [BsonElement("pico")]
        [BsonRepresentation(BsonType.Boolean)]
        [JsonPropertyName("pico")]
        public bool pico { get; set; } = false;

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var otraHora = (Horarios_operacion)obj;

            return id_hora == otraHora.id_hora
                && pico.Equals(otraHora.pico);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 3;
                hash = hash * 5 + (id_hora?.GetHashCode() ?? 0);
                hash = hash * 5 + pico.GetHashCode();
                return hash;
            }
        }
    }
}
