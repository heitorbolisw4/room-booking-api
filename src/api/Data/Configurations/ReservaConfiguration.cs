using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using stage1.Domain.Entities;

namespace stage1.Data.Configurations;

public class ReservaConfiguration : IEntityTypeConfiguration<Reserva>
{
    public void Configure(EntityTypeBuilder<Reserva> builder)
    {
        builder.HasKey( r => r.Id);

        builder.HasIndex( r => r.SalaId);

        builder.Property( r => r.Inicio).IsRequired().HasConversion( new DateTimeOffsetToBinaryConverter() );
        builder.Property( r => r.Fim).IsRequired().HasConversion( new DateTimeOffsetToBinaryConverter() );

    }
}