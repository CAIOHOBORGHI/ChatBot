using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Chat.Core.Models;

namespace Chat.Database
{

    public class ApplicationDbContext : IdentityDbContext, IDataProtectionKeyContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            /* Creates initial tables in docker database */
            Database.Migrate();
        }

        #region Tables
        public DbSet<ChatUser> ChatUsers { get; set; }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        public DbSet<Message> Messages { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Relationships
            builder
                .Entity<Message>()
                .HasOne<ChatUser>(message => message.Writter)
                .WithMany(user => user.Messages)
                .HasForeignKey(user => user.UserId);
            #endregion
        }
    }
}