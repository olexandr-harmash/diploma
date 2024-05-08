namespace diploma.Estimation.API.Dto;

public record EstimateDtoForCreate(Guid ProjectId, string Name, string Description, string CreatedBy);