using System;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApp.Data;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp.UI
{
    public class Startup : BackgroundService
    {
        private readonly IUserRepository _userRepository;

        public Startup(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Welcome to the Best App");
            stoppingToken.Register(() => Console.WriteLine("Bye Bye"));

            while (!stoppingToken.IsCancellationRequested)
            {
                bool done = false;
                while (!done)
                {
                    Console.WriteLine(@"Enter:
    '1' to Get User By Id
    '2' to Get All Users
    '3' to Quit");
                    var isValid = int.TryParse(Console.ReadLine(), out int result);
                    if (isValid)
                    {
                        switch (result)
                        {
                            case 1:
                                await DisplayUserByIdMenu();
                                break;
                            case 2:
                                await DisplayAllUsers();
                                break;
                            case 3:
                                done = true;
                                break;
                            default:
                                ResetWindow();
                                break;
                        }
                    }
                    else
                    {
                        ResetWindow();
                    }
                }

                await Task.Delay(2000, stoppingToken);
                break;
            }

            Console.WriteLine("Bye Bye");
        }

        async Task DisplayUserByIdMenu()
        {
            Console.WriteLine("Enter user id");
            var input = Console.ReadLine();
            var isNotValid = string.IsNullOrWhiteSpace(input);
            if (isNotValid)
            {
                ResetWindow();
                return;
            }

            var result = await _userRepository.GetUserById(input.Trim());
            if (result != null)
            {
                Console.WriteLine($"{result.Id}- {result.FirstName}, {result.LastName}");
            }
            else
            {
                ResetWindow();
            }
        }

        async Task DisplayAllUsers()
        {
            var users = await _userRepository.GetUsers();

            foreach (User user in users)
            {
                Console.WriteLine($"{user.Id}, {user.FirstName}, {user.LastName}");
            }

            Console.WriteLine("Press Enter to Continue");
            Console.ReadLine();

        }

        void ResetWindow()
        {
            Console.WriteLine("Invalid input, Press Enter to continue");
            Console.ReadLine();
            Console.Clear();
        }
    }
}