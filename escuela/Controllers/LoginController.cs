using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NetCoreAPIMySQL.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;
using NetCoreAPIMySQL.Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using Org.BouncyCastle.Asn1.Ocsp;

namespace API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAlumnoRepository _alumnoRepository;
        public LoginController(IConfiguration config, IAlumnoRepository alumnoRepository)
        {
            _config = config;
            _alumnoRepository = alumnoRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(UserLoginRequest request)
        {
            UserLoginResponse response = new UserLoginResponse();

            try
            {

                response = await _alumnoRepository.UserLogin(request);

                if (response.nCodigo == 0)
                {
                    return BadRequest(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje });
                }
                else
                {
                    var token = GenerateToken(request);
                    return Ok(new { nCodigo = response.nCodigo, sMensaje = response.sMensaje, Data = token });
                }

            }
            catch (Exception ex)
            {
                response.nCodigo = 0;
                response.sMensaje = ex.Message;
                return BadRequest(new { nCodigo = response.nCodigo, sMensaje = ex.Message });
            }
        }

        private string GenerateToken(UserLoginRequest request)
        {

            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, request.UserName)
            };


            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
