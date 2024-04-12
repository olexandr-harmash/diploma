namespace diploma.Estimation.API.Model;

public class EstimateCriterion
{
    public Guid Id { get; set; }
    public double Value {  get; set; }
    public Guid CriterionId { get; set; }
    public Criterion Criterion { get; set;}
    public Guid EstimateId { get; set; }
    public Estimate Estimate { get; set; }
}
