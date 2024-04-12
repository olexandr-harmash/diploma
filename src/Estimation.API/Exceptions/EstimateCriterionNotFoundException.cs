using diploma.Shared.Exceptions;

namespace diploma.Estimation.API.Exceptions;

public class EstimateCriterionNotFoundException : NotFoundException
{
    public EstimateCriterionNotFoundException(Guid estimateId, Guid criteriaId)
        : base($"The relation between estimate and criterion with: estimate id - {estimateId} and criteria id - {criteriaId} doesn't exist in the database.") { }

    public EstimateCriterionNotFoundException(Guid estimateId, IEnumerable<Guid> criterionIds)
        : base($"The relation between estimate and criterion with: estimate id - {estimateId} and criterion ids {criterionIds} doesn't exist in the database.") { }
}
