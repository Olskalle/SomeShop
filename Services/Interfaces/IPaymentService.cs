using SomeShop.Models;

namespace SomeShop.Services.Interfaces
{
    public interface IPaymentService
    {
        // Manage Payment
        void CreatePayment(Payment item);
        IEnumerable<Payment> GetPayments();
        IEnumerable<Payment> GetPayments(Func<Payment, bool> predicate);
        Payment? GetPaymentByOrderId(int id);
        void UpdatePayment(Payment item);
        void DeletePayment(Payment item);
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
