namespace ECommerceDemo.Services
{
    public class GetCurrentUser : IGetCurrentUser
    {
        private readonly IHttpContextAccessor _accessor;

        public GetCurrentUser(IHttpContextAccessor accessor)
        => _accessor = accessor;

        public string? UserName => _accessor.HttpContext.User.Identity.Name;
    }
}
