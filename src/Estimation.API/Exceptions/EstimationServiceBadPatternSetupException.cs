namespace diploma.Estimation.API.Exceptions;

public class EstimationServiceBadPatternSetupException : Exception
{
    public EstimationServiceBadPatternSetupException(string rootPath) : base($"Failed to load or validate patterns from {rootPath}.") { }
}
