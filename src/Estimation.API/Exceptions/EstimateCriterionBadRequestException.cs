using diploma.Shared.Exceptions;

namespace diploma.Estimation.API.Exceptions;

public class EstimateCriterionBadRequestException : BadRequestException
{
    public EstimateCriterionBadRequestException(Guid estimateId, Guid criteriaId)
       : base($"The relation between estimate and criterion with: estimate id - {estimateId} and criteria id - {criteriaId} already exist in the database.") { }
}
