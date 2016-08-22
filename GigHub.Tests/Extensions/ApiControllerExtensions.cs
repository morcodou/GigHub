using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;

namespace GigHub.Tests.Extensions
{
    public static class ApiControllerExtensions
    {
        public static void MockCurrentUser(this ApiController controller, string userid, string username)
        {

            var identity = new GenericIdentity(userid);
            identity
                .AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", userid));

            identity
                .AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", userid));

            var principal = new GenericPrincipal(identity, null);

            controller.User = principal;
        }
    }
}
