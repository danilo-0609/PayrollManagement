using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayrollManagement.Domain.Users;
using PayrollManagement.Domain.ValueObjects;

namespace PayrollManagement.Infraestructure.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(k => k.UserId);

            builder.Property(c => c.UserId)
                .HasConversion(
                 userId => userId.Value,
                 value => new UserId(value)
                 )
                .HasColumnName("UserId")
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(c => c.UserName)
                .HasConversion(
                userName => userName.Value,
                value => UserName.Create(value)!
                )
                .HasColumnName("Username")
                .HasMaxLength(90)
                .IsRequired(true);

            builder.Property(c => c.Email)
                .HasConversion(
                 email => email.Value,
                 value => Email.Create(value)!
                 )
                .HasColumnName("Email")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(c => c.Password)
                .HasConversion(
                 password => password.Value,
                 value => Password.Create(value)!
                 );

            builder.Property(c => c.Role)
                .IsRequired()
                .HasMaxLength(5);

            builder.HasIndex(i => i.Email).IsUnique();
        }
    }
}
