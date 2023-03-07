using ActivosFijos.Data;
using ActivosFijos.Model;
using ActivosFijos.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ActivosFijos.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IConfiguration configuration;

        public AccountController(ApplicationDbContext DbContext, IConfiguration configuration)
        {
            this.DbContext = DbContext;
            this.configuration = configuration;
        }

        [HttpGet("/api/user")]
        public async Task<ActionResult<List<User>>> GetUser()
        {
            return await DbContext.User.ToListAsync();
        }

        [HttpPost("/api/user")]
        public async Task<ActionResult<List<User>>> CreateUser([FromBody] UserCreateDTO model)
        {
            var user = new User
            {
                Username = model.Username,
                Password = model.Password
            };

            DbContext.Add(user);
            await DbContext.SaveChangesAsync();
            return Ok("Usuario creado correctamente.");
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] UserCreateDTO login)
        {
            var user = await DbContext.User.
                FirstOrDefaultAsync(x => x.Username.ToLower() == login.Username.ToLower());

            if (user == null)
            {
                return Unauthorized("Usuario o contraseña no validos");
            }
            else if (user.Password != login.Password)
            {
                return Unauthorized("Usuario o contraseña no validos");
            }
            else
            {
                //    // Generar un token JWT
                //    var tokenHandler = new JwtSecurityTokenHandler();
                //    var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("JwtSecret"));
                //    var tokenDescriptor = new SecurityTokenDescriptor
                //    {
                //        Subject = new ClaimsIdentity(new[]
                //        {
                //            new Claim(ClaimTypes.Name, login.Username)
                //        }),
                //        Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("JwtExpirationMinutes")),
                //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                //    };
                //    var token = tokenHandler.CreateToken(tokenDescriptor);
                //    var tokenString = tokenHandler.WriteToken(token);

                //    return Ok(new { Token = tokenString });
                return Ok(login);

            }
        }

        [HttpPost("/logout")]
        public IActionResult LogOut()
        {
            // Invalidar el token de autenticación
            HttpContext.Response.Cookies.Delete("jwt");
            return Ok("Sesion cerrada correctametne.");
        }
    }
}
