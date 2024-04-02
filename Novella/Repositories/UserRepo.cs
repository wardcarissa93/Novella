using Novella.Data;
using Novella.ViewModels;
using Novella.EfModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Novella.Repositories
{
    public class UserRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public UserRepo(ApplicationDbContext context,
                        UserManager<IdentityUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public List<UserVM> GetAllUsers()
        {
            var users = _context.Users.Select(u => new UserVM { Email = u.Email }).ToList();
            return users;
        }

        public async Task<string> GetUserNameByUserIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                return user.UserName;
            }

            return null; // Handle the case where user is not found
        }
    }
    public class OtherUserRepo
    {
        private readonly NovellaContext _db;
        public OtherUserRepo(NovellaContext db)
        {
            _db = db;
        }
        public class RegistrationModel
        {
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }
            public string PaypalAccount { get; set; }
        }
        public UserAccountVM GetUserByEmail(string userEmail)
        {
            var user = _db.UserAccounts.FirstOrDefault(u => u.PkUserId == userEmail);

            if (user == null)
            {
                return null;
            }

            // Map UserAccount to UserAccountVM
            var userAccountVM = new UserAccountVM
            {
                UserId = user.PkUserId,
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
        }

        public async Task RegisterUserAsync(RegistrationModel model)
        {
            // Check if the UserAccount already exists
            var existingUserAccount = _db.UserAccounts.FirstOrDefault(u => u.PkUserId == model.Email);
            if (existingUserAccount == null)
            {
                // Create a new UserAccount entry
                var userAccount = new UserAccount
                {
                    PkUserId = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Role = "Customer",
                    PhoneNumber = model.PhoneNumber.ToString(),
                    PaypalAccount = model.PaypalAccount,
                };

                // Add the UserAccount entry to the database
                _db.UserAccounts.Add(userAccount);
                await _db.SaveChangesAsync();
            }
        }
    }
}
