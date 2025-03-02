using ShoeGrabCRM.Extensions;
using ShoeGrabCRM.Grpc;
using ShoeGrabCRM.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.SetupKestrel();

builder.Services.AddScoped<IEmailService, SmtpEmailService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddGrpc();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapGrpcService<CrmManagementService>();

app.MapControllers();

app.Run();
