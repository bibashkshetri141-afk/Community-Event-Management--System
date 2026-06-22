namespace Client.Services
{
    public class AuthService
    {
        private bool _isAdminLoggedIn = false;

        private const string AdminUsername = "bibash";
        private const string AdminPassword = "bibash123";

        public bool IsAdminLoggedIn => _isAdminLoggedIn;

        // This event fires when login state changes
        public event Action? OnAuthStateChanged;

        public bool Login(string username, string password)
        {
            if (username == AdminUsername && password == AdminPassword)
            {
                _isAdminLoggedIn = true;
                OnAuthStateChanged?.Invoke();
                return true;
            }
            return false;
        }

        public void Logout()
        {
            _isAdminLoggedIn = false;
            OnAuthStateChanged?.Invoke();
        }
    }
}