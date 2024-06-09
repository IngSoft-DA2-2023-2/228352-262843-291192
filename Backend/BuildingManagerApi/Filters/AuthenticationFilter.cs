using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerILogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;

namespace BuildingManagerApi.Filters
{
    public class AuthenticationFilter : Attribute, IAuthorizationFilter
    {
        private readonly RoleType _requiredRole;

        public AuthenticationFilter(RoleType requiredRole)
        {
            _requiredRole = requiredRole;
        }

        public AuthenticationFilter() { }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string headerToken = context.HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(headerToken))
            {
                SetUnauthorizedResult(context, "A token is required");
                return;
            }
            if (!Guid.TryParse(headerToken, out Guid token))
            {
                SetUnauthorizedResult(context, "Invalid token format");
                return;
            }

            IUserLogic userLogic = context.HttpContext.RequestServices.GetService(typeof(IUserLogic)) as IUserLogic;
            if (!userLogic.ExistsFromSessionToken(token))
            {
                SetUnauthorizedResult(context, "Invalid Token");
                return;
            }
            if (!_requiredRole.Equals(userLogic.RoleFromSessionToken(token)))
            {
                context.Result = new ObjectResult(new { ErrorMessage = "You don't have enough permissions to proceed further" })
                {
                    StatusCode = 403
                };
                return;
            }
        }

        private void SetUnauthorizedResult(AuthorizationFilterContext context, string errorMessage)
        {
            context.Result = new ObjectResult(new { ErrorMessage = errorMessage })
            {
                StatusCode = 401
            };
        }
    }
}