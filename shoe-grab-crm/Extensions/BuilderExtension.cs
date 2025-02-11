using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace ShoeGrabCRM.Extensions;

public static class BuilderExtension
{
    public static void SetupKestrel(this WebApplicationBuilder builder)
    {
        builder.WebHost.UseKestrel(options =>
        {
            options.Configure();
        });

        builder.WebHost.ConfigureKestrel((context, options) =>
        {
            var kestrelSection = context.Configuration.GetSection("Kestrel:Endpoints");

            var grpcEndpoint = kestrelSection.GetSection("Grpc");
            if (grpcEndpoint.Exists())
            {
                var grpcUrl = new Uri(Environment.GetEnvironmentVariable("CRM_GRPC_URI"));
                options.Listen(IPAddress.Parse(grpcUrl.Host), grpcUrl.Port, listenOptions =>
                {
                    listenOptions.Protocols = Enum.Parse<HttpProtocols>(grpcEndpoint["Protocols"]);
                    listenOptions.UseHttps(httpsOptions =>
                    {
                        var certificatePath = grpcEndpoint["Certificate:Path"];
                        var certificatePassword = grpcEndpoint["Certificate:Password"];
                        var parseSuccess = Enum.TryParse(grpcEndpoint["Certificate:ClientCertificateMode"], out ClientCertificateMode clientCertificateMode);

                        if (certificatePath != null && certificatePassword != null && parseSuccess)
                        {
                            var certificate = new X509Certificate2(certificatePath, certificatePassword);
                            httpsOptions.ServerCertificate = certificate;
                            httpsOptions.ClientCertificateMode = clientCertificateMode;
                        }
                    });
                });
            }
        });
    }
}
