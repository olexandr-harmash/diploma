using AutoMapper;

namespace diploma.Estimation.API.Dto;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<Criterion, CriterionDto>();
        CreateMap<CriterionDtoForCreate, Criterion>();
        CreateMap<CriterionDtoForCreate, Criterion>();

        CreateMap<Estimate, EstimateDto>();
        CreateMap<EstimateDtoForCreate, Estimate>();
        CreateMap<EstimateDtoForUpdate, Estimate>();
    }
}
