namespace Pladau.Models;

public class University: BaseModel
{
    public string? Name { get; set; }
    public string? LogoUrl { get; set; }
    public List<string>? FacultyIds { get; set; }
}
