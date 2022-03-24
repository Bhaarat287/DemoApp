using DemoAppAPI.CRUDAPI;
using DemoAppAPI.Extensions;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Microsoft.AspNetCore.Authentication
{
    public class DemoApiAuthHandler
         : AuthenticationHandler<DemoAppAuthSchemeOptions>
    {
        public DemoApiAuthHandler(
            IOptionsMonitor<DemoAppAuthSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("ApiKey"))
            {
                return Task.FromResult(AuthenticateResult.Fail("ApiKey Not Found."));
            }

            var header = Request.Headers["ApiKey"].ToString();
            Operations operations = new();
            if (operations.ValidateKey(header))
            {
                if (header != null)
                {
                    var claims = new[] { new Claim(ClaimTypes.NameIdentifier, header) };
                    var claimsIdentity = new ClaimsIdentity(claims, nameof(DemoApiAuthHandler));
                    var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), this.Scheme.Name);
                    return Task.FromResult(AuthenticateResult.Success(ticket));
                }
            }
            return Task.FromResult(AuthenticateResult.Fail("Failed to authenticate"));
        }
    }
}
