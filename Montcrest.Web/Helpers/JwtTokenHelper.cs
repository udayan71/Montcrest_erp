using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Montcrest.Web.Helpers
{
    public static class JwtTokenHelper
    {
        private static JwtSecurityToken ReadToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(token);
        }

        public static string GetRoleFromToken(string token)
        {
            var jwt = ReadToken(token);
            return jwt.Claims
                .First(c => c.Type == ClaimTypes.Role)
                .Value;
        }

        public static string GetUserIdFromToken(string token)
        {
            var jwt = ReadToken(token);
            return jwt.Claims
                .First(c => c.Type == ClaimTypes.NameIdentifier)
                .Value;
        }

        public static string GetEmailFromToken(string token)
        {
            var jwt = ReadToken(token);
            return jwt.Claims
                .First(c => c.Type == ClaimTypes.Email)
                .Value;
        }
    }
}
