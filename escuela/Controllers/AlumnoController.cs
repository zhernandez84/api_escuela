using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NetCoreAPIMySQL.Data.Repositories;
using NetCoreAPIMySQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using static Org.BouncyCastle.Math.EC.ECCurve;
using Microsoft.AspNetCore.Authorization;
using Org.BouncyCastle.Asn1.Ocsp;

namespace API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private readonly IAlumnoRepository _alumnoRepository;

        public AlumnoController(IAlumnoRepository alumnoRepository)
        {
            _alumnoRepository = alumnoRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAlumno()
        {
            SearchAlumnoResponse response = new SearchAlumnoResponse();

            try
            {

                response = await _alumnoRepository.GetAllAlumno();
                if (response.nCodigo == 0)
                {
                    return BadRequest(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje, Data = response.alumno });
                }

            }
            catch (Exception ex)
            {
                response.nCodigo = 0;
                response.sMensaje = ex.Message;
                return BadRequest(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje, Data = ex.Message });
            }

            return Ok(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje, Data = response.alumno });
        }

        [HttpGet("{busca}")]
        [Authorize]
        public async Task<IActionResult> GetAlumno(string busca)
        {
            SearchAlumnoResponse response = new SearchAlumnoResponse();

            try
            {

                response = await _alumnoRepository.GetAlumno(busca);
                if (response.nCodigo == 0)
                {
                    return BadRequest(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje, Data = response.alumno });
                }

            }
            catch (Exception ex)
            {
                response.nCodigo = 0;
                response.sMensaje = ex.Message;
                return BadRequest(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje, Data = ex.Message });
            }

            return Ok(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje, Data = response.alumno });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InsertAlumno([FromBody] Alumno alumno)
        {
            RegisterAlumnoResponse response = new RegisterAlumnoResponse();
            response = await _alumnoRepository.InsertAlumno(alumno);
            if (response.nCodigo != 1)
            {
                return BadRequest(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje });
            }
            return Ok(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje });
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateAlumno([FromBody] Alumno alumno)
        {
            RegisterAlumnoResponse response = new RegisterAlumnoResponse();
            response = await _alumnoRepository.UpdateAlumno(alumno);
            if (response.nCodigo != 1)
            {
                return BadRequest(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje });
            }
            return Ok(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje });

        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAlumno(int id)
        {
            RegisterAlumnoResponse response = new RegisterAlumnoResponse();
            response = await _alumnoRepository.DeleteAlumno(new Alumno() { nIdAlumno = id });
            if (response.nCodigo != 1)
            {
                return BadRequest(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje });
            }
            return Ok(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje });

        }

        [HttpPost("InsertarCalificacion")]
        [Authorize]
        public async Task<IActionResult> InsertCalificacion([FromBody] Calificacion calificacion)
        {
            RegisterCalificacionResponse response = new RegisterCalificacionResponse();
            response = await _alumnoRepository.InsertCalificacion(calificacion);
            if (response.nCodigo != 1)
            {
                return BadRequest(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje });
            }
            return Ok(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje });
        }

        [HttpPost("BuscarCalificacion")]
        [Authorize]
        public async Task<IActionResult> BuscarCalificacion([FromBody] SearchCalificacionRequest searchCalificacionRequest)
        {
            SearchCalificacionResponse response = new SearchCalificacionResponse();

            try
            {

                response = await _alumnoRepository.BuscarCalificacion(searchCalificacionRequest);
                if (response.nCodigo == 0)
                {
                    return BadRequest(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje, Data = response.searchCalificacion });
                }

            }
            catch (Exception ex)
            {
                response.nCodigo = 0;
                response.sMensaje = ex.Message;
                return BadRequest(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje, Data = ex.Message });
            }

            return Ok(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje, Data = response.searchCalificacion });
        }


    }
}
