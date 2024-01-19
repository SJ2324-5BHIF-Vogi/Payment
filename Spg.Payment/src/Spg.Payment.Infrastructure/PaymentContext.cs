using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spg.Payment.DomainModel.Model; 

namespace Spg.Payment.Infrastructure
{
    public class PaymentContext : DbContext
    {
        public DbSet<Payments> Payments => Set<Payments>();
        public DbSet<User> Users => Set<User>();

        public PaymentContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        { }

        // 4. Methoden ((OnConfiguring), OnModelCreating)
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Payments>().OwnsOne(b => b.PaidFromUserNavigation);
            builder.Entity<Payments>().OwnsOne(b => b.UserNavigation);
        }
    }
}
