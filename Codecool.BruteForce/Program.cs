using Codecool.BruteForce.Authentication;
using Codecool.BruteForce.Passwords.Breaker;
using Codecool.BruteForce.Passwords.Generator;
using Codecool.BruteForce.Passwords.Model;
using Codecool.BruteForce.Users.Generator;
using Codecool.BruteForce.Users.Model;
using Codecool.BruteForce.Users.Repository;
using System.Diagnostics;

namespace Codecool.BruteForce;

internal static class Program
{
    private static readonly AsciiTableRange LowercaseChars = new(97, 122);
    private static readonly AsciiTableRange UppercaseChars = new(65, 90);
    private static readonly AsciiTableRange Numbers = new(48, 57);

    public static void Main()
    {
        string workDir = AppDomain.CurrentDomain.BaseDirectory;
        var dbFile = $"{workDir}\\Resources\\Users.db";
        var crackedDbFile = $"{workDir}\\Resources\\CrackedUsers.db";

        IUserRepository crackedUserRepository = new CrackedUserRepository(crackedDbFile);
        crackedUserRepository.DeleteAll();

        IUserRepository userRepository = new UserRepository(dbFile);
        userRepository.DeleteAll();

        int userCount = 5;
        int maxPwLength = 2;

        var passwordGenerators = CreatePasswordGenerators();
        IUserGenerator userGenerator = new UserGenerator(passwordGenerators);
       
        AddUsersToDb(userCount, maxPwLength, userGenerator, userRepository);

        Console.WriteLine($"Database initialized with {userCount} users; maximum password length: {maxPwLength}");

        IAuthenticationService authenticationService = new AuthenticationService(userRepository);
        List<User> crackedUserList = BreakUsers(userCount, maxPwLength, authenticationService);

        AddCrackedUsersToDb(crackedUserRepository, crackedUserList);

        Console.WriteLine($"Press any key to exit.");

        Console.ReadKey();
    }

    private static void AddUsersToDb(int count, int maxPwLength, IUserGenerator userGenerator,
        IUserRepository userRepository)
    {
        userRepository.DeleteAll();

        foreach (var (userName, password) in userGenerator.Generate(count, maxPwLength))
        {
           try
            {                
                userRepository.Add(userName, password);
                Console.WriteLine($"Added user: {userName} + password: {password}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add user: {userName}. Error: {ex.Message}");
            }
        }
    }

    private static void AddCrackedUsersToDb(IUserRepository crackedUserRepository, List<User> crackedUserList)
    {
        crackedUserRepository.DeleteAll();

        foreach (var (id, userName, password) in crackedUserList)
        {
            Console.WriteLine($"{userName}, password: {password}");
           
            try
            {
                crackedUserRepository.Add(userName, password);
                Console.WriteLine($"Added user: {userName} + password: {password}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add user: {userName}. Error: {ex.Message}");
            }
        }
    }
    

    private static IEnumerable<IPasswordGenerator> CreatePasswordGenerators()
    {
        IPasswordGenerator lowercasePwGen = new PasswordGenerator(LowercaseChars);
        IPasswordGenerator uppercasePwGen = new PasswordGenerator(LowercaseChars, UppercaseChars);
        IPasswordGenerator numbersPwGen = new PasswordGenerator(Numbers, LowercaseChars, UppercaseChars); //lowercase + uppercase + numbers
      
        return new List<IPasswordGenerator>
        {
            lowercasePwGen, uppercasePwGen, numbersPwGen
        };
    }

    private static List<User> BreakUsers(int userCount, int maxPwLength, IAuthenticationService authenticationService)
    {
        List<User> crackedUsers = new List<User>();    

        var passwordBreaker = new PasswordBreaker(Numbers, LowercaseChars, UppercaseChars);
        Console.WriteLine("Initiating password breaker...\n");

        for (int i = 1; i <= userCount; i++)
        {
            var userName = $"user{i}";
            for (int j = 1; j <= maxPwLength; j++)
            {
                Console.WriteLine($"Trying to break {userName} with all possible password combinations with length = {j}... ");

                Stopwatch stopWatch = Stopwatch.StartNew();
                stopWatch.Start();

                //Get all pw combinations
                var pwCombinations =passwordBreaker.GetCombinations(j);
                bool broken = false;
               
                foreach (var pw in pwCombinations)
                {
                   
                    if (authenticationService.Authenticate(userName, pw))
                    {
                        stopWatch.Stop();
                        TimeSpan ts = stopWatch.Elapsed;

                        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                            ts.Hours, ts.Minutes, ts.Seconds,
                            ts.Milliseconds / 10);
                        Console.WriteLine($"Password cracked for {userName}: {pw}, Elapsed Time: {elapsedTime}");
                        broken = true;
                        crackedUsers.Add(new User(i, userName,pw));
                        break;
                    }
                    
                }
                if (broken)
                {
                    break;
                }
              
            }
        }
       return crackedUsers;
    }
}
