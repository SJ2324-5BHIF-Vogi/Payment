using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spg.Payment.DomainModel.Model;
using System.Reflection.Emit;

namespace Spg.Payment.Infrastructure
{
    public class PaymentContext : DbContext
    {
        public DbSet<Payment.DomainModel.Model.Payment> Payments => Set<Payment.DomainModel.Model.Payment>();
        public DbSet<Payment.DomainModel.Model.User> Users => Set<Payment.DomainModel.Model.User>();

        public PaymentContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        { }

        // 4. Methoden ((OnConfiguring), OnModelCreating)
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Payment.DomainModel.Model.Payment>().OwnsOne(b => b.PaidFromUserNavigation);
            //builder.Entity<Payment.DomainModel.Model.Payment>().OwnsOne(b => b.UserNavigation);
            //builder.Entity<Payment.DomainModel.Model.User>();
            builder.Entity<User>().HasMany(p => p.Payments).WithOne(p => p.UserNavigation);
        }
    }
}
