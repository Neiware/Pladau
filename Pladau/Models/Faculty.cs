namespace Pladau.Models;

public class Faculty: BaseModel
{
    public string? Name { get; set; }
    public string? LogoUrl { get; set; }
    public List<string>? CarrerIds { get; set; }
}
