using tadb_202320_ex04.Models;
using tadb_202320_ex04.Helpers;
using tadb_202320_ex04.Repositories;
using System.Threading.Tasks;

namespace tadb_202320_ex04.Services
{
    public class Horarios_OperacionService
    {
        private readonly Horarios_operacionRepository _Horarios_operacionRepository;
        public Horarios_OperacionService(Horarios_operacionRepository Horarios_operacionRepository)
        {
            _Horarios_operacionRepository = Horarios_operacionRepository;
        }
        public async Task<IEnumerable<Horarios_operacion>> GetAllAsync()
        {
            return await _Horarios_operacionRepository.GetAllAsync();
        }
        public async Task<Horarios_operacion> GetByIdAsync(int id_hora)
        {
            //Validamos que la hora exista con ese Id
            var Hora = await _Horarios_operacionRepository.GetByIdAsync(id_hora);

            if (Hora.hora == -1)
                throw new AppValidationException($"Esta hora no se ha encontrado {id_hora}");

            return Hora;
        }

        public async Task<Horarios_operacion> CreateAsync(Horarios_operacion Hora)
        {
            //Validamos que la hora este en el rango
            if (Hora.hora < 0 || Hora.hora > 23)
                throw new AppValidationException("Debe insertar una hora entre 0 y 23");

            //Validamos que no exista esa hora
            var unaHoraAux = await _Horarios_operacionRepository.GetByIdAsync(Hora.hora);

            if (unaHoraAux.hora != -1)
            {
                throw new AppValidationException("Esta hora ya existe");
            }

            var unaHoraExistente = new Horarios_operacion();

            try
            {
                bool resultadoAccion = await _Horarios_operacionRepository.CreateAsync(Hora);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la Base de datos");

                unaHoraExistente = await _Horarios_operacionRepository.GetByIdAsync(Hora.hora);
            }
            catch (DbOperationException error)
            {
                throw error;
            }

            return (unaHoraExistente);
        }
    }
}