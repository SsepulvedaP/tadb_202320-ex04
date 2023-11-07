using tadb_202320_ex04.Helpers;
using tadb_202320_ex04.Models;
using tadb_202320_ex04.Services;
using Microsoft.AspNetCore.Mvc;

namespace tadb_202320_ex04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Programacion_autobusesController : Controller
    {
        private readonly Programacion_autobusesService _programacion_autobusesService;

        public Programacion_autobusesController(Programacion_autobusesService programacion_autobusesService)
        {
            _programacion_autobusesService = programacion_autobusesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var lasoperaciones = await _programacion_autobusesService.GetAllProgramacion_autobusesesAsync();

            return Ok(lasoperaciones);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            try
            {
                var unaProgramacion = await _programacion_autobusesService.GetByIdAsync(id);
                return Ok(unaProgramacion);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Programacion_autobuses unaProgramacion)
        {
            try
            {
                var operacionCreada = await _programacion_autobusesService.CreateAsync(unaProgramacion);

                return Ok(operacionCreada);
            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en DB: {error.Message}");
            }
        }

        //Controlador para actualizar 
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Programacion_autobuses unaProgramacion)
        {
            try
            {
                var operacionActualizado = await _programacion_autobusesService.UpdateAsync(unaProgramacion);

                return Ok(operacionActualizado);

            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en DB: {error.Message}");
            }
        }

        //Controlador para eliminar
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                await _programacion_autobusesService.DeleteAsync(id);

                return Ok($"La programacion con el id: {id} fue eliminada.");

            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en DB: {error.Message}");
            }
        }
    }
}
