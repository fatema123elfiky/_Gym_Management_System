using GymManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GymManagementSystem.DAL.Data.Configurations
{
    public class GymUserConfigurations<T> : IEntityTypeConfiguration<T> where T : GymUser, new()
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(u => u.Name)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

            builder.Property(u => u.Email)
                   .HasColumnType("varchar")
                   .HasMaxLength(100);
            // non clustered index and unique for fast
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Phone).IsUnique();

            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("EmailCheck", "Email like '_%@_%._%'");
                tb.HasCheckConstraint("PhoneCheck", "Phone like '010%' or Phone like '011%' or Phone like '012%' or Phone like '015%'");
            });

            // catch owned class and map the class to its attributes in db
            builder.OwnsOne(x => x.Address, Address =>
            {
                Address.Property(x => x.Street).HasColumnName("Street").HasColumnType("varchar").HasMaxLength(30);
                Address.Property(x => x.City).HasColumnName("City").HasColumnType("varchar").HasMaxLength(30);
                Address.Property(x => x.BuildingNumber).HasColumnName("BuildingNumber");
            });

        }
    }
}