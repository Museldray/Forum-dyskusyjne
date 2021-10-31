using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Forum_dyskusyjne.Models
{
    // Możesz dodać dane profilu dla użytkownika, dodając więcej właściwości do klasy ApplicationUser. Odwiedź stronę https://go.microsoft.com/fwlink/?LinkID=317594, aby dowiedzieć się więcej.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Element authenticationType musi pasować do elementu zdefiniowanego w elemencie CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Dodaj tutaj niestandardowe oświadczenia użytkownika
            return userIdentity;
        }

        public int Rank { get; set; }
        public string Avatar { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Privileges { get; set; }
        public bool IfAdminChangedRank { get; set; }
        public int PostsOnPage { get; set; }
        public virtual ICollection<Moderator> Moderators { get; set; }
        public virtual ICollection<MessageUser> Messages { get; set; }
        public virtual ICollection<Thread> Posts { get; set; }
        public virtual ICollection<Post> Comments { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Message> Messeges { get; set; }
        public DbSet<Attachment> Photos { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Friends> Friends { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Moderator> Moderators { get; set; }
        public DbSet<MessageUser> MessageUser { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Friends>()
                .HasRequired(f=>f.Friend)
                .WithOptional()
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Friends>()
                .HasRequired(f => f.Friend)
                .WithOptional()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MessageUser>()
            .HasKey(mu => new { mu.MessageId, mu.ReceiverId, mu.SenderId });

            modelBuilder.Entity<ApplicationUser>()
                        .HasMany(m1 => m1.Messages)
                        .WithRequired()
                        .HasForeignKey(mu => mu.ReceiverId);

            modelBuilder.Entity<ApplicationUser>()
                        .HasMany(m2 => m2.Messages)
                        .WithRequired()
                        .HasForeignKey(mu => mu.SenderId);

            modelBuilder.Entity<Message>()
                        .HasMany(u => u.Users)
                        .WithRequired()
                        .HasForeignKey(mu => mu.MessageId);
        }
    }
}