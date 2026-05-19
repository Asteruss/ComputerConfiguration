using ComputerConfiguration.DTO;
using ComputerConfiguration.Models.Build;

namespace ComputerConfiguration.Mappers;

public interface IComputerBuildMapper
{
    public ComputerBuild Map(ComputerBuildDTO dto);

}
