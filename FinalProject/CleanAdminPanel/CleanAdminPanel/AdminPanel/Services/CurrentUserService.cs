using Application.Common.Interfaces;
using System.Security.Claims;

namespace AdminPanel.Services;
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _contextAccessor;
    public CurrentUserService(IHttpContextAccessor contextAccessor)
    {
        this._contextAccessor = contextAccessor;
    } 
    public string? UserId => _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}
