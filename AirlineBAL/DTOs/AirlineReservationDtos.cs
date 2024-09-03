using static AirLineShared.Enum.AirlineEnums;

namespace AirlineBAL.DTOs
{
    public class AirlineReservationDtos
    {
        public int TicketNo { get; set; }
        public DateTime DateofJourney { get; set; }
        public string? FlightNo { get; set; }
        public string? Source { get; set; }
        public string? Destination { get; set; }
        public string? PassengerName { get; set; }
        public string? Email { get; set; }
        public Gender Gender { get; set; }
        public long ContactNo { get; set; }
        public decimal Fair { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public Status Status { get; set; }
        public string? PNRNumber { get; set; }
    }
}
