using diploma.Projects.API.Infrastructure.Exceptions;

namespace diploma.Projects.API.Exceptions;

public class ProjectNotFoundException : NotFoundException
{
    public ProjectNotFoundException(Guid id)
        : base($"The project with id: {id} doesn't exist in the database.") { }

    // For the "get collection query" mistmatches.
    public ProjectNotFoundException(IEnumerable<Guid> ids)
        : base($"The projects with some ids doesn't exist in the database.") { }
}
