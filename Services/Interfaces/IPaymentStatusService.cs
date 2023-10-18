﻿using SomeShop.Models;

namespace SomeShop.Services.Interfaces
{
    public interface IPaymentStatusService
    {
        // Manage PaymentStatuses
        void CreatePaymentStatus(PaymentStatus item);
        IEnumerable<PaymentStatus> GetPaymentStatuss();
        IEnumerable<PaymentStatus> GetPaymentStatuss(Func<PaymentStatus, bool> predicate);
        PaymentStatus? GetPaymentStatusById(int id);
        void UpdatePaymentStatus(PaymentStatus item);
        void DeletePaymentStatus(PaymentStatus item);
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