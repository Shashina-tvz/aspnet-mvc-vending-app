using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using VendingMachineApp.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;    
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace VendingMachineApp.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing"); //Postavi environment na "Testing" da bi se izbjeglo korištenje stvarne baze i drugih produkcijskih servisa
            
            builder.ConfigureServices(services =>
            {
                //1. Pronadi i ukloni staru registarciju za DbContextOptions<AppDbContext>
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                // 2. Registriraj novi DbContext koji koristi InMemory bazu samo za testove
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });

                // 3. Opcionalno: Dodaj lažni AuthenticationHandler koji će preskočiti autentikaciju i autorizaciju tijekom testiranja    
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "TestScheme";
                    options.DefaultChallengeScheme = "TestScheme";
                })
                .AddScheme<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions, TestAuthHandler>(
                    "TestScheme", options => { });
            });
        }
    }

    public class TestAuthHandler : Microsoft.AspNetCore.Authentication.AuthenticationHandler<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions>
{
    public TestAuthHandler(
        Microsoft.Extensions.Options.IOptionsMonitor<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions> options,
        Microsoft.Extensions.Logging.ILoggerFactory logger,
        System.Text.Encodings.Web.UrlEncoder encoder,
        Microsoft.AspNetCore.Authentication.ISystemClock clock)
        : base(options, logger, encoder, clock) { }

    protected override Task<Microsoft.AspNetCore.Authentication.AuthenticateResult> HandleAuthenticateAsync()
    {
        // Stvaramo lažni identitet (TestUser) koji će preskočiti [Authorize] filter u kontroleru
        var claims = new[] { new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, "TestUser") };
        var identity = new System.Security.Claims.ClaimsIdentity(claims, "Test");
        var principal = new System.Security.Claims.ClaimsPrincipal(identity);
        var ticket = new Microsoft.AspNetCore.Authentication.AuthenticationTicket(principal, "TestScheme");

        var result = Microsoft.AspNetCore.Authentication.AuthenticateResult.Success(ticket);
        return Task.FromResult(result);
    }
}

    
}