using Microsoft.AspNetCore.Mvc;

namespace Authentication.Models.Entities.Authorizations
{
    public class AuthorizationAttribute : TypeFilterAttribute
    {
        public AuthorizationAttribute()
            : base(typeof(AuthorizationFilter))
        {
            Arguments = new object[] { AuthorizationType.Any, Array.Empty<string>() };
        }

        public AuthorizationAttribute(AuthorizationType authorizationType)
            : base(typeof(AuthorizationFilter))
        {
            Arguments = new object[] { authorizationType, Array.Empty<string>() };
        }

        public AuthorizationAttribute(AuthorizationType authorizationType, params string[] roles)
           : base(typeof(AuthorizationFilter))
        {
            Arguments = new object[] { authorizationType, roles };
        }
    }
}
