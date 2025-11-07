using Domain.Entity;

namespace Application.Abstraction
{
    public interface IPaymentRepository
    {
        Payment? GetPaymentById(int id);
        List<Payment> GetAllPayments();
        bool CreatePayment(Payment payment);
        bool UpdatePayment(Payment payment);
        bool DeletePayment(int id);

        // creo una nueva firma de metodo para verificar existencia por TurnId
        // esto no hace nada mas que devolver un booleano si existe o no

        bool PaymentExistsByTurnId(int turnId);

    }
}
