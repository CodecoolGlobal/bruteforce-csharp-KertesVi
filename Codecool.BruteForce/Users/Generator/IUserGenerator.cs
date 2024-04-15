namespace Codecool.BruteForce.Users.Generator;

public interface IUserGenerator
{
    IEnumerable<(string userName, string password)> Generate(int count, int maxPasswordLength);
}
