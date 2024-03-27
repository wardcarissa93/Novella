using Novella.Data;
using Novella.ViewModels;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Novella.Data;
using Novella.EfModels;
using Novella.ViewModels;
using System.Linq;


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
    public class OtherUserRepo
    {
        private readonly NovellaContext _db;
        public OtherUserRepo(NovellaContext db)
        {
            _db = db;
        }
        public UserAccountVM GetUserByEmail(string userEmail)
        {
            var user = _db.UserAccounts.FirstOrDefault(u => u.PkUserId == userEmail);

            if (user == null)
            {
                // Handle the case where user is not found in the database
                // For example, return null or throw an exception
                return null;
            }

            // Map UserAccount to UserAccountVM
            var userAccountVM = new UserAccountVM
            {
                UsertId = user.PkUserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
            };
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n");
            Console.WriteLine(userAccountVM.FirstName);
            return userAccountVM;
        }
        public void UpdateUserInfo(string userId, string newFirstName, string newLastName, string newPhoneNumber)
        {
            var user = _db.UserAccounts.FirstOrDefault(u => u.PkUserId == userId);

            if (user != null)
            {
                user.FirstName = newFirstName;
                user.LastName = newLastName;
                user.PhoneNumber = newPhoneNumber;
                _db.SaveChanges();
            }
            // Handle the case where user is not found if needed
        }

    }
}
