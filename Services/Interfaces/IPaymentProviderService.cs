using SomeShop.Models;
using System.Linq.Expressions;

namespace SomeShop.Services.Interfaces
{
    public interface IPaymentProviderService
    {
        Task CreatePaymentProviderAsync(PaymentProvider item, CancellationToken cancellationToken);
        Task<async Task<IEnumerable<PaymentProvider>>> GetPaymentProvidersAsync(CancellationToken cancellationToken);
        Task<async Task<IEnumerable<PaymentProvider>>> GetPaymentProvidersAsync(Expression<Func<PaymentProvider, bool>> predicate, CancellationToken cancellationToken);
        PaymentProvider? GetProviderByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdatePaymentProviderAsync(PaymentProvider item, CancellationToken cancellationToken);
        Task DeletePaymentProviderAsync(PaymentProvider item, CancellationToken cancellationToken);
    }
}
