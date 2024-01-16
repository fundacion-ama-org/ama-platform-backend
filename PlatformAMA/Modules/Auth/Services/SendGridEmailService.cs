using PlatformAMA.Modules.Auth.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace PlatformAMA.Modules.Auth.Services
{
  public class SendGridEmailService : IEmailService
  {
    private readonly ISendGridClient _sendGridClient;
    private readonly string _senderEmail;
    private readonly IConfiguration _configuration;

    public SendGridEmailService(ISendGridClient sendGridClient, string senderEmail, IConfiguration configuration)
    {
      _sendGridClient = sendGridClient;
      _senderEmail = senderEmail;
      _configuration = configuration;
    }

    public Task ResetPasswordAsync(string to, string token)
    {
      var resetLink = $"{_configuration["ClientBaseUrl"]}/auth/restaurar-login?token={token}&email={to}";

      var message = new SendGridMessage();
      message.SetFrom(new EmailAddress(_senderEmail, "Fundación AMA"));
      message.SetTemplateId(_configuration["SendGrid:RestorePasswordTemplateId"]);
      message.AddTo(new EmailAddress(to));
      message.SetTemplateData(new
      {
        resetLink
      });

      return _sendGridClient.SendEmailAsync(message);
    }
  }
}

