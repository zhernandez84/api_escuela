using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreAPIMySQL.Model;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;
using NetCoreAPIMySQL.Data.Repositories;

namespace escuela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
       // private readonly IConfiguration _config;
        private readonly IAlumnoRepository _alumnoRepository;
        public MateriaController( IAlumnoRepository alumnoRepository)
        {
           //_config = config;
            _alumnoRepository = alumnoRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMateria()
        {
            SearchMateriaResponse response = new SearchMateriaResponse();

            try
            {

                response = await _alumnoRepository.GetMateria();
                if (response.nCodigo == 0)
                {
                    return BadRequest(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje, Data = response.materia });
                }

            }
            catch (Exception ex)
            {
                response.nCodigo = 0;
                response.sMensaje = ex.Message;
                return BadRequest(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje, Data = ex.Message });
            }

            return Ok(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje, Data = response.materia });
        }
    }
}
