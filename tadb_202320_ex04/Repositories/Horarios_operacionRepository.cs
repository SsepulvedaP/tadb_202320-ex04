using tadb_202320_ex04.DbContexts;
using tadb_202320_ex04.Models;
using System.Data;
using tadb_202320_ex04.Helpers;
using MongoDB.Driver;

namespace tadb_202320_ex04.Repositories
{
    public class Horarios_operacionRepository
    {
        private readonly MongoDbContext contextoDB;

        public Horarios_operacionRepository(MongoDbContext unContexto)
        {
            contextoDB = unContexto;
        }

        public async Task<IEnumerable<Horarios_operacion>> GetAllAsync()
        {
            var conexion = contextoDB.CreateConnection();
            var coleccionHoras = conexion.GetCollection<Horarios_operacion>(contextoDB.configuracionColecciones.ColeccionHorarios_operacion);

            var lasHoras = await coleccionHoras
                .Find(_ => true)
                .SortBy(hora => hora.hora)
                .ToListAsync();

            return lasHoras;
        }

        public async Task<Horarios_operacion> GetByIdAsync(int id_hora)
        {

            Horarios_operacion hora = new();
            var conexion = contextoDB.CreateConnection();
            var coleccionHoras = conexion.GetCollection<Horarios_operacion>(contextoDB.configuracionColecciones.ColeccionHorarios_operacion);

            var resultado = await coleccionHoras.Find(hora => hora.hora == id_hora).FirstOrDefaultAsync();

            if (resultado is not null)
            {
                hora = resultado;
            }

            return hora;
        }

        public async Task<bool> CreateAsync(Horarios_operacion hora)
        {
            bool resultadoAccion = false;

            var conexion = contextoDB.CreateConnection();
            var coleccionHoras = conexion.GetCollection<Horarios_operacion>(contextoDB.configuracionColecciones.ColeccionHorarios_operacion);

            await coleccionHoras.InsertOneAsync(hora);

            var resultado = await GetByIdAsync(hora.hora);


            if (resultado is not null)
            {
                resultadoAccion = true;
            }

            return resultadoAccion;
        }


    }
}