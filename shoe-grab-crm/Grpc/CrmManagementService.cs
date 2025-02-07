using Grpc.Core;
using ShoeGrabCRM.Services;

namespace ShoeGrabCRM.Grpc;

public class CrmManagementService : CrmService.CrmServiceBase
{
    private readonly IEmailService _emailService;
    public CrmManagementService(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public override async Task<SendNotificationEmailResponse> SendNotificationEmail(SendNotificationEmailRequest request, ServerCallContext context)
    {
        var success = await _emailService.SendOrderConfirmationEmailAsync(request.RecepientEmail);
        return new SendNotificationEmailResponse { Success = success };
    }
}
