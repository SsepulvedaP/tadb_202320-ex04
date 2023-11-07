using MongoDB.Driver;
using tadb_202320_ex04.DbContexts;
using tadb_202320_ex04.Models;

namespace tadb_202320_ex04.Repositories;

public class Programacion_cargadoresRepository
{
    private readonly MongoDbContext contextoDB;

    public Programacion_cargadoresRepository(MongoDbContext unContexto)
    {
        contextoDB = unContexto;
    }

    //Trae todas las Programacion_cargadoreses
    public async Task<IEnumerable<Programacion_cargadores>> GetAllAsync()
    {
        var conexion = contextoDB.CreateConnection();
        var coleccionProgramacion_cargadoreses = conexion.GetCollection<Programacion_cargadores>(contextoDB.configuracionColecciones.ColeccionProgramacion_cargadores);

        var losBuses = await coleccionProgramacion_cargadoreses
            .Find(_ => true)
            .SortBy(Programacion_cargadores => Programacion_cargadores.hora)
            .ToListAsync();

        return losBuses;
    }

    //Trae utilización por id
    public async Task<Programacion_cargadores> GetByIdAsync(string id_Programacion_cargadores)
    {
        Programacion_cargadores unaProgramacion_cargadores = new();
        var conexion = contextoDB.CreateConnection();
        var coleccionProgramacion_cargadoreses = conexion.GetCollection<Programacion_cargadores>(contextoDB.configuracionColecciones.ColeccionProgramacion_cargadores);

        var resultado = await coleccionProgramacion_cargadoreses.Find(Programacion_cargadores => Programacion_cargadores.id == id_Programacion_cargadores).FirstOrDefaultAsync();

        if (resultado is not null)
        {
            unaProgramacion_cargadores = resultado;
        }

        return unaProgramacion_cargadores;
    }




    //Permite crear una utilización
    public async Task<bool> CreateAsync(Programacion_cargadores unaProgramacion_cargadores)
    {
        bool resultadoAccion = false;

        var conexion = contextoDB.CreateConnection();
        var coleccionProgramacion_cargadoreses = conexion.GetCollection<Programacion_cargadores>(contextoDB.configuracionColecciones.ColeccionProgramacion_cargadores);

        await coleccionProgramacion_cargadoreses.InsertOneAsync(unaProgramacion_cargadores);

        var resultado = await GetByIdAsync(unaProgramacion_cargadores.id);

        if (resultado is not null)
        {
            resultadoAccion = true;
        }

        return resultadoAccion;
    }


    //Para actualizar una utilización
    public async Task<bool> UpdateAsync(Programacion_cargadores unaProgramacion_cargadores)
    {

        bool resultadoAccion = false;

        var conexion = contextoDB.CreateConnection();
        var coleccionProgramacion_cargadoreses = conexion.GetCollection<Programacion_cargadores>(contextoDB.configuracionColecciones.ColeccionProgramacion_cargadores);

        var resultado = await coleccionProgramacion_cargadoreses.ReplaceOneAsync(Programacion_cargadores => Programacion_cargadores.id == unaProgramacion_cargadores.id, unaProgramacion_cargadores);

        if (resultado.IsAcknowledged)
        {
            resultadoAccion = true;
        }

        return resultadoAccion;
    }

    //Eliminar utilización
    public async Task<bool> DeleteAsync(Programacion_cargadores unaProgramacion_cargadores)
    {

        bool resultadoAccion = false;

        var conexion = contextoDB.CreateConnection();
        var coleccionProgramacion_cargadoreses = conexion.GetCollection<Programacion_cargadores>(contextoDB.configuracionColecciones.ColeccionProgramacion_cargadores);

        var resultado = await coleccionProgramacion_cargadoreses.DeleteOneAsync(Programacion_cargadores => Programacion_cargadores.hora == unaProgramacion_cargadores.hora);

        if (resultado.IsAcknowledged)
        {
            resultadoAccion = true;
        }
        return resultadoAccion;
    }

    //Dice si un bus está disponible a una hora
    public async Task<bool> BusIsAvailableAtHour(int id_hora, string id_autobus)
    {
        bool disponible = false;
        var conexion = contextoDB.CreateConnection();
        var coleccionProgramacion_cargadoreses = conexion.GetCollection<Programacion_cargadores>(contextoDB.configuracionColecciones.ColeccionProgramacion_cargadores);

        var resultado = await coleccionProgramacion_cargadoreses.Find(Programacion_cargadores => Programacion_cargadores.id_autobus == id_autobus && Programacion_cargadores.hora == id_hora).FirstOrDefaultAsync();

        if (resultado is null)
        {
            disponible = true;
        }

        return disponible;
    }

    //Dice si un cargador está disponible a una hora
    public async Task<bool> CargadorAvailableAtHour(string id_cargador, int id_hora)
    {
        bool disponible = false;
        var conexion = contextoDB.CreateConnection();
        var coleccionProgramacion_cargadoreses = conexion.GetCollection<Programacion_cargadores>(contextoDB.configuracionColecciones.ColeccionProgramacion_cargadores);

        var resultado = await coleccionProgramacion_cargadoreses.Find(Programacion_cargadores => Programacion_cargadores.id_cargador == id_cargador && Programacion_cargadores.hora == id_hora).FirstOrDefaultAsync();

        if (resultado is null)
        {
            disponible = true;
        }

        return disponible;
    }
}
