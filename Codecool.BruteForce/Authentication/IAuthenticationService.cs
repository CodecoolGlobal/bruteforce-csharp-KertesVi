namespace Codecool.BruteForce.Authentication;

public interface IAuthenticationService
{
    bool Authenticate(string userName, string password);
}
