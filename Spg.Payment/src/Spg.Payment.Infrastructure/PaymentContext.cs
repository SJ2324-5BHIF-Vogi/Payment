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
        public DbSet<Payment.DomainModel.Model.Payment> Payments => Set<Payment.DomainModel.Model.Payment>();
        public DbSet<User> Users => Set<User>();

        public PaymentContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        { }

        // 4. Methoden ((OnConfiguring), OnModelCreating)
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Payment.DomainModel.Model.Payment>().OwnsOne(b => b.PaidFromUserNavigation);
            builder.Entity<Payment.DomainModel.Model.Payment>().OwnsOne(b => b.UserNavigation);
        }
    }
}
