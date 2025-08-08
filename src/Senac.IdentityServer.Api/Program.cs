using System.Security.Cryptography.X509Certificates;

namespace Senac.IdentityServer.Api
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      var configuration = builder.Configuration;

      var cert = new X509Certificate2("./meucertificado.pfx", "senhadificil");

      builder.Services.AddIdentityServer(options =>
      {
        options.IssuerUri = configuration["IdentityServer:BaseUrl"];
      }).AddSigningCredential(cert)
        .AddInMemoryApiScopes(Config.ApiScopes)
        .AddInMemoryClients(Config.Clients);

      builder.Services.AddControllers();
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      var app = builder.Build();

      if (app.Environment.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseIdentityServer();

      app.Run();
    }
  }
}
