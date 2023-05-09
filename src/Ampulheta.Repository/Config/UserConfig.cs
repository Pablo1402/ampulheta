using Ampulheta.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ampulheta.Repository.Config
{

    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
              .IsRequired()
              .HasColumnType("varchar(200)");

            builder.Property(x => x.Login)
              .IsRequired()
              .HasColumnType("varchar(400)");


            builder.Property(x => x.Email)
              .IsRequired()
              .HasColumnType("varchar(400)");


            builder.Property(x => x.Password)
              .IsRequired()
              .HasColumnType("varchar(max)");

            builder.HasOne(x => x.UserType)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.UserTypeId)
                .HasPrincipalKey(x => x.Id);


            builder.HasMany(x => x.Times)
                .WithOne(x => x.User);


        }
    }
}
