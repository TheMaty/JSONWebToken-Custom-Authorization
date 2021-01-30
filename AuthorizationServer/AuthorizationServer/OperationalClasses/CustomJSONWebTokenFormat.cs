using Microsoft.Owin.Security;
using System;
using System.Data.Entity;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Thinktecture.IdentityModel.Tokens;

namespace AuthorizationServer.OperationalClasses
{
    public class CustomJSONWebTokenFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly string _issuer;

        public CustomJSONWebTokenFormat(string issuer)
        {
            _issuer = issuer;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            string audienceId = data.Properties.Dictionary.ContainsKey("audience") ? data.Properties.Dictionary["audience"] : null;

            if (string.IsNullOrWhiteSpace(audienceId) || audienceId.Length != 36)
            {
                throw new InvalidOperationException("audience missing from AuthenticationTicket.Properties");
            }

            Audience audienceDto;

            using (var dbContext = new URDEV_SW_MOBILITYEntities())
            {
                audienceDto = dbContext.Audiences.FirstOrDefaultAsync(x => x.ClientId == audienceId).Result;
            }

            if (audienceDto == null)
            {
                throw new InvalidOperationException("invalid_client");
            }

            var keyByteArray = Convert.FromBase64String(audienceDto.Secret);

            //var signingKey = new HmacSigningCredentials(keyByteArray);



            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(keyByteArray);
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                        securityKey, SecurityAlgorithms.HmacSha256Signature);
          
            var issued = data.Properties.IssuedUtc;
            var expires = data.Properties.ExpiresUtc;
            var token = new JwtSecurityToken(_issuer, audienceId, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}