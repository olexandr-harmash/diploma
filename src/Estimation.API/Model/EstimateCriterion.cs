using System.Text.Json.Serialization;

namespace diploma.Estimation.API.Model;

public class EstimateCriterion
{
    public Guid Id { get; set; }
    public double Value {  get; set; }
    public Guid CriterionId { get; set; }
    public Guid EstimateId { get; set; }

    [JsonIgnore]
    public Estimate Estimate { get; set; }

    [JsonIgnore]
    public Criterion Criterion { get; set;}
}
