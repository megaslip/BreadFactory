using BreadFactory.Data;
using BreadFactory.Models;
using System.Linq;

namespace BreadFactory.Repositories
{
    public class UserRepository
    {
        public User Authenticate(string username, string password)
        {
            using (var context = new BreadFactoryContext())
            {
                var user = context.Users
                    .FirstOrDefault(u => u.Username == username);

                if (user == null)
                    return null;

                // В реальном приложении добавьте проверку пароля:
                // if (!VerifyPassword(password, user.PasswordHash, user.Salt))
                //     return null;

                return user;
            }
        }
    }
}