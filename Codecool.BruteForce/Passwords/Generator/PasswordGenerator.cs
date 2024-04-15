using Codecool.BruteForce.Passwords.Model;

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
        return null;
    }

    private AsciiTableRange GetRandomCharacterSet()
    {
        return null;
    }

    private static char GetRandomCharacter(AsciiTableRange characterSet)
    {
        return (char)1;
    }
}
