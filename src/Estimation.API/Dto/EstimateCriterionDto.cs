namespace diploma.Estimation.API.Dto;

public record EstimateCriterionDto(Guid Id, Guid EstimateId, Guid CriterionId, double Value);
