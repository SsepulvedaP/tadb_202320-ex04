using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using System.Numerics;
using System.Text.RegularExpressions;


namespace tadb_202320_ex04.Models
{
    public class Cargadores
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id_cargador")]
        public string? id_cargador { get; set; } = string.Empty;

        [BsonElement("voltaje")]
        [BsonRepresentation(BsonType.String)]
        [JsonPropertyName("voltaje")]
        public string voltaje { get; set; } = string.Empty;

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            var otroCargador = (Cargadores)obj;

            return id_cargador == otroCargador.id_cargador
                && voltaje == otroCargador.voltaje;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 3;
                hash = hash * 5 + (id_cargador?.GetHashCode() ?? 0);
                hash = hash * 5 + (voltaje?.GetHashCode() ?? 0);
                return hash;
            }
        }
    }
}
