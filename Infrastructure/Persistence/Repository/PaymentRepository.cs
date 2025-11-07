using Application.Abstraction;
using Domain.Entity;

namespace Infrastructure.Persistence.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly BarberiumDbContext _context;

        public PaymentRepository(BarberiumDbContext context)
        {
            _context = context;
        }

        public Payment? GetPaymentById(int id)
        {
            return _context.Payments.FirstOrDefault(p => p.Id == id);
        }

        public List<Payment> GetAllPayments()
        {
            return _context.Payments.ToList();
        }

        public bool CreatePayment(Payment payment)
        {
            _context.Payments.Add(payment);
            return _context.SaveChanges() > 0;
        }

        public bool UpdatePayment(Payment payment)
        {
            _context.Payments.Update(payment);
            return _context.SaveChanges() > 0;
        }

        public bool DeletePayment(int id)
        {
            var paymentToDelete = _context.Payments.FirstOrDefault(p => p.Id == id);
            if (paymentToDelete == null) return false;

            _context.Payments.Remove(paymentToDelete);
            return _context.SaveChanges() > 0;
        }

        public bool PaymentExistsByTurnId(int turnId)
        {
            return _context.Payments.Any(p => p.TurnId == turnId);
        }
    }
}