using SomeShop.Models;
using System.Linq.Expressions;

namespace SomeShop.Services.Interfaces
{
    public interface IPaymentStatusService
    {
        // Manage PaymentStatuses
        Task CreatePaymentStatusAsync(PaymentStatus item, CancellationToken cancellationToken);
        Task<IEnumerable<PaymentStatus>> GetPaymentStatusesAsync(CancellationToken cancellationToken);
        Task<IEnumerable<PaymentStatus>> GetPaymentStatusesAsync(Expression<Func<PaymentStatus, bool>> predicate, CancellationToken cancellationToken);
        Task<PaymentStatus?> GetPaymentStatusByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdatePaymentStatusAsync(PaymentStatus item, CancellationToken cancellationToken);
        Task DeletePaymentStatusAsync(PaymentStatus item, CancellationToken cancellationToken);
        Task DeletePaymentStatusByIdAsync(int id, CancellationToken cancellationToken);
    }
}
