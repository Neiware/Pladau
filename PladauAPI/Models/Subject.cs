using System.Security.Principal;

namespace PladauAPI.Models;

public class Subject:BaseModel
{
    public string? Name { get; set; }
    //implementar las recomendaciones de pdf, libros, videos de youtube, etc...
    public List<string>? GeneralPopUpIds { get; set; }
    public string? Description { get; set; }
    public int TotalTerms { get; set; }

    // online, presencial o hibrido
    public string? DeliveryType { get; set; }
    //es lista porque posiblemente existan mas de una materia que sea requisito.
    public List<string>? RequiredSubjectIds { get; set; }
    public string? LogoUrl { get; set; }
    public string? PopularPostId{ get; set; }
    public List<string>? CarrerIds { get; set; }
}