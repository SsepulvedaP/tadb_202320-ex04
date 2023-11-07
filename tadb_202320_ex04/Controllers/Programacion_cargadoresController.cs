using tadb_202320_ex04.Helpers;
using tadb_202320_ex04.Models;
using tadb_202320_ex04.Services;
using Microsoft.AspNetCore.Mvc;

namespace tadb_202320_ex04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Programacion_cargadoresController : Controller
    {
        private readonly Programacion_cargadoresServices _programacion_cargadoresServices;

        public Programacion_cargadoresController(Programacion_cargadoresServices programacion_cargadoresServices)
        {
            _programacion_cargadoresServices = programacion_cargadoresServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var lasProgramacion_cargadoreses = await _programacion_cargadoresServices.GetAllProgramacion_cargadoresesAsync();

            return Ok(lasProgramacion_cargadoreses);
        }

        //Controlador para crear
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Programacion_cargadores unaProgramacion_cargadores)
        {
            try
            {
                Console.WriteLine(unaProgramacion_cargadores.hora);
                var Programacion_cargadoresCreada = await _programacion_cargadoresServices.CreateAsync(unaProgramacion_cargadores);

                return Ok(Programacion_cargadoresCreada);
            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en la base de datos: {error.Message}");
            }
        }

        //TODO: Controlador para actualización de Programacion_cargadoreses

        //Controlador para actualizar una Utilización
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateAsync(Programacion_cargadores unaProgramacion_cargadores, string id)
        {
            try
            {
                var operacionActualizado = await _programacion_cargadoresServices.UpdateAsync(unaProgramacion_cargadores, id);

                return Ok(operacionActualizado + ": Se dió la actualizacion");

            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en la base de datos: {error.Message}");
            }
        }


        //Controlador para eliminar una Utilización
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                await _programacion_cargadoresServices.DeleteAsync(id);

                return Ok($"La utilización con id: {id} fue eliminada");

            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en la base de datos: {error.Message}");
            }
        }

    }
}