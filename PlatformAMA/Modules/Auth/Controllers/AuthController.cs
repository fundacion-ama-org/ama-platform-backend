using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PlatformAMA.Modules.Auth.DTOs;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PlatformAMA.Modules.Auth.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _configuration = configuration;
    }

    /// <summary>
    /// Registrar un nuevo usuario
    /// </summary>
    /// <param name="signUpDTO">Datos del usuario a registrar</param>
    /// <returns></returns>
    [HttpPost("signup")]
    public async Task<ActionResult<AuthResponseDTO>> SignUp([FromBody] SingUpDTO signUpDTO)
    {
      var user = new IdentityUser { UserName = signUpDTO.Identification, Email = signUpDTO.Email };
      var result = await _userManager.CreateAsync(user, signUpDTO.Password);

      if (result.Succeeded)
      {
        return Ok(CreateToken(signUpDTO.Identification));
      }

      return BadRequest(result.Errors);
    }

    /// <summary>
    /// Iniciar sesión
    /// </summary>
    /// <param name="signInDTO">Credenciales de inicio de sesión</param>
    /// <returns></returns>
    [HttpPost("signin")]
    public async Task<ActionResult<AuthResponseDTO>> SignIn([FromBody] SingInDTO signInDTO)
    {
      var result = await _signInManager.PasswordSignInAsync(signInDTO.Identification, signInDTO.Password, isPersistent: false, lockoutOnFailure: false);

      if (result.Succeeded)
      {
        return Ok(CreateToken(signInDTO.Identification));
      }

      return Unauthorized();
    }

    /// <summary>
    /// Renovar el token de autenticación
    /// </summary>
    /// <returns></returns>
    [HttpGet("renew")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<AuthResponseDTO> Renew()
    {
      var identification = HttpContext.User.Claims
        .Where(x => x.Type == ClaimTypes.NameIdentifier)
        .FirstOrDefault()
        .Value;
        
      return Ok(CreateToken(identification));
    }

    private AuthResponseDTO CreateToken(string identification)
    {
      var claims = new[]
      {
        new Claim(ClaimTypes.NameIdentifier, identification)
      };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecret"]));
      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      DateTime expiration = DateTime.UtcNow.AddDays(1);

      var token = new JwtSecurityToken(
        issuer: null,
        audience: null,
        claims: claims,
        expires: expiration,
        signingCredentials: credentials
      );

      string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

      return new AuthResponseDTO()
      {
        Token = tokenString,
        Expiration = expiration
      };
    }
  }
}
