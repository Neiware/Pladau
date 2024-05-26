
namespace Pladau.Models;

public class Carrer:BaseModel
{
    public string? Name  { get; set; }
    public List<string>? SubjectIds { get; set; }
    public string? ImgCurriculumUrl { get; set; }
    public float DurationInYears { get; set; }
    public string? LogoUrl { get; set; }
    // semestre, tetramestre, trimestre... etc
    public string? AcademicTermType { get; set; }
}