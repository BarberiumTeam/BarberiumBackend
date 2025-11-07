using Contracts.Payment.Request;
using Contracts.Payment.Response;
using Domain.Entity;

namespace Application.Service
{
    public interface IPaymentService
    {
        PaymentResponse? GetPaymentById(int id);
        List<PaymentResponse> GetAllPayments();
        bool CreatePayment(CreatePaymentRequest request);
        bool UpdatePayment(int id, UpdatePaymentRequest request);
        bool DeletePayment(int id);
    }
}