namespace diploma.Projects.API.Infrastructure.Exceptions;

public abstract class BadRequestException : Exception
{
    protected BadRequestException(string message)
    : base(message)
    {
    }
}