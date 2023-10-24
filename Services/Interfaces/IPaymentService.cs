using SomeShop.Models;
using System.Linq.Expressions;

namespace SomeShop.Services.Interfaces
{
    public interface IPaymentService
    {
        // Manage Payment
        Task CreatePaymentAsync(Payment item, CancellationToken cancellationToken);
        Task<async Task<IEnumerable<Payment>>> GetPaymentsAsync(CancellationToken cancellationToken);
        Task<async Task<IEnumerable<Payment>>> GetPaymentsAsync(Expression<Func<Payment, bool>> predicate, CancellationToken cancellationToken);
        Payment? GetPaymentByOrderIdAsync(int id, CancellationToken cancellationToken);
        Task UpdatePaymentAsync(Payment item, CancellationToken cancellationToken);
        Task DeletePaymentAsync(Payment item, CancellationToken cancellationToken);
    }
}
