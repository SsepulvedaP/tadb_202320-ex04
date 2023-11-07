using tadb_202320_ex04.Helpers;
using tadb_202320_ex04.Models;
using tadb_202320_ex04.Repositories;
namespace tadb_202320_ex04.Services
{
    public class Programacion_cargadoresServices
    {
        private readonly Programacion_cargadoresRepository _Programacion_cargadoresRepository;
        private readonly AutobusesRepository _busesRepository;
        private readonly CargadoresRepository _cargadoresRepository;
        public Programacion_cargadoresServices(Programacion_cargadoresRepository Programacion_cargadoresRepository, AutobusesRepository busesRepository, CargadoresRepository cargadoresRepository)
        {
            _Programacion_cargadoresRepository = Programacion_cargadoresRepository;
            _busesRepository = busesRepository;
            _cargadoresRepository = cargadoresRepository;
        }

        //Validación para traer todas las programaciones de cargadores
        public async Task<IEnumerable<Programacion_cargadores>> GetAllUtilizacionesAsync()
        {
            return await _Programacion_cargadoresRepository.GetAllAsync();
        }

        //Validación para traer una Programacion de cargadores por id
        public async Task<Programacion_cargadores> GetByIdAsync(string id)
        {
            //Válidamos que el autobus exista con esa Marca
            var unaProgramacion_cargadores = await _Programacion_cargadoresRepository.GetByIdAsync(id);

            if (string.IsNullOrEmpty(unaProgramacion_cargadores.id))
                throw new AppValidationException($"Autobus no encontrado con el id: {id}");

            return unaProgramacion_cargadores;
        }

        public async Task<Programacion_cargadores> CreateAsync(Programacion_cargadores unaProgramacion_cargadores)
        {
            //Validamos que la Programacion de cargadores tenga hora valida
            if (unaProgramacion_cargadores.hora < 0 || unaProgramacion_cargadores.hora > 23)
                throw new AppValidationException("No se puede insertar una Programacion de cargadores sin hora válida");

            //Válidamos que la hora no sea pico
            if (unaProgramacion_cargadores.hora > 4 && unaProgramacion_cargadores.hora < 9 || unaProgramacion_cargadores.hora > 15 && unaProgramacion_cargadores.hora < 20)
                throw new AppValidationException("no se puede cargar el autobus en hora pico");



            //Válidamos que se envie con id_autobus
            if (string.IsNullOrEmpty(unaProgramacion_cargadores.id_autobus))
                throw new AppValidationException($"El valor de id_autobus no puede ir vacío");

            //Validamos que el autobus exista
            var busExistente = await _busesRepository.GetByIdAsync(unaProgramacion_cargadores.id_autobus);

            if (string.IsNullOrEmpty(busExistente.Marca))
                throw new AppValidationException($"No existe ese autobus");

            //Válidamos que el autobus no esté en operación
            bool busOperando = await _Programacion_cargadoresRepository.BusIsAvailableAtHour(unaProgramacion_cargadores.hora, busExistente.id_autobus);

            if (!busOperando)
                throw new AppValidationException($"Ya está operando el autobus a esa hora");

            //Validamos que el cargador exista
            var cargadorExiste = await _cargadoresRepository.GetByIdAsync(unaProgramacion_cargadores.id_cargador);

            if (string.IsNullOrEmpty(cargadorExiste.voltaje))
                throw new AppValidationException($"No existe ese cargador");

            //Validamos que el cargador no esté en uso a esa hora
            bool cargadorUso = await _Programacion_cargadoresRepository.CargadorAvailableAtHour(unaProgramacion_cargadores.id_cargador, unaProgramacion_cargadores.hora);

            if (!cargadorUso)
                throw new AppValidationException($"Ya se está usando ese cargador a esa hora");

            //Se intenta añadir una nueva Programacion de cargadores a Mongo
            Programacion_cargadores utilizacionExiste = new();

            try
            {
                bool resultadoAccion = await _Programacion_cargadoresRepository.CreateAsync(unaProgramacion_cargadores);

                if (resultadoAccion == false)
                    throw new AppValidationException("La operación se intentó ejecutar, pero no tuvo cambios en la base de datos");

                utilizacionExiste = await _Programacion_cargadoresRepository.GetByIdAsync(unaProgramacion_cargadores.id);
            }
            catch (DbOperationException error)
            {
                throw error;
            }

            return utilizacionExiste;
        }

        public async Task<bool> UpdateAsync(Programacion_cargadores unaProgramacion_cargadores, string id)
        {
            //Validamos que los parametros sean consistentes
            if (id != unaProgramacion_cargadores.id)
            {
                throw new AppValidationException($"Inconsistencia en el Id de la actualización a actualizar.");
            }

            //Validamos que la Programacion de cargadores tenga hora valida
            if (unaProgramacion_cargadores.hora < 0 || unaProgramacion_cargadores.hora > 23)
                throw new AppValidationException("No se puede insertar una Programacion de cargadores sin hora válida");

            //Válidamos que la hora no sea pico
            if (unaProgramacion_cargadores.hora > 4 && unaProgramacion_cargadores.hora < 9 || unaProgramacion_cargadores.hora > 15 && unaProgramacion_cargadores.hora < 20)
                throw new AppValidationException("No se puede cargar el autobus en hora pico");



            //Válidamos que se envie con id_autobus
            if (string.IsNullOrEmpty(unaProgramacion_cargadores.id_autobus))
                throw new AppValidationException($"El valor de id_autobus no puede estar vacío");

            //Validamos que el autobus exista
            var busExistente = await _busesRepository.GetByIdAsync(unaProgramacion_cargadores.id_autobus);

            if (string.IsNullOrEmpty(busExistente.Marca))
                throw new AppValidationException($"No existe ese autobus");

            //Válidamos que el autobus no esté en operación
            bool busOperando = await _Programacion_cargadoresRepository.BusIsAvailableAtHour(unaProgramacion_cargadores.hora, busExistente.id_autobus);

            if (!busOperando)
                throw new AppValidationException($"Ya está operando el autobus a esa hora");

            //Validamos que el cargador exista
            var cargadorExiste = await _cargadoresRepository.GetByIdAsync(unaProgramacion_cargadores.id_cargador);

            if (string.IsNullOrEmpty(cargadorExiste.voltaje))
                throw new AppValidationException($"No existe ese cargador");

            //Validamos que el cargador no esté en uso a esa hora
            bool cargadorUso = await _Programacion_cargadoresRepository.CargadorAvailableAtHour(unaProgramacion_cargadores.id_cargador, unaProgramacion_cargadores.hora);

            if (!cargadorUso)
                throw new AppValidationException($"Ya se está usando ese cargador a esa hora");

            // Llamar al repositorio para actualizar la Programacion de cargadores
            return await _Programacion_cargadoresRepository.UpdateAsync(unaProgramacion_cargadores);
        }

        public async Task DeleteAsync(string id)
        {
            //Validamos que exista un autobus con dicho id
            var utilizacionExiste = await _Programacion_cargadoresRepository.GetByIdAsync(id);

            if (string.IsNullOrEmpty(utilizacionExiste.id))
            {
                throw new AppValidationException($"Ya existe una Programacion de cargadores con ese id ");
            }

            try
            {
                bool resultadoAccion = await _Programacion_cargadoresRepository.DeleteAsync(utilizacionExiste);

                if (resultadoAccion == false)
                    throw new AppValidationException("La operación se intentó ejecutar, pero no tuvo cambios en la base de datos");
            }
            catch (DbOperationException error)
            {

                throw error;
            }

        }
    }
}
