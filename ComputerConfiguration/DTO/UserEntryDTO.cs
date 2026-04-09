using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.DTO
{
    public class UserEntryDTO
    {
        public string Email { get; set; }
        public SecureString Password { get; set; }
        
    }
}
