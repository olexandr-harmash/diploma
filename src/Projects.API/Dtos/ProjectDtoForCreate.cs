using System.ComponentModel.DataAnnotations;

namespace diploma.Projects.API.Dtos;

public class ProjectDtoForCreate
{
    [Required(ErrorMessage = "ProjectModel name is a required field.")]
    public string? Name { get; init; }

    [Required(ErrorMessage = "ProjectModel link is a required field.")]
    public string? Link { get; init; }

    [Required(ErrorMessage = "ProjectModel description is a required field.")]
    public string? Description { get; init; }
}

