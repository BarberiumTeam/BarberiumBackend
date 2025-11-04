using Domain.Entity;
namespace Contracts.Turn.Response
{
    public class TurnResponse
    {
        public int Id { get; set; }
        public string Client { get; set; } = string.Empty;
        public string Barber { get; set; } = string.Empty;

        public DateTime DateTimeTurn { get; set; }
        public ServiceType Service { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public State State { get; set; }

    }
}
