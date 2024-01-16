

namespace PlatformAMA.Modules.Auth.Interfaces
{
  public interface IEmailService
  {
    Task ResetPasswordAsync(string to, string token);
  }

}