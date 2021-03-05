using Microsoft.EntityFrameworkCore;
using Payment.Application.Interface;
using Payment.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Persistence.Context
{
    public class PaymentDbContext : DbContext, ICheapPaymentGateway, IExpensivePaymentGateway, PremiumPaymentGateway
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Payments> Payments { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}
