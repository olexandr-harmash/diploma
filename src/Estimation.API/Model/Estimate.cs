namespace diploma.Estimation.API.Model;

public class Estimate
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CreatedBy { get; set; }
    public IEnumerable<EstimateCriterion> Criterions { get; set; }

    public double[] GetCriterionValueCollection()
    {
        return Criterions.Select(x => x.Value).ToArray();
    }
}
