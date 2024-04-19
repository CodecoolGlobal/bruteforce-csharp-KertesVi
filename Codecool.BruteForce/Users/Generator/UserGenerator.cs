using Codecool.BruteForce.Passwords.Generator;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Codecool.BruteForce.Users.Generator;

public class UserGenerator : IUserGenerator
{
    private readonly List<IPasswordGenerator> _passwordGenerators;
    private readonly Random _random = new();

  
    public UserGenerator(IEnumerable<IPasswordGenerator> passwordGenerators)
    {
        _passwordGenerators = passwordGenerators.ToList();
        
    }

    public IEnumerable<(string userName, string password)> Generate(int count, int maxPasswordLength)
    {
        var generatedUsers = new List<(string userName, string password)>();
        for (int i = 0; i < count; i++)
        {
            var userName = GenerateUserName();
            var passwordGenerator = GetRandomPasswordGenerator();
            var password = passwordGenerator.Generate(GetRandomPasswordLength(maxPasswordLength));
            generatedUsers.Add((userName, password));
        }
        return generatedUsers;
    }

    private string GenerateUserName()
    {
       return "User" + _random.Next(1000, 10000);
    }

    private IPasswordGenerator GetRandomPasswordGenerator()
    {
        var randomIndex = _random.Next(_passwordGenerators.Count);
        return _passwordGenerators[randomIndex];
    }

    private int GetRandomPasswordLength(int maxPasswordLength)
    {
        return _random.Next(1, maxPasswordLength + 1);
    }
}
