using System.Security;

namespace ComputerConfiguration.DTO;

public class UserRegistrationDTO : UserEntryDTO
{
    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    private SecureString _passwordRepeat;
    public SecureString PasswordRepeat
    {
        get => _passwordRepeat;
        set
        {
            _passwordRepeat = value;
            OnPropertyChanged();
        }
    }
}