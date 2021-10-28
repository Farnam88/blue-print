using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestAssignment.Models;

namespace TestAssignment.Core.Infrastructure.DataLayer.EntitiesConfigurations
{
    internal class TestAssignmentEntityConfig : IEntityTypeConfiguration<TestAssignmentEntity>
    {
        public void Configure(EntityTypeBuilder<TestAssignmentEntity> builder)
        {
            builder.ToTable("TestAssignmentEntities");

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
}