using System.ComponentModel.DataAnnotations;

namespace diploma.Estimation.API.Dto;

public record EstimateDtoForCreate
{
    public Guid ProjectId { get; init; }
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; init; }
    [Required(ErrorMessage = "Description is required")]
    public string Description { get; init; }
    [Required(ErrorMessage = "CreatedBy is required")]
    public string CreatedBy { get; init; }
}
