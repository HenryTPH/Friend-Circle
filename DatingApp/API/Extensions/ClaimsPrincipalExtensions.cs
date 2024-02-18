namespace API.Extensions;

public static class ClaimsPrincipleExtensions
{
    public static string GetUsername(this ClaimsPrinciple user)
    {
        // The User in the system.Security.Claims.Claims for user associated with the executing action
        // In FindFirst method having ArgumentNullException will throw exception if the argument does not exist.
        return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}