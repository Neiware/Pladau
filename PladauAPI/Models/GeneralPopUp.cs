namespace PladauAPI.Models;

public class GeneralPopUp:BaseModel
{
    public string? NameOfPopUp { get; set; }
    public string? DetailsOfPopUp { get; set; }
    public string? Link { get; set; }
    public List<object>? Items { get; set; }
}
