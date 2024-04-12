namespace diploma.Projects.API.Infrastructure.Exceptions;

public abstract class NotFoundException : Exception
{
    protected NotFoundException(string message)
    : base(message)
    { }
}