using System;
using System.Web.Http;
using AuthorizationServer.OperationalClasses;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(AuthorizationServer.Startup))]

namespace AuthorizationServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            ConfigureOAuth(app);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            int accessTokenExpiresInSeconds = ConfigurationHelper.GetAppSetting("AccessTokenExpirationInSeconds").ToInt();

            var oAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString(ConfigurationHelper.GetAppSetting("TokenEndPoint")),
                AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(accessTokenExpiresInSeconds),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJSONWebTokenFormat(ConfigurationHelper.GetAppSetting("TokenIssuer"))
            };

            app.UseOAuthAuthorizationServer(oAuthServerOptions);
        }
    }
}
