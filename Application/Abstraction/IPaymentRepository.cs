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
        // asi se puede validar antes de crear un pago nuevo
        // por ejemplo, para evitar pagos duplicados para el mismo turno

        bool PaymentExistsByTurnId(int turnId);

    }
}
