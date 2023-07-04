using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MelonBookchelfApi.Infrastructure.Data.Models;

namespace MelonBookchelfApi.Infrastructure.Data
{
    public class DbContext : IdentityDbContext<IdentityUser>
    {
        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        { }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<RequestFollower> RequestsFollowers { get; set; }
        public DbSet<RequestUpvoter> RequestsUpvoters { get; set; }
        public DbSet<ResourceCategory> ResourceCategories { get; set; }
        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
