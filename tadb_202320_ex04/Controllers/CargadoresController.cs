using tadb_202320_ex04.Helpers;
using tadb_202320_ex04.Models;
using tadb_202320_ex04.Services;
using Microsoft.AspNetCore.Mvc;

namespace tadb_202320_ex04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargadoresController : Controller
    {
        private readonly CargadorService _cargadorService;

        public CargadoresController(CargadorService cargadorService)
        {
            _cargadorService = cargadorService;
        }

        //Controlador para visualizar todos los cargadores
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var loscargadores = await _cargadorService.GetAllAsync();

            return Ok(loscargadores);
        }

        //Controlador para visualizar un cargador por id
        [HttpGet("{id_cargador:length(24)}")]
        public async Task<IActionResult> GetByIdAsync(string id_cargador)
        {
            try
            {
                var unCargador = await _cargadorService.GetByIdAsync(id_cargador);
                return Ok(unCargador);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }

        //Controlador para crear un cargador
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Cargadores unCargador)
        {
            try
            {
                var cargadorCreado = await _cargadorService.CreateAsync(unCargador);

                return Ok(cargadorCreado);
            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en Base de datos: {error.Message}");
            }
        }

        //Controlador para actualizar un cargador
        [HttpPut("{id_cargador:length(24)}")]
        public async Task<IActionResult> UpdateAsync(string id_cargador, Cargadores unCargador)
        {
            try
            {
                var cargadorActualizado = await _cargadorService.UpdateAsync(id_cargador, unCargador);

                return Ok(cargadorActualizado);

            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en Base de datos: {error.Message}");
            }
        }

        //Controlador para eliminar un cargador
        [HttpDelete("{id_cargador:length(24)}")]
        public async Task<IActionResult> DeleteAsync(string id_cargador)
        {
            try
            {
                await _cargadorService.DeleteAsync(id_cargador);

                return Ok($"El cargador con id: {id_cargador} fue eliminado");

            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en Base de datos: {error.Message}");
            }
        }
    }
}