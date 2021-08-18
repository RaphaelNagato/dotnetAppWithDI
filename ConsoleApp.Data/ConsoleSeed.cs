using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp.Data
{
    public class ConsoleSeed
    {
        public static async Task SeedAsync(ConsoleContext context)
        {
            if (!context.Users.Any())
            {
                try
                {
                    var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    var filePath = buildDir + @"/Data/Users.json";
                    var usersData = await File.ReadAllTextAsync(filePath);
                    var users = JsonSerializer.Deserialize<IReadOnlyList<User>>(usersData);

                    await context.Users.AddRangeAsync(users);
                    await context.SaveChangesAsync();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}