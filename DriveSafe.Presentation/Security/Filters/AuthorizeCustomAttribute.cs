using DriveSafe.Domain.Publishing.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DriveSafe.Presentation.Publishing.Filters;

public class AuthorizeCustomAttribute : Attribute, IAsyncAuthorizationFilter
{
    private readonly string[] _roles;

    public AuthorizeCustomAttribute(params string[] roles)
    {
        _roles = roles;
    }
    
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.Items["User"] as User;
        
        if (user == null || !_roles.Any(role => user.Type.Contains(role)))
        {
            context.Result = new UnauthorizedResult();
        }
    }
}