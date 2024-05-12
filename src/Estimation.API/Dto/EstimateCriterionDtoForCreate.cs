using System.ComponentModel.DataAnnotations;

namespace diploma.Estimation.API.Dto;

public record EstimateCriterionDtoForCreate
{
    [Range(-1, 1, ErrorMessage = "Value must be in a range")]
    public double Value { get; init; }
}
