using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ConsoleContext _context;

        public UserRepository(ConsoleContext context)
        {
            _context = context;
        }
        public async  Task<User> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IReadOnlyList<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        
        
    }
}