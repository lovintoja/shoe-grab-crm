using System.Net;
using System.Net.Mail;
using ShoeGrabCommonModels;

namespace ShoeGrabCRM.Services;

public class SmtpEmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;
    private readonly string _senderEmail;

    public SmtpEmailService(IConfiguration config)
    {
        _senderEmail = config["Smtp:SenderEmail"];
        _smtpClient = new SmtpClient(config["Smtp:Host"])
        {
            Port = int.Parse(config["Smtp:Port"]),
            Credentials = new NetworkCredential(
                config["Smtp:Username"],
                config["Smtp:Password"]
            ),
            EnableSsl = true
        };
    }

    public async Task<bool> SendOrderConfirmationEmailAsync(string recipientEmail)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_senderEmail),
            Subject = "Your Order Confirmation",
            Body = BuildOrderConfirmationEmailBody(),
            IsBodyHtml = true
        };
        mailMessage.To.Add(recipientEmail);
        try
        {
            //await _smtpClient.SendMailAsync(mailMessage); --> needs smtp set up
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    private string BuildOrderConfirmationEmailBody()
    {
        return $@"
            <h1>Thank you for your order!</h1>
        ";
    }
}