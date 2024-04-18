using Codecool.BruteForce.Passwords.Model;
using System.Text;

namespace Codecool.BruteForce.Passwords.Generator;

public class PasswordGenerator : IPasswordGenerator
{
    private static readonly Random Random = new();
    private readonly AsciiTableRange[] _characterSets;

    public PasswordGenerator(params AsciiTableRange[] characterSets)
    {
        _characterSets = characterSets;
    }

    public string Generate(int length)
    {
        if (length <= 3)
            throw new ArgumentException("Password length must be greater than 3 characters.");

        var passwordBuilder = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            AsciiTableRange randomChatSet = GetRandomCharacterSet();
            Char randomChar = GetRandomCharacter(randomChatSet);
            passwordBuilder.Append(randomChar);
        }
        return passwordBuilder.ToString();
    }

    private AsciiTableRange GetRandomCharacterSet()
    {
        var randomIndex = Random.Next(_characterSets.Length);
        return _characterSets[randomIndex];
    }

    private static char GetRandomCharacter(AsciiTableRange characterSet)
    {
        var randomCharCode = Random.Next(characterSet.Start, characterSet.End + 1);
        return (char)randomCharCode;
    }
}
