using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TP3.Models;

namespace TP3.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        UserManager<User> UserManager;
        IConfiguration Config;

        public UsersController(UserManager<User> userManager, IConfiguration configuration)
        {
            UserManager = userManager;
            this.Config = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            if (register.Password != register.PasswordConfirm)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Les deux mots de passe spécifiés sont différents." });
            }

            var user = new User
            {
                UserName = register.Username,
                Email = register.Email,
            };

            var result = await UserManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Une erreur est survenue lors de la création de l'utilisateur." });
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            User user = await UserManager.FindByNameAsync(login.Username);
            if (user != null && await UserManager.CheckPasswordAsync(user, login.Password))
            {
                IList<string> roles = await UserManager.GetRolesAsync(user);
                List<Claim> authClaims = new List<Claim>();
                foreach (string role in roles) { 
                     authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                authClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Config["JWT:Secret"]));
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: "https://localhost:7065",
                    audience: "http://localhost:4200",
                    claims: authClaims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    validTo = token.ValidTo
                });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Nom d'utilisateur ou mot de passe incorrect." });
            }
        }
    }
}
