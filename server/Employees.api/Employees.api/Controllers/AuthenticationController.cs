using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Employees.api.Models;
using Employees.api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Employees.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly string issuer;
        private readonly string key;
        private readonly IUsersRepository repository;

        private IConfiguration _config;


        public AuthenticationController(IConfiguration config, IUsersRepository _repository)
        {
            _config = config;
            this.issuer = _config.GetValue<string>("Jwt:Issuer");
            this.key = _config.GetValue<string>("Jwt:Secret");

            this.repository = _repository;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody]User newRegister)
        {
            if (repository.IsUserExist(newRegister))
            {
                return BadRequest("This email is allready exist");
            }

            repository.AddNewUser(newRegister);
            return Ok(new
            {
                user = newRegister.AsDto(),
                token = Login(new LoginAuthentication() { Email = newRegister.Email, Password = newRegister.Password })
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]LoginAuthentication login)
        {
            if (login == null)
            {
                return BadRequest("Invalid login request");
            }

            if (repository.Authenticate(login) != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.key));
                var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: this.issuer,
                    audience: this.issuer,
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: signingCredentials);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString });
            }

            return Unauthorized();
        }
    }
}



