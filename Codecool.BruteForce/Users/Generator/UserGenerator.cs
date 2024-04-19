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
        for (int i = 1; i <= count; i++)
        {
            var userName = GenerateUserName(i);
            var passwordGenerator = GetRandomPasswordGenerator();
            var password = passwordGenerator.Generate(GetRandomPasswordLength(maxPasswordLength));
            generatedUsers.Add((userName, password));
        }
        return generatedUsers;
    }

    private static string GenerateUserName(int i)
    {
          return $"user{i}";
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
