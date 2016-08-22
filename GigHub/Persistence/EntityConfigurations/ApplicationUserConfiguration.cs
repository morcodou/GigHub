using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistence.EntityConfigurations
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);


            HasMany(a => a.Followers)
                .WithRequired(f => f.Followee)
                .WillCascadeOnDelete(false);

            HasMany(a => a.Followees)
               .WithRequired(f => f.Follower)
               .WillCascadeOnDelete(false);

            //        modelBuilder.Entity<UserNotification>()
            //            .HasRequired(u => u.User)
            //            .WithMany(u => u.UserNotifications)
            //            .WillCascadeOnDelete(false);
        }
    }
}