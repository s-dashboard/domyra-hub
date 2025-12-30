using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domyra.Infrastructure.Persistence.Configurations;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public abstract class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    protected abstract string TableName { get; }
    
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.ToTable(TableName);
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id)
            .IsRequired()
            .HasColumnName("id");
        builder.Property(d => d.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");
        builder.Property(d => d.UpdatedAt)
            .HasColumnName("updated_at");
        
        TableConfigure(builder);
    }
    
    protected abstract void TableConfigure(EntityTypeBuilder<T> builder);
}