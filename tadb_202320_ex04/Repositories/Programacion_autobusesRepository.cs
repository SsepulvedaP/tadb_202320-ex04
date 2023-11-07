using tadb_202320_ex04.DbContexts;
using tadb_202320_ex04.Models;
using System.Data;
using tadb_202320_ex04.Helpers;
using MongoDB.Driver;

namespace tadb_202320_ex04.Repositories;

public class Programacion_autobusesRepository
{
    private readonly MongoDbContext contextoDB;

    public Programacion_autobusesRepository(MongoDbContext unContexto)
    {
        contextoDB = unContexto;
    }

    public async Task<IEnumerable<Programacion_autobuses>> GetAllAsync()
    {
        var conexion = contextoDB.CreateConnection();
        var coleccionOperaciones = conexion.GetCollection<Programacion_autobuses>(contextoDB.configuracionColecciones.ColeccionProgramacion_autobuses);

        var lasOperaciones = await coleccionOperaciones
            .Find(_ => true)
            .SortBy(operacion => operacion.hora)
            .ToListAsync();

        return lasOperaciones;
    }

    public async Task<Programacion_autobuses> GetByIdAsync(string id)
    {
        Programacion_autobuses unaProgramacion = new();

        var conexion = contextoDB.CreateConnection();
        var coleccionOperaciones = conexion.GetCollection<Programacion_autobuses>(contextoDB.configuracionColecciones.ColeccionProgramacion_autobuses);

        var resultado = await coleccionOperaciones
                .Find(operacion => operacion.id == id)
                .FirstOrDefaultAsync();

        if (resultado is not null)
        {
            unaProgramacion = resultado;
        }

        return unaProgramacion;
    }
    public async Task<Programacion_autobuses> GetByHourAsync(int hora)
    {
        Programacion_autobuses unaProgramacion = new();

        var conexion = contextoDB.CreateConnection();
        var coleccionOperaciones = conexion.GetCollection<Programacion_autobuses>(contextoDB.configuracionColecciones.ColeccionProgramacion_autobuses);

        var resultado = await coleccionOperaciones
            .Find(operacion => operacion.hora == hora)
            .FirstOrDefaultAsync();

        if (resultado is not null)
        {
            unaProgramacion = resultado;
        }

        return unaProgramacion;

    }

    public async Task<bool> BusIsAvailableAtHour(int hora, string id_autobus)
    {
        bool resultadoAccion = false;

        var conexion = contextoDB.CreateConnection();
        var coleccionOperaciones = conexion.GetCollection<Programacion_autobuses>(contextoDB.configuracionColecciones.ColeccionProgramacion_autobuses);

        var builder = Builders<Programacion_autobuses>.Filter;
        var filtro = builder.And(
            builder.Eq(operacion => operacion.hora, hora),
            builder.Eq(operacion => operacion.id_autobus, id_autobus));

        var resultado = await coleccionOperaciones
            .Find(filtro)
            .FirstOrDefaultAsync();

        if (resultado is not null)
        {
            resultadoAccion = true;
        }

        return resultadoAccion;
    }

    public async Task<bool> CreateAsync(Programacion_autobuses unaProgramacion)
    {
        bool resultadoAccion = false;

        var conexion = contextoDB.CreateConnection();
        var coleccionOperaciones = conexion.GetCollection<Programacion_autobuses>(contextoDB.configuracionColecciones.ColeccionProgramacion_autobuses);

        await coleccionOperaciones
            .InsertOneAsync(unaProgramacion);

        var builder = Builders<Programacion_autobuses>.Filter;
        var filtro = builder.And(
            builder.Eq(operacion => operacion.hora, unaProgramacion.hora),
            builder.Eq(operacion => operacion.id_autobus, unaProgramacion.id_autobus));


        var resultado = await coleccionOperaciones
            .Find(filtro)
            .FirstOrDefaultAsync();

        if (resultado is not null)
        {
            resultadoAccion = true;
        }
        return resultadoAccion;
    }
    public async Task<bool> UpdateAsync(Programacion_autobuses unaProgramacion)
    {
        bool resultadoAccion = false;

        var conexion = contextoDB.CreateConnection();
        var coleccionOperaciones = conexion.GetCollection<Programacion_autobuses>(contextoDB.configuracionColecciones.ColeccionProgramacion_autobuses);

        var resultado = await coleccionOperaciones
            .ReplaceOneAsync(operacion => operacion.id == unaProgramacion.id, unaProgramacion);

        if (resultado.IsAcknowledged)
        {
            resultadoAccion = false;
        }
        return resultadoAccion;
    }

    public async Task<bool> DeleteAsync(Programacion_autobuses unaProgramacion)
    {
        bool resultadoAccion = false;

        var conexion = contextoDB.CreateConnection();
        var coleccionOperaciones = conexion.GetCollection<Programacion_autobuses>(contextoDB.configuracionColecciones.ColeccionProgramacion_autobuses);

        var resultado = await coleccionOperaciones
            .DeleteOneAsync(operacion => operacion.id == unaProgramacion.id);

        if (resultado.IsAcknowledged)
        {
            resultadoAccion = true;
        }

        return resultadoAccion;
    }
}
