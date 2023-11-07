using tadb_202320_ex04.DbContexts;
using tadb_202320_ex04.Models;
using System.Data;
using tadb_202320_ex04.Helpers;
using MongoDB.Driver;

namespace tadb_202320_ex04.Repositories;

public class CargadoresRepository
{
    private readonly MongoDbContext contextoDB;

    public CargadoresRepository(MongoDbContext unContexto)
    {
        contextoDB = unContexto;
    }

    public async Task<IEnumerable<Cargadores>> GetAllAsync()
    {
        var conexion = contextoDB.CreateConnection();
        var coleccionCargadores = conexion.GetCollection<Cargadores>(contextoDB.configuracionColecciones.ColeccionCargadores);

        var losCargadores = await coleccionCargadores
            .Find(_ => true)
            .SortBy(cargador => cargador.voltaje)
            .ToListAsync();

        return losCargadores;
    }

    public async Task<Cargadores> GetByIdAsync(string id_cargador)
    {
        Cargadores unCargador = new();

        var conexion = contextoDB.CreateConnection();
        var coleccionCargadores = conexion.GetCollection<Cargadores>(contextoDB.configuracionColecciones.ColeccionCargadores);

        var resultado = await coleccionCargadores
                .Find(cargador => cargador.id_cargador == id_cargador)
                .FirstOrDefaultAsync();

        if (resultado is not null)
        {
            unCargador = resultado;
        }

        return unCargador;
    }

    public async Task<Cargadores> GetBySerialAsync(string voltaje)
    {
        Cargadores unCargador = new();

        var conexion = contextoDB.CreateConnection();
        var coleccionCargadores = conexion.GetCollection<Cargadores>(contextoDB.configuracionColecciones.ColeccionCargadores);

        var resultado = await coleccionCargadores
            .Find(cargador => cargador.voltaje == voltaje)
            .FirstOrDefaultAsync();

        if (resultado is not null)
            unCargador = resultado;

        return unCargador;
    }

    public async Task<bool> CreateAsync(Cargadores unCargador)
    {
        bool resultadoAccion = false;

        var conexion = contextoDB.CreateConnection();
        var coleccionCargadores = conexion.GetCollection<Cargadores>(contextoDB.configuracionColecciones.ColeccionCargadores);

        await coleccionCargadores
            .InsertOneAsync(unCargador);

        var resultado = await coleccionCargadores
            .Find(cargador => cargador.voltaje == unCargador.voltaje)
            .FirstOrDefaultAsync();

        if (resultado is not null)
            resultadoAccion = true;

        return resultadoAccion;
    }

    public async Task<bool> UpdateAsync(Cargadores unCargador)
    {
        bool resultadoAccion = false;

        var conexion = contextoDB.CreateConnection();
        var coleccionCargadores = conexion.GetCollection<Cargadores>(contextoDB.configuracionColecciones.ColeccionCargadores);

        var resultado = await coleccionCargadores.ReplaceOneAsync(cargador => cargador.id_cargador == unCargador.id_cargador, unCargador);

        if (resultado.IsAcknowledged)
            resultadoAccion = true;

        return resultadoAccion;
    }

    public async Task<bool> DeleteAsync(Cargadores unCargador)
    {
        bool resultadoAccion = false;

        var conexion = contextoDB.CreateConnection();
        var coleccionCargadores = conexion.GetCollection<Cargadores>(contextoDB.configuracionColecciones.ColeccionCargadores);

        var resultado = await coleccionCargadores
            .DeleteOneAsync(cargador => cargador.id_cargador == unCargador.id_cargador);

        if (resultado.IsAcknowledged)
            resultadoAccion = true;

        return resultadoAccion;
    }
}



