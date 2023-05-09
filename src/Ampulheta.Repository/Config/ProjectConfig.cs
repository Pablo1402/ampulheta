using Ampulheta.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ampulheta.Repository.Config
{
    public class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
              .IsRequired()
              .HasColumnType("varchar(200)");

            builder.Property(x => x.Note)
              .IsRequired()
              .HasColumnType("varchar(max)");


            builder.HasMany(x => x.Times)
                .WithOne(x => x.Project);


        }
    }
}
