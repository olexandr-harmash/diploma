﻿namespace diploma.Estimation.API.Dto;

public class EstimateDtoForCreate
{
    public Guid ProjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CreatedBy { get; set; }
}
