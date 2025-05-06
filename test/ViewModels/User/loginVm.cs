using test.Services.User;

namespace test.ViewModels.User
{
    public class loginVm
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        // Using UserDto to store user data
        public UserDto User { get; set; }
    }
}
