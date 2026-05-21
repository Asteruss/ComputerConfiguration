using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models.UI;

public static class ResultExtension
{
    public static bool IsError(this IResult result) => result is Error;
    public static bool IsSuccess(this IResult result) => result is Success;

    public static Error AsError(this IResult result) => result as Error;
    public static Success AsSuccess(this IResult result) => result as Success;
    public static OrderSuccess AsOrderSuccess(this IResult result) => result as OrderSuccess;

}
