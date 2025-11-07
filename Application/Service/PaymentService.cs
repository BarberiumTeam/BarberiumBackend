using Application.Abstraction;
using Contracts.Payment.Request;
using Contracts.Payment.Response;
using Domain.Entity;

namespace Application.Service
{
    public class PaymentService : IPaymentService

        // hacemos otra DI verificar la existencia de un Turn en el servicio/repositorio de Turn.
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ITurnRepository _turnRepository; // inyeccion de la otra entidad porque no tiene el contexto sino

        public PaymentService(IPaymentRepository paymentRepository, ITurnRepository turnRepository)
        {
            _paymentRepository = paymentRepository;
            _turnRepository = turnRepository; // la otra inyeccion
        }

        public List<PaymentResponse> GetAllPayments()
        {
            var payments = _paymentRepository.GetAllPayments();
            return payments.Select(payment => new PaymentResponse
            {
                Id = payment.Id,
                TurnId = payment.TurnId,
                Amount = payment.Amount,
                DatePayment = payment.DatePayment,
                TimePayment = payment.TimePayment,
                Method = payment.Method,
                State = payment.State,
            }).ToList();
        }

        public PaymentResponse? GetPaymentById(int id)
        {
            var payment = _paymentRepository.GetPaymentById(id);
            if (payment == null)
            {
                return null;
            }
            return new PaymentResponse
            {
                Id = payment.Id,
                TurnId = payment.TurnId,
                Amount = payment.Amount,
                DatePayment = payment.DatePayment,
                TimePayment = payment.TimePayment,
                Method = payment.Method,
                State = payment.State,
            };
        }

        public bool CreatePayment(CreatePaymentRequest request)
        {
            // Verificar si el Turn existe antes de crear el Payment
            var turn = _turnRepository.GetTurnById(request.TurnId);
            if (turn == null)
            {
                return false; // No se puede crear el Payment si el Turn no existe
            }

            // Validamos si ya existe un pago para ese TurnId (UNICIDAD)

            if (_paymentRepository.PaymentExistsByTurnId(request.TurnId))
            {
                // Si el pago para este turno ya existe, retornamos false.
                return false;
            }

            // podria crear un Factory-Method para Payment si quisiera abstraer mas la creacion, pero por ahora lo hago directo aca
            // porque no quiero tener que hacer otra migracion por ahora

            DateTime currentDateTime = DateTime.Now;
            DateOnly paymentDate = DateOnly.FromDateTime(currentDateTime);
            TimeOnly paymentTime = TimeOnly.FromDateTime(currentDateTime);

            var payment = new Payment
            {
                TurnId = request.TurnId,
                Amount = request.Amount,
                Method = request.Method,
                State = request.State,

                DatePayment = paymentDate,
                TimePayment = paymentTime,
            };
            return _paymentRepository.CreatePayment(payment);
        }

        public bool UpdatePayment(int id, UpdatePaymentRequest request)
        {
            var existingPayment = _paymentRepository.GetPaymentById(id);
            if (existingPayment == null)
            {
                return false;
            }
            existingPayment.TurnId = request.TurnId;
            existingPayment.Amount = request.Amount;
            existingPayment.Method = request.Method;
            existingPayment.State = request.State;

            return _paymentRepository.UpdatePayment(existingPayment);
        }

        public bool DeletePayment(int id)
        {
            return _paymentRepository.DeletePayment(id);
        }
    }

}