using SomeShop.Models;

namespace SomeShop.Services.Interfaces
{
    public interface IPaymentProviderService
    {
        // Manage PaymentProviders
        void CreatePaymentProvider(PaymentProvider item);
        IEnumerable<PaymentProvider> GetPaymentProviders();
        IEnumerable<PaymentProvider> GetPaymentProviders(Func<PaymentProvider, bool> predicate);
        PaymentProvider? GetProviderById(int id);
        void UpdatePaymentProvider(PaymentProvider item);
        void DeletePaymentProvider(PaymentProvider item);
    }
}
/*
void Create%EntityType%(%EntityType% item);
IEnumerable<%EntityType%> Get%EntityType%s();
IEnumerable<%EntityType%> Get%EntityType%s(Func<%EntityType%, bool> predicate);
%EntityType%? Get%EntityType%ById(int id);
void Update%EntityType%(%EntityType% item);
void Delete%EntityType%(%EntityType% item);
 */
