namespace GroepsProject1._3Backend.WebApi.Services
{
    public interface IAuthenticationService
    {            /// <summary>
                 /// Returns the user name of the authenticated user
                 /// </summary>
                 /// <returns></returns>
        string? GetCurrentAuthenticatedUserId();
    }
}