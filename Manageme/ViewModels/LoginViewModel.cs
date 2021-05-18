namespace Manageme.ViewModels
{
    public class LoginViewModel
    {
        public string Token { get; set; }
        public UserViewModel UserViewModel { get; set; }

        public LoginViewModel(string token, UserViewModel userViewModel)
        {
            Token = token;
            UserViewModel = userViewModel;
        }
    }
}
