namespace Codecool.BruteForce.Passwords.Breaker;

public interface IPasswordBreaker
{
    IEnumerable<string> GetCombinations(int passwordLength);
}
