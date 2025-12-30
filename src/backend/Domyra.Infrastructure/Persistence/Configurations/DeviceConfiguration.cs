namespace Domyra.Infrastructure.Persistence.Configurations;

using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DeviceConfiguration : BaseConfiguration<Device>
{
    protected override string TableName => "devices";

    protected override void TableConfigure(EntityTypeBuilder<Device> builder)
    { 
        // only specific table columns that the BaseEntity doesnt include.
        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("name");
        
        builder.Property(d => d.Description)
            .HasMaxLength(500)
            .HasColumnName("description");

        builder.Property(d => d.State)
            .IsRequired()
            .HasDefaultValue(DeviceState.Inactive)
            .HasColumnName("state");
    }
}