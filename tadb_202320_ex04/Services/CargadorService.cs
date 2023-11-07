using tadb_202320_ex04.Helpers;
using tadb_202320_ex04.Models;
using tadb_202320_ex04.Repositories;

namespace tadb_202320_ex04.Services
{
    public class CargadorService
    {
        private readonly CargadoresRepository _cargadorRepository;
        public CargadorService(CargadoresRepository cargadorRepository)
        {
            _cargadorRepository = cargadorRepository;
        }
        public async Task<IEnumerable<Cargadores>> GetAllAsync()
        {
            return await _cargadorRepository
                .GetAllAsync();
        }
        public async Task<Cargadores> GetByIdAsync(string id_cargador)
        {
            //Validamos que el cargador exista con ese Id
            var Cargador = await _cargadorRepository
                .GetByIdAsync(id_cargador);

            if (string.IsNullOrEmpty(Cargador.id_cargador))
                throw new AppValidationException($"Cargador no encontrado con el id {id_cargador}");

            return Cargador;
        }

        public async Task<Cargadores> CreateAsync(Cargadores Cargador)
        {
            //Validamos que el cargador tenga un Voltaje
            if (Cargador.voltaje.Length == 0)
                throw new AppValidationException("No se puede insertar un cargador sin Voltaje.");

            //Validamos que no exista un cargador con ese Voltaje
            var unCargadorAuxSerial = await _cargadorRepository.GetBySerialAsync(Cargador.voltaje);

            if (unCargadorAuxSerial.voltaje != string.Empty)
            {
                throw new AppValidationException("Ya existe un cargador con este Voltaje");
            }

            var unCargadorExistente = new Cargadores();
            try
            {
                bool resultadoAccion = await _cargadorRepository.CreateAsync(Cargador);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la Base de datos");

                unCargadorExistente = await _cargadorRepository.GetByIdAsync(Cargador.id_cargador);
            }
            catch (DbOperationException error)
            {
                throw error;
            }

            return (unCargadorExistente);
        }

        //Actualizar cargadores
        public async Task<Cargadores> UpdateAsync(string id_cargador, Cargadores Cargador)
        {
            //Validamos que los parametros sean consistentes
            if (id_cargador != Cargador.id_cargador)
                throw new AppValidationException($"Inconsistencia en el id del cargador a actualizar.");

            //Validamos que el cargador exista con ese Id
            var cargadorExistente = await _cargadorRepository
                .GetByIdAsync(id_cargador);

            if (string.IsNullOrEmpty(Cargador.id_cargador))
                throw new AppValidationException($"No existe un cargador registrado con el id {Cargador.id_cargador}");

            //Validamos que el cargador tenga Voltaje
            if (Cargador.voltaje.Length == 0)
                throw new AppValidationException("No se puede actualizar un cargador con Voltaje nulo");

            //Validamos que el Voltaje no esté repetido
            var unCargadorAuxSerial = await _cargadorRepository.GetBySerialAsync(Cargador.voltaje);

            if (unCargadorAuxSerial.voltaje != string.Empty)
            {
                throw new AppValidationException("No se puede actualizar, ya existe un cargador con este Voltaje");
            }

            //Validamos que haya al menos un cambio en las propiedades
            if (Cargador.Equals(cargadorExistente))
                throw new AppValidationException("No hay cambios en los atributos del cargador.");

            try
            {
                bool resultadoAccion = await _cargadorRepository
                    .UpdateAsync(Cargador);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la Base de datos");
            }
            catch (DbOperationException error)
            {
                throw error;
            }

            return Cargador;
        }

        public async Task DeleteAsync(string id_cargador)
        {
            // validamos que el cargador a eliminar si exista con ese Id
            var cargadorExistente = await _cargadorRepository
                .GetByIdAsync(id_cargador);

            if (string.IsNullOrEmpty(cargadorExistente.id_cargador))
                throw new AppValidationException($"No existe un cargador con el id {id_cargador} que se pueda eliminar");

            try
            {
                bool resultadoAccion = await _cargadorRepository
                    .DeleteAsync(cargadorExistente);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la Base de datos");
            }
            catch (DbOperationException error)
            {
                throw error;
            }
        }
    }
}
