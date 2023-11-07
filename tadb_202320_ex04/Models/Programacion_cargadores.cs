using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace tadb_202320_ex04.Models
{
    public class Programacion_cargadores
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("_id")]
        public string? id { get; set; } = string.Empty;

        [BsonElement("hora")]
        [BsonRepresentation(BsonType.Int32)]
        [JsonPropertyName("hora")]
        public int hora { get; set; } = -1;

        [BsonElement("id_autobus")]
        [BsonRepresentation(BsonType.String)]
        [JsonPropertyName("id_autobus")]
        public string id_autobus { get; set; } = string.Empty;

        [BsonElement("id_cargador")]
        [BsonRepresentation(BsonType.String)]
        [JsonPropertyName("id_cargador")]
        public string id_cargador { get; set; } = string.Empty;

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var otraUtilizacion = (Programacion_cargadores)obj;

            return hora == otraUtilizacion.hora
                && id_autobus == otraUtilizacion.id_autobus
                && id_cargador == otraUtilizacion.id_cargador;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 3;
                hash = hash * 5 + hora.GetHashCode();
                hash = hash * 5 + id_autobus.GetHashCode();
                hash = hash * 5 + id_cargador.GetHashCode();

                return hash;
            }
        }
    }
}
