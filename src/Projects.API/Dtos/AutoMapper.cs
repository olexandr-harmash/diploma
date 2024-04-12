using AutoMapper;

namespace diploma.Projects.API.Dtos;

public class AutoMapper : Profile
{

    public AutoMapper()
    {
        CreateMap<ProjectModel, ProjectDto>();

        CreateMap<ProjectDtoForCreate, ProjectModel>();

        CreateMap<ProjectDtoForUpdate, ProjectModel>();
    }
}