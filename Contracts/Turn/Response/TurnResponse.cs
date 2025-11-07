using Domain.Entity;
namespace Contracts.Turn.Response
{
    public class TurnResponse
    {
        public int Id { get; set; }
        public string Client { get; set; } = string.Empty;
        public string Barber { get; set; } = string.Empty;


        public ServiceType Service { get; set; }
        public DateOnly TurnDate { get; set; }
        public TimeOnly TurnStartTime { get; set; }
        public TimeOnly TurnEndTime { get; set; }
        public State State { get; set; }

    }
}
