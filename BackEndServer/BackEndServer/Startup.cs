using System.Collections.Generic;
using System.Web.Http;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin;
using BackEndServer.OperationalClassess;

[assembly: OwinStartup(typeof(BackEndServer.Startup))]

namespace BackEndServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            this.ConfigureOAuth(app);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            List<Audience> audiences = (List<Audience>)BackEndServer.OperationalClassess.AuthorizationProvider.GetAllAudiences();

            var jwtAudiences = new List<string>();
            var tokenProviders = new List<IIssuerSecurityTokenProvider>();

            var issuer = ConfigurationHelper.GetAppSetting("TokenIssuer");

            foreach (var audienceTemp in audiences)
            {
                jwtAudiences.Add(audienceTemp.ClientId);
                tokenProviders.Add(new SymmetricKeyIssuerSecurityTokenProvider(issuer, TextEncodings.Base64Url.Decode(audienceTemp.Secret)));
            }

            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = jwtAudiences,
                    IssuerSecurityTokenProviders = tokenProviders
                });
        }
    }
}
