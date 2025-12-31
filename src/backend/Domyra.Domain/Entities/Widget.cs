namespace Domyra.Domain.Entities;

using Domyra.Domain.Enums; 

public class Widget : BaseEntity
{
    public required string Alias { get; set; }
    public string? Description { get; set; }
    public WidgetState State { get; set; }
    public string? Kind { get; set; }
    public string? ElementName { get; set; }    // for web components
    public string? ScriptUrl { get; set; }      // for web components
    public string? Html { get; set; }           // for static widgets
    public int Cols { get; set; } = 1;
    public int Rows { get; set; } = 1;
}