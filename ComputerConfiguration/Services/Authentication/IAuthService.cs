using ComputerConfiguration.DTO;
using ComputerConfiguration.Models.Authentication;
using ComputerConfiguration.Models.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Services.Authentication
{
    public interface IAuthService
    {
        User? CurrentUser { get; }
        bool IsAuthenticated { get; }
        Task<IResult> RegisterAsync(UserRegistrationDTO userData);
        Task<IResult> LoginAsync(UserEntryDTO userData);
        void Logout();
        event EventHandler<User?> UserChanged;
    }
}
