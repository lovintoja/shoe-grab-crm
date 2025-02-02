using ShoeGrabCommonModels;

namespace ShoeGrabCRM.Services;

public interface IEmailService
{
    Task<bool> SendOrderConfirmationEmailAsync(string recipientEmail);
}