using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using stage1.Domain.Entities;

namespace stage1.Data.Configurations;


public class SalaConfiguration : IEntityTypeConfiguration<Sala>
{
    public void Configure(EntityTypeBuilder<Sala> builder)
    {
        builder.HasKey( s => s.Id );
        builder.HasIndex( s => s.Numero );

        builder.Property( s => s.Id ).UseAutoincrement();
        builder.Property( s => s.IsActive ).IsRequired();
    }
}