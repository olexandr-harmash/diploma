using diploma.Shared.Exceptions;

namespace diploma.Estimation.API.Exceptions;

public class EstimateNotFoundException : NotFoundException
{
    public EstimateNotFoundException(Guid id)
        : base($"The estimate with id: {id} doesn't exist in the database.") { }

    public EstimateNotFoundException(IEnumerable<Guid> ids)
        : base($"The estimates with ids: {ids} doesn't exist in the database.") { }
}
