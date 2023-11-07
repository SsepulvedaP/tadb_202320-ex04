using tadb_202320_ex04.Helpers;
using tadb_202320_ex04.Models;
using tadb_202320_ex04.Services;
using Microsoft.AspNetCore.Mvc;

namespace tadb_202320_ex04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutobusesController : Controller
    {
        private readonly AutobusService _AutobusService;

        public AutobusesController(AutobusService AutobusService)
        {
            _AutobusService = AutobusService;
        }

        //Controlador para visualizar todos los buses
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var losBuses = await _AutobusService.GetAllAsync();

            return Ok(losBuses);
        }

        //Visualizar autobus por id
        [HttpGet("{id_autobus:length(24)}")]
        public async Task<IActionResult> GetByIdAsync(string id_autobus)
        {
            try
            {
                var unAutobus = await _AutobusService.GetById(id_autobus);

                return Ok(unAutobus);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }

        //Crear un autobus
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Autobuses unAutobus)
        {
            try
            {
                var busCreado = await _AutobusService.CreateAsync(unAutobus);

                return Ok(busCreado);
            }
            catch (AppValidationException error)
            {

                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operación en la Base de datos: {error.Message}");
            }
        }

        //Atualizar un autobus
        [HttpPut("{id_autobus:length(24)}")]
        public async Task<IActionResult> Updateasync(string id_autobus, Autobuses unAutobus)
        {
            try
            {
                var busActualizado = await _AutobusService.UpdateAsync(id_autobus, unAutobus);

                return Ok(busActualizado);

            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operación en la Base de datos: {error.Message}");
            }
        }


        //Eliminar un autobus
        [HttpDelete("{id_autobus:length(24)}")]
        public async Task<IActionResult> DeleteAsync(string id_autobus)
        {
            try
            {
                await _AutobusService.DeleteAsync(id_autobus);

                return Ok($"El autobus con id: {id_autobus} fue eliminado");

            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operación en la Base de datos: {error.Message}");
            }
        }

    }
}