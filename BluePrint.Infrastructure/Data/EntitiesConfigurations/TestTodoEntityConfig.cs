using BluePrint.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BluePrint.Infrastructure.Data.EntitiesConfigurations;

internal class TestTodoEntityConfig : IEntityTypeConfiguration<TestTodoEntity>
{
    public void Configure(EntityTypeBuilder<TestTodoEntity> builder)
    {
        builder.ToTable("TestDtoEntities");

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).IsRequired();

        builder.Property(p => p.Title)
            .HasMaxLength(120)
            .IsUnicode(false)
            .IsRequired();        
        
        builder.Property(p => p.CreateDateTime)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAdd()
            .IsRequired();
    }
}