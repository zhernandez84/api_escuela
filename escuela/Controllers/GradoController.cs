using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreAPIMySQL.Model;
using System.Threading.Tasks;
using System;
using NetCoreAPIMySQL.Data.Repositories;

namespace escuela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradoController : ControllerBase
    {
        private readonly IAlumnoRepository _alumnoRepository;
        public GradoController(IAlumnoRepository alumnoRepository)
        {
            _alumnoRepository = alumnoRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetGrado()
        {
            SearchGradoResponse response = new SearchGradoResponse();

            try
            {

                response = await _alumnoRepository.GetGrado();
                if (response.nCodigo == 0)
                {
                    return BadRequest(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje, Data = response.grado });
                }

            }
            catch (Exception ex)
            {
                response.nCodigo = 0;
                response.sMensaje = ex.Message;
                return BadRequest(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje, Data = ex.Message });
            }

            return Ok(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje, Data = response.grado });
        }
    }
}
