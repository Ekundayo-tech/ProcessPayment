using Microsoft.EntityFrameworkCore;
using Payment.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Application.Interface
{
    public interface PremiumPaymentGateway
    {
        DbSet<Payments> Payments { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
