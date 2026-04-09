using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models.UI
{
    public class Error : IResult
    {
        public string Source { get; set; }
        public string Message { get; set; }
        public Error(string source, string message)
        {
            Source = source;
            Message = message;
        }
    }
}
