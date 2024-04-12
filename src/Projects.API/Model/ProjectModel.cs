namespace diploma.Projects.API.Model;

public class ProjectModel
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Link { get; set; }

    public string? Description { get; set; }

    public bool IsChecked { get; set; }
}

