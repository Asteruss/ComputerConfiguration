using ComputerConfiguration.Commands;
using System.Security;

namespace ComputerConfiguration.DTO;

public class UserEntryDTO : NotifyPropertyChanged
{
    private string _email;
    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }

    private SecureString _password;
    public SecureString Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
        }
    }
}
