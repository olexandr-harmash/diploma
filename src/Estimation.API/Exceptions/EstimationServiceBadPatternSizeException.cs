namespace diploma.Estimation.API.Exceptions;

public class EstimationServiceBadPatternSizeException : Exception
{
    public EstimationServiceBadPatternSizeException() : base($"Failed match estimate to pattern due to criterions mismatch.") { }
}
