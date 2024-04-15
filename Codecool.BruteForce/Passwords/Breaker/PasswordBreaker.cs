namespace Codecool.BruteForce.Passwords.Breaker;

public class PasswordBreaker : IPasswordBreaker
{
    public IEnumerable<string> GetCombinations(int passwordLength)
    {
        return null;
    }

    private static IEnumerable<string> GetAllPossibleCombos(IEnumerable<IEnumerable<string>> strings)
    {
        IEnumerable<string> combos = new[] { "" };

        combos = strings
            .Aggregate(combos, (current, inner) => current.SelectMany(c => inner, (c, i) => c + i));

        return combos;
    }
}
