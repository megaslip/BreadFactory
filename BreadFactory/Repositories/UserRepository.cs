using BreadFactory.Data;
using BreadFactory.Models;
using System.Linq;

namespace BreadFactory.Repositories
{
    public interface IUserRepository
    {
        User GetUser(string username, string password);
        void AddUser(User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly BreadFactoryContext _context;

        public UserRepository(BreadFactoryContext context)
        {
            _context = context;
        }

        public User GetUser(string username, string password)
        {
            return _context.Users
                .FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}