using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PlatformAMA.Modules.Auth.DTOs;
using PlatformAMA.Modules.Auth.Interfaces;
using SendGrid.Helpers.Mail;
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
    private readonly IEmailService _emailService;

    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, IEmailService emailService)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _configuration = configuration;
      _emailService = emailService;
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

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] RecoverPasswordDTO model)
    {
      var user = await _userManager.FindByEmailAsync(model.Email);
      if (user == null)
      {
        // Handle error: Usuario no encontrado
        return BadRequest("Usuario no encontrado");
      }

      var token = await _userManager.GeneratePasswordResetTokenAsync(user);

      await _emailService.ResetPasswordAsync(model.Email, token);

      // Puedes registrar el enlace de restablecimiento o devolver un mensaje de éxito
      return Ok("Se ha enviado un enlace de restablecimiento de contraseña por correo electrónico");
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] RecoverPasswordParams model)
    {
      var user = await _userManager.FindByEmailAsync(model.Email);
      if (user == null)
      { 
        return BadRequest("Usuario no encontrado");
      }

      var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
      if (result.Succeeded)
      {
        
        return Ok("Contraseña restablecida exitosamente");
      }

      return BadRequest(result.Errors);
    }

  }
}
