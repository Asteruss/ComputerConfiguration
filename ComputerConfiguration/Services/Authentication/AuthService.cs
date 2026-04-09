using ComputerConfiguration.DB;
using ComputerConfiguration.DTO;
using ComputerConfiguration.Models.Authentication;
using ComputerConfiguration.Models.UI;
using Microsoft.EntityFrameworkCore;

namespace ComputerConfiguration.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly ComputerConfigurationDBContext _db;
        public AuthService(ComputerConfigurationDBContext db)
        {
            _db = db;
        }

        private User? _currentUser;
        public User? CurrentUser
        {
            get => _currentUser; set
            {
                _currentUser = value;
                OnUserChanged();
            }
        }
        public bool IsAuthenticated => _currentUser != null;
        public event EventHandler<User?> UserChanged;
        public void OnUserChanged()
        {
            UserChanged?.Invoke(this, CurrentUser);
        }
        public async Task<IResult> RegisterAsync(UserEntryDTO userData)
        {
            if (await _db.Users.AnyAsync(u => u.Email == userData.Email))
                return new Error("Auth", "Пользователь не найден");
            User user = new()
            {
                Email = userData.Email,
                RegistrationDate = DateTime.Now,
                RoleId = 1,
                PasswordHash = HashPassword(userData.Password.ToString())
            };
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return new Success("Auth", "Пользователь успешно создан");
        }
        public async Task<IResult> LoginAsync(UserEntryDTO userData)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == userData.Email);
            if (user == null)
                return new Error("Auth", "Пользователь не найден");
            if (!VerifyPassword(userData.Password.ToString(), user.PasswordHash))
                return new Error("Auth", "Введен неверный пароль");
            CurrentUser = user;
            return new Success("Auth", "Вход в аккаунт совершен");
        }
        public void Logout()
        {
            CurrentUser = null;
        }
        private string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
        private bool VerifyPassword(string userPassword, string truePassword) => BCrypt.Net.BCrypt.Verify(userPassword, truePassword);


    }
}
