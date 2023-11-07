using tadb_202320_ex04.Models;
using tadb_202320_ex04.Helpers;
using tadb_202320_ex04.Repositories;

namespace tadb_202320_ex04.Services
{
    public class AutobusService
    {
        //Variable Repository para hacer las operaciones CRUD a buses
        private readonly AutobusesRepository _AutobusRepository;

        //Constructor del AutobusRepository, que recibe un parametro del tipo AutobusesRepository, que es del mismo tipo de su propiedad
        public AutobusService(AutobusesRepository AutobusRepository)
        {
            _AutobusRepository = AutobusRepository;
        }

        //Métodos para validar

        //Llama a todos los buses y devuelve un objeto iterable
        public async Task<IEnumerable<Autobuses>> GetAllAsync()
        {
            return await _AutobusRepository.GetAllAsync();
        }

        //Validación para devolver un autobus por marca
        public async Task<Autobuses> GetByPlacaAsync(string marca)
        {
            //Válidamos que el bus exista con esa marca
            var unAutobus = await _AutobusRepository.GetByPlacaAsync(marca);

            if (string.IsNullOrEmpty(unAutobus.Marca))
                throw new AppValidationException($"Autobuses no encontrado con la marca: {marca}");

            return unAutobus;
        }

        //Validación para devolver un autobus por marca
        public async Task<Autobuses> GetById(string id_autobus)
        {
            //Válidamos que el bus exista con esa marca
            var unAutobus = await _AutobusRepository.GetByIdAsync(id_autobus);

            if (string.IsNullOrEmpty(unAutobus.id_autobus))
                throw new AppValidationException($"Autobuses no encontrado con el id: {id_autobus}");

            return unAutobus;
        }

        //Validamos para la creación de un bus
        public async Task<Autobuses> CreateAsync(Autobuses unAutobus)
        {
            //Validamos que el bus tenga una marca
            if (unAutobus.Marca.Length == 0)
                throw new AppValidationException("No se puede insertar un bus con marca vacía");

            //Validamos que la marca ya no esté registrada para otro bus
            var busExistente = await _AutobusRepository.GetByPlacaAsync(unAutobus.Marca);

            if (string.IsNullOrEmpty(busExistente.Marca) == false)
                throw new AppValidationException($"Ya existe un bus con esa marca {unAutobus.Marca}");

            //Se intenta añadir un nuevo bus a Mongo
            try
            {
                bool resultadoAccion = await _AutobusRepository.CreateAsync(unAutobus);

                if (resultadoAccion == false)
                    throw new AppValidationException("La operación se intentó ejecutar, pero no tuvo cambios en la DB");

                busExistente = await _AutobusRepository.GetByPlacaAsync(unAutobus.Marca);
            }
            catch (DbOperationException error)
            {
                throw error;
            }

            return busExistente;
        }

        //Válidamos para la actualización de un autobus
        public async Task<Autobuses> UpdateAsync(string id_autobus, Autobuses unAutobus)
        {
            //Validamos que los parametros sean consistentes
            if (id_autobus != unAutobus.id_autobus)
            {
                throw new AppValidationException($"Inconsistencia en el Id del bus a actualizar. Verifica argumentos.");
            }

            //Válidamos que exista un bus con ese Id
            var busExistente = await _AutobusRepository.GetByIdAsync(id_autobus);

            if (string.IsNullOrEmpty(busExistente.id_autobus))
            {
                throw new AppValidationException($"No existe un bus registrado con el id{id_autobus}");
            }

            //Validamos que el bus tenga marca
            if (unAutobus.Marca.Length == 0)
            {
                throw new AppValidationException("No se puede actualizar un bus con marca nula");
            }

            try
            {
                bool resultadoAccion = await _AutobusRepository.UpdateAsync(unAutobus);

                if (resultadoAccion == false)
                    throw new AppValidationException("La operación se intentó ejecutar, pero no tuvo cambios en la DB");

                busExistente = await _AutobusRepository.GetByPlacaAsync(unAutobus.Marca);

            }
            catch (DbOperationException error)
            {

                throw error;
            }

            return busExistente;
        }

        //Válidamos para la eliminación de un bus
        public async Task DeleteAsync(string id_autobus)
        {
            //Validamos que exista un bus con dicho id
            var busExistente = await _AutobusRepository.GetByIdAsync(id_autobus);

            if (string.IsNullOrEmpty(busExistente.id_autobus))
            {
                throw new AppValidationException($"No existe un bus registrado con el id: {id_autobus}");
            }

            try
            {
                bool resultadoAccion = await _AutobusRepository.DeleteAsync(busExistente);

                if (resultadoAccion == false)
                    throw new AppValidationException("La operación se intentó ejecutar, pero no tuvo cambios en la DB");
            }
            catch (DbOperationException error)
            {

                throw error;
            }

        }

    }

}
