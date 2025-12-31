namespace Domyra.Infrastructure.Persistence.Configurations;

using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class WidgetConfiguration : BaseConfiguration<Widget>
{
    protected override string TableName => "widgets";

    protected override void TableConfigure(EntityTypeBuilder<Widget> builder)
    { 
        // only specific table columns that the BaseEntity doesnt include.
        builder.Property(d => d.Alias)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("alias");
        
        builder.HasKey(d => d.Alias);
        
        builder.Property(d => d.Description)
            .HasMaxLength(500)
            .HasColumnName("description");

        builder.Property(d => d.State)
            .IsRequired()
            .HasDefaultValue(WidgetState.Inactive)
            .HasColumnName("state");
    
        builder.Property(d => d.Kind)
            .HasMaxLength(255)
            .HasColumnName("kind");
    
        builder.Property(d => d.ElementName)
            .HasMaxLength(255)
            .HasColumnName("element_name");
    
        builder.Property(d => d.ScriptUrl)
            .HasMaxLength(1000)
            .HasColumnName("script_url");
        
        builder.Property(d => d.Html)
            .HasMaxLength(1000)
            .HasColumnName("html");
        
        builder.Property(d => d.Cols)
            .HasColumnName("cols");
        
        builder.Property(d => d.Rows)
            .HasColumnName("rows");
    }
}