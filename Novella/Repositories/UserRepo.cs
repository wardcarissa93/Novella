using Novella.Data;
using Novella.ViewModels;

namespace Novella.Repositories
{
    public class UserRepo
    {
        private readonly ApplicationDbContext _context;

        public UserRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public List<UserVM> GetAllUsers()
        {
            var users = _context.Users.Select(u => new UserVM { Email = u.Email }).ToList();
            return users;
        }
    }
}
