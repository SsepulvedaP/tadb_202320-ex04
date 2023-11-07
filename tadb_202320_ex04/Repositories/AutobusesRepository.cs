using MongoDB.Driver;
using tadb_202320_ex04.DbContexts;
using tadb_202320_ex04.Models;

namespace tadb_202320_ex04.Repositories;

public class AutobusesRepository
{
    private readonly MongoDbContext contextoDB;

    public AutobusesRepository(MongoDbContext unContexto)
    {
        contextoDB = unContexto;
    }

    //Traer todos los buses
    public async Task<IEnumerable<Autobuses>> GetAllAsync()
    {
        var conexion = contextoDB.CreateConnection();
        var coleccionBuses = conexion.GetCollection<Autobuses>(contextoDB.configuracionColecciones.ColeccionBuses);

        var losBuses = await coleccionBuses
            .Find(_ => true)
            .SortBy(bus => bus.Marca)
            .ToListAsync();

        return losBuses;
    }

    //Traer bus por id
    public async Task<Autobuses> GetByIdAsync(string id_autobus)
    {
        Autobuses autobus = new();
        var conexion = contextoDB.CreateConnection();
        var coleccionBuses = conexion.GetCollection<Autobuses>(contextoDB.configuracionColecciones.ColeccionBuses);

        var resultado = await coleccionBuses.Find(bus => bus.id_autobus == id_autobus).FirstOrDefaultAsync();

        if (resultado is not null)
        {
            autobus = resultado;
        }

        return autobus;
    }

    //Traer bus por marca
    public async Task<Autobuses> GetByPlacaAsync(string marca)
    {
        Autobuses autobus = new();
        var conexion = contextoDB.CreateConnection();
        var coleccionBuses = conexion.GetCollection<Autobuses>(contextoDB.configuracionColecciones.ColeccionBuses);

        var resultado = await coleccionBuses.Find(bus => bus.Marca == marca).FirstOrDefaultAsync();

        if (resultado is not null)
        {
            autobus = resultado;
        }

        return autobus;
    }

    //Para crear un bus
    public async Task<bool> CreateAsync(Autobuses autobus)
    {
        bool resultadoAccion = false;

        var conexion = contextoDB.CreateConnection();
        var coleccionBuses = conexion.GetCollection<Autobuses>(contextoDB.configuracionColecciones.ColeccionBuses);

        await coleccionBuses.InsertOneAsync(autobus);

        var resultado = await GetByPlacaAsync(autobus.Marca);

        if (resultado is not null)
        {
            resultadoAccion = true;
        }

        return resultadoAccion;
    }

    //Para actualizar un autobus
    public async Task<bool> UpdateAsync(Autobuses autobus)
    {
        bool resultadoAccion = false;

        var conexion = contextoDB.CreateConnection();
        var coleccionBuses = conexion.GetCollection<Autobuses>(contextoDB.configuracionColecciones.ColeccionBuses);

        var resultado = await coleccionBuses.ReplaceOneAsync(bus => bus.id_autobus == autobus.id_autobus, autobus);

        if (resultado.IsAcknowledged)
        {
            resultadoAccion = true;
        }

        return resultadoAccion;

    }

    //Para eliminar un autobus
    public async Task<bool> DeleteAsync(Autobuses autobus)
    {
        bool resultadoAccion = false;

        var conexion = contextoDB.CreateConnection();
        var coleccionBuses = conexion.GetCollection<Autobuses>(contextoDB.configuracionColecciones.ColeccionBuses);

        var resultado = await coleccionBuses.DeleteOneAsync(bus => bus.id_autobus == autobus.id_autobus);

        if (resultado.IsAcknowledged)
        {
            resultadoAccion = true;
        }
        return resultadoAccion;
    }

}


