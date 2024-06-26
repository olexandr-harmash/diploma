﻿using System.ComponentModel.DataAnnotations;

namespace diploma.Estimation.API.Dto;

public record CriterionDtoForCreate
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; init; }
    [Required(ErrorMessage = "Description is required")]
    public string Description { get; init; }
}
