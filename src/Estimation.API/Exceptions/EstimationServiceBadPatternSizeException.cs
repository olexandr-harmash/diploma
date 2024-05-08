namespace diploma.Estimation.API.Exceptions;

public class EstimationServiceBadPatternSizeException : Exception
{
    public EstimationServiceBadPatternSizeException(Guid estimateId) : base($"Failed match estimate to pattern with estimateId: {estimateId} due to criterions mismatch.") { }
}
