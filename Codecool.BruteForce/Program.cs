using Codecool.BruteForce.Authentication;
using Codecool.BruteForce.Passwords.Breaker;
using Codecool.BruteForce.Passwords.Generator;
using Codecool.BruteForce.Passwords.Model;
using Codecool.BruteForce.Users.Generator;
using Codecool.BruteForce.Users.Repository;

namespace Codecool.BruteForce;

internal static class Program
{
    private static readonly AsciiTableRange LowercaseChars = new(97, 122);
    private static readonly AsciiTableRange UppercaseChars = new(65, 90);
    private static readonly AsciiTableRange Numbers = new(48, 57);

    public static void Main(string[] args)
    {
        string workDir = AppDomain.CurrentDomain.BaseDirectory;
        var dbFile = $"{workDir}\\Resources\\Users.db";

        IUserRepository userRepository = new UserRepository(dbFile);
        userRepository.DeleteAll();

        var passwordGenerators = CreatePasswordGenerators();
        IUserGenerator userGenerator = new UserGenerator(passwordGenerators);
        int userCount = 10;
        int maxPwLength = 4;

        AddUsersToDb(userCount, maxPwLength, userGenerator, userRepository);

        Console.WriteLine($"Database initialized with {userCount} users; maximum password length: {maxPwLength}");

        IAuthenticationService authenticationService = null;
        //BreakUsers(userCount, maxPwLength, authenticationService);

        Console.WriteLine($"Press any key to exit.");

        Console.ReadKey();
    }

    private static void AddUsersToDb(int count, int maxPwLength, IUserGenerator userGenerator,
        IUserRepository userRepository)
    {
    }

    private static IEnumerable<IPasswordGenerator> CreatePasswordGenerators()
    {
        var lowercasePwGen = new PasswordGenerator(LowercaseChars);
        var uppercasePwGen = new PasswordGenerator(LowercaseChars, UppercaseChars);
        IPasswordGenerator numbersPwGen = null; //lowercase + uppercase + numbers

        return new List<IPasswordGenerator>
        {
            lowercasePwGen, uppercasePwGen, numbersPwGen
        };
    }

    private static void BreakUsers(int userCount, int maxPwLength, IAuthenticationService authenticationService)
    {
        var passwordBreaker = new PasswordBreaker();
        Console.WriteLine("Initiating password breaker...\n");

        for (int i = 1; i <= userCount; i++)
        {
            var user = $"user{i}";
            for (int j = 1; j <= maxPwLength; j++)
            {
                Console.WriteLine($"Trying to break {user} with all possible password combinations with length = {j}... ");

                //start Stopwatch

                //Get all pw combinations
                var pwCombinations = Array.Empty<string>();
                bool broken = false;

                foreach (var pw in pwCombinations)
                {
                    //Try to authenticate the current user with pw
                    //If successful, stop the stopwatch, and print the pw and the elapsed time to the console, then go to next user
                }

                if (broken)
                {
                    break;
                }
            }
        }
    }
}
