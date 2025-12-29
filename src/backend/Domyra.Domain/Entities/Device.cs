namespace Domyra.Domain.Entities;

using Domyra.Domain.Enums; 

public class Device : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DeviceState State { get; set; }
}