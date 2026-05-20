using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models.UI;

public class Success : IResult
{
    public string Source { get; set; }
    public string Block { get; set; }
    public string Message { get; set; }
    public Success(string source, string block, string message)
    {
        Source = source;
        Block = block;
        Message = message;
    }
}
