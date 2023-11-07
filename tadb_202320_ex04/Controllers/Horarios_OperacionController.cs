using tadb_202320_ex04.Helpers;
using tadb_202320_ex04.Models;
using tadb_202320_ex04.Services;
using Microsoft.AspNetCore.Mvc;


namespace tadb_202320_ex04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Horarios_OperacionController : Controller
    {
        private readonly Horarios_OperacionService _Horarios_OperacionService;

        public Horarios_OperacionController(Horarios_OperacionService horarios_OperacionService)
        {
            _Horarios_OperacionService = horarios_OperacionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var lasHoras = await _Horarios_OperacionService.GetAllAsync();

            return Ok(lasHoras);
        }

    }

}
