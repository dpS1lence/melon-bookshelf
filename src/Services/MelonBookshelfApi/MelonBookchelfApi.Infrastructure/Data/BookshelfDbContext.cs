using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MelonBookchelfApi.Infrastructure.Data.Models;
using System.Reflection.Emit;

namespace MelonBookchelfApi.Infrastructure.Data
{
    public class BookshelfDbContext : IdentityDbContext<IdentityUser>
    {
        public BookshelfDbContext()
        { }

        public BookshelfDbContext(DbContextOptions<BookshelfDbContext> options)
            : base(options)
        { }

        public DbSet<Resource> Resources { get; set; } = null!;
        public DbSet<RequestFollower> RequestsFollowers { get; set; } = null!;
        public DbSet<RequestUpvoter> RequestsUpvoters { get; set; } = null!;
        public DbSet<ResourceCategory> ResourceCategories { get; set; } = null!;
        public DbSet<Request> Requests { get; set; } = null!;
        public DbSet<UserResource> UsersResources { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RequestFollower>().HasKey(a => new
            {
                a.RequestId,
                a.UserID
            });
            
            builder.Entity<RequestUpvoter>().HasKey(a => new
            {
                a.RequestId,
                a.UserID
            });

            builder.Entity<UserResource>().HasKey(a => new
            {
                a.ResourceId,
                a.UserId
            });

            base.OnModelCreating(builder);
        }
    }
}
