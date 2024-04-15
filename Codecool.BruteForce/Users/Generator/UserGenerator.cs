using Codecool.BruteForce.Passwords.Generator;

namespace Codecool.BruteForce.Users.Generator;

public class UserGenerator : IUserGenerator
{
    private readonly List<IPasswordGenerator> _passwordGenerators;

    private int _userCount;

    public UserGenerator(IEnumerable<IPasswordGenerator> passwordGenerators)
    {
        _passwordGenerators = passwordGenerators.ToList();
    }

    public IEnumerable<(string userName, string password)> Generate(int count, int maxPasswordLength)
    {
        return null;
    }

    private IPasswordGenerator GetRandomPasswordGenerator()
    {
        return null;
    }

    private static int GetRandomPasswordLength(int maxPasswordLength)
    {
        return 0;
    }
}
