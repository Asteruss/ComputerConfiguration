namespace ComputerConfiguration.Models.UI;

public interface IResult
{
    string Source { get; }
    string Block { get; }
    string Message { get; }
}
