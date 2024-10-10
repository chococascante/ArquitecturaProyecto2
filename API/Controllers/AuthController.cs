using DataAccess.EF.Models;
using DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO userData)
        {
            var user = await _userManager.FindByNameAsync(userData.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, userData.Password))
            {
                var token = await GenerateJwtToken(user);
                return Ok(new { token });
            }
            return Unauthorized();
        }

        private async Task<string> GenerateJwtToken(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);  // Retrieve the roles for the user
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            // Add role claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:DurationInMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromBody] LogUpDTO newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new AppUser
            {
                UserName = newUser.UserName,
                Email = newUser.Email
            };

            var createdUserResult = await _userManager.CreateAsync(user, newUser.Password);

            if (createdUserResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return Created("Usuario creado exitosamente", null);
            }

            foreach (var error in createdUserResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<bool> RoleTesting(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                // El usuario no existe
                return false;
            }

            var result = await _userManager.IsInRoleAsync(user, "Admin");
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> MakeAdmin(string userName)
        {
            // Buscar al usuario por su nombre de usuario
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }

            // Agregar el rol 'Admin' al usuario
            var result = await _userManager.AddToRoleAsync(user, "Admin");

            if (result.Succeeded)
            {
                return Ok("Usuario agregado al rol de Admin con �xito");
            }

            // Si hubo alg�n error al agregar el rol
            return BadRequest("No se pudo agregar el rol de Admin al usuario");
        }
    }
}