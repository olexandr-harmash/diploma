using diploma.Shared.Exceptions;

namespace diploma.Estimation.API.Exceptions;

public class CriterionNotFoundException : NotFoundException
{
    public CriterionNotFoundException(Guid id)
        : base($"The criterion with id: {id} doesn't exist in the database.") { }

    public CriterionNotFoundException(IEnumerable<Guid> ids)
        : base($"The criterions with ids: {ids} doesn't exist in the database.") { }
}
