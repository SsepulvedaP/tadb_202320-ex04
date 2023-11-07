using tadb_202320_ex04.Helpers;
using tadb_202320_ex04.Models;
using tadb_202320_ex04.Repositories;

namespace tadb_202320_ex04.Services
{
    public class Programacion_autobusesService
    {
        //Variable Repository para hacer las Programacion_autobuseses CRUD a Programacion_autobuseses
        private readonly Programacion_autobusesRepository programacion_AutobusesRepository;

        //Constructor de Programacion_autobusesService
        public Programacion_autobusesService(Programacion_autobusesRepository repository)
        {
            programacion_AutobusesRepository = repository;
        }

        //Métodos validadores
        public async Task<IEnumerable<Programacion_autobuses>> GetAllProgramacion_autobusesesAsync()
        {
            try
            {
                return await programacion_AutobusesRepository
                    .GetAllAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Programacion_autobuses> GetByIdAsync(string id)
        {
            //Validamos que la Programacion del autobus exista con ese id
            var unaProgramacion_autobuses = await programacion_AutobusesRepository
                .GetByIdAsync(id);

            if (string.IsNullOrEmpty(unaProgramacion_autobuses.id))
                throw new AppValidationException($"Programacion del autobus no encontrada con el id {id}");

            return unaProgramacion_autobuses;
        }

        public async Task<Programacion_autobuses> CreateAsync(Programacion_autobuses unaProgramacion_autobuses)
        {
            try
            {
                var unaProgramacion_autobusesExistente = new Programacion_autobuses();
                bool resultadoProgramacion_autobuses = false;


                // Realizamos la validación para asegurarnos de que el autobus no esté en otra Programacion del autobus a la misma hora.
                bool isBusAvailable = await programacion_AutobusesRepository.BusIsAvailableAtHour(unaProgramacion_autobuses.hora, unaProgramacion_autobuses.id_autobus);

                if (isBusAvailable)
                {
                    throw new AppValidationException("El autobus no está disponible en esa hora.");
                }


                resultadoProgramacion_autobuses = await programacion_AutobusesRepository.CreateAsync(unaProgramacion_autobuses);

                if (resultadoProgramacion_autobuses)
                {
                    unaProgramacion_autobusesExistente = await programacion_AutobusesRepository.GetByIdAsync(unaProgramacion_autobuses.id);
                }

                return unaProgramacion_autobusesExistente;
            }
            catch (DbOperationException error)
            {
                throw error;
            }
        }

        //Método para validar actualizar una Programacion del autobus
        public async Task<bool> UpdateAsync(Programacion_autobuses unaProgramacion_autobuses)
        {
            try
            {
                Programacion_autobuses Programacion_autobusesExistente = new();

                // Validar que lo que se busca actualizar no sea lo mismo que ya está
                Programacion_autobusesExistente = await programacion_AutobusesRepository.
                    GetByIdAsync(unaProgramacion_autobuses.id);

                if (string.IsNullOrEmpty(Programacion_autobusesExistente.id))
                {
                    if (string.IsNullOrEmpty(unaProgramacion_autobuses.id_autobus) || unaProgramacion_autobuses.hora == -1)
                    {
                        // Validar que el autobus que se actualizará no se esté cargando a esa hora
                        bool isBusAvailable = await programacion_AutobusesRepository.BusIsAvailableAtHour(unaProgramacion_autobuses.hora, unaProgramacion_autobuses.id_autobus);

                        if (!isBusAvailable)
                        {

                            throw new AppValidationException("El autobus no está disponible en esa hora para la actualización.");
                        }
                        return await programacion_AutobusesRepository.UpdateAsync(unaProgramacion_autobuses);
                    }
                    else
                    {
                        throw new AppValidationException("Los datos de la Programacion del autobus a actualizar son nulos.");
                    }
                }
                else
                {
                    throw new AppValidationException("La Programacion del autobus que intentas actualizar ya existe.");
                }
            }
            catch (InvalidOperationException error)
            {
                throw error;
            }
        }
        // Método para eliminar una Programacion del autobus existente en la base de datos.
        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                // Validar que la Programacion del autobus existe
                Programacion_autobuses Programacion_autobusesExistente = await programacion_AutobusesRepository
                    .GetByIdAsync(id);

                if (!string.IsNullOrEmpty(Programacion_autobusesExistente.id))
                {
                    return await programacion_AutobusesRepository
                        .DeleteAsync(Programacion_autobusesExistente);

                }
                else
                {
                    throw new AppValidationException("La Programacion del autobus que intentas eliminar no existe.");
                }
            }
            catch (DbOperationException error)
            {
                throw error;
            }
        }
    }
}
