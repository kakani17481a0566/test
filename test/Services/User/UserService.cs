using Microsoft.EntityFrameworkCore;
using test.Data;
using test.Models;
using test.ViewModels.User;

namespace test.Services.User
{
    public class UserService : IUserService
    {

        private readonly ApplicationDbContext _context;

        // Constructor to inject ApplicationDbContext
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Check if a Role exists by ID
        public async Task<bool> RoleExistsAsync(int roleId)
        {
            return await _context.Roles.AnyAsync(r => r.Id == roleId);
        }

        // Get a list of all users
        public async Task<List<UserViewModel>> GetUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Role)  // Including related Role entity
                .Select(user => new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                    LoginId = user.LoginId,
                    RoleId = user.RoleId,
                    RoleName = user.Role.Name  // Mapping Role Name
                })
                .ToListAsync();  // Asynchronously fetching the list of users
        }

        // Get a specific user by ID
        public async Task<UserViewModel> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Role)  // Including related Role entity
                .Where(u => u.Id == id)
                .Select(user => new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                    LoginId = user.LoginId,
                    RoleId = user.RoleId,
                    RoleName = user.Role.Name  // Mapping Role Name
                })
                .FirstOrDefaultAsync();  // Fetching the user by ID
        }

        // Implement the method to create a user
        public async Task<UserModel> CreateUserAsync(UserCreateViewModel userCreateViewModel)
        {
            var user = new UserModel
            {
                FirstName = userCreateViewModel.FirstName,
                LastName = userCreateViewModel.LastName,
                Email = userCreateViewModel.Email,
                Phone = userCreateViewModel.Phone,
                LoginId = userCreateViewModel.LoginId,
                Password = userCreateViewModel.Password, // Plain-text password
                RoleId = userCreateViewModel.RoleId
            };

            _context.Users.Add(user);  // Add user to context
            await _context.SaveChangesAsync();  // Save changes to database

            // Return the created user with the auto-generated ID
            return user;
        }



        // Update an existing user
        public async Task<bool> UpdateUserAsync(int id, UserViewModel userViewModel)
        {
            var user = await _context.Users.FindAsync(id);  // Find user by ID
            if (user == null) return false;  // If not found, return false

            user.FirstName = userViewModel.FirstName;
            user.LastName = userViewModel.LastName;
            user.Email = userViewModel.Email;
            user.Phone = userViewModel.Phone;
            user.LoginId = userViewModel.LoginId;

            if (!string.IsNullOrEmpty(userViewModel.Password))
            {
                user.Password = userViewModel.Password;  // Update password if provided
            }

            user.RoleId = userViewModel.RoleId;

            _context.Users.Update(user);  // Update the user
            await _context.SaveChangesAsync();  // Save changes
            return true;
        }

        // Delete a user by ID
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);  // Find user by ID
            if (user == null) return false;  // If not found, return false

            _context.Users.Remove(user);  // Remove user from context
            await _context.SaveChangesAsync();  // Save changes to the database
            return true;
        }

        // Handle user login with plain-text password
        public async Task<loginVm> LoginAsync(string identifier, string password)
        {
            var loginvm = new loginVm();

            // Find the user by identifier (email, phone, or login ID)
            var user = await _context.Users
                .Include(u => u.Role)  // Include Role to retrieve role information
                .FirstOrDefaultAsync(u =>
                    u.Email == identifier ||
                    u.Phone == identifier ||
                    u.LoginId == identifier);

            // If user not found
            if (user == null)
            {
                loginvm.Message = "User not found";
                loginvm.Status = false;
            }
            // If password is incorrect
            else if (user.Password != password)
            {
                loginvm.Message = "Invalid password";
                loginvm.Status = false;
            }
            else
            {
                loginvm.Message = "Login successful";
                loginvm.Status = true;

                // Map UserModel to UserDto
                loginvm.User = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName, // Using FirstName from UserModel
                    LastName = user.LastName,   // Using LastName from UserModel
                    Email = user.Email,
                    Phone = user.Phone,


                };
            }

            return loginvm;
        }

    }
}
