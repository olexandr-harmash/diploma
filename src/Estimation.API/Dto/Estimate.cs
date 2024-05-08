namespace diploma.Estimation.API.Dto;

public record EstimateDto(Guid Id, Guid ProjectId, string Name, string Description, string CreatedBy);

