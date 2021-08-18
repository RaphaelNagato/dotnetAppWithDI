using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp.Data
{
    public interface IUserRepository
    {
        Task<User> GetUserById(string id);
        Task<IReadOnlyList<User>> GetUsers();
    }
}