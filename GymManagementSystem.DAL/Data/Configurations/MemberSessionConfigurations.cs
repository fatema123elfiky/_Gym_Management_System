using GymManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.DAL.Data.Configurations
{
    internal class MemberSessionConfigurations : IEntityTypeConfiguration<MemberSession>
    {
        public void Configure(EntityTypeBuilder<MemberSession> builder)
        {
            builder.Ignore(x => x.Id);
            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.HasKey(x => new { x.MemberId, x.SessionId });
        }
    }
}
