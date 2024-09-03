using AirlineBAL.DTOs;
using AirlineBAL.Utiliites;
using AirlineDAL.Entities;
using AirlineDAL.Repository;
using AirLineShared.Enum;
using Microsoft.EntityFrameworkCore;
using static AirlineDAL.Repository.AirlineReservationRepository;
using static AirLineShared.Enum.AirlineEnums;

namespace AirlineBAL.Services
{
    public class AirlineReservationService
    {
        private readonly AirlineReservationRepository _airlineRepository;
        public AirlineReservationService()
        {
            _airlineRepository = new();
        }

        #region 1. Add Records.

        // auto ticket number starting with 101.
        public int currentNumber = 101;

        public async Task<int> GenerateNextNumberAsync()
        {
            bool ticketExists = await _airlineRepository.CheckTicketNoExistRepositoryAsync(currentNumber);

            while (ticketExists)
            {
                currentNumber++;
                ticketExists = await _airlineRepository.CheckTicketNoExistRepositoryAsync(currentNumber);
            }

            return currentNumber++;
        }

        // Check Email Exist Today
        public async Task<bool> CheckPasswordExistService(string email)
        {
            return await _airlineRepository.CheckEmailExistTodayRepositoryAsync(email);
        }

        // auto generate pnr number
        public string GeneratePNRNumber()
        {
            Random random = new Random();
            string pnr = "";
            for (int i = 0; i < 8; i++)
            {
                pnr += random.Next(0, 10).ToString();
            }
            return pnr;
        }

        // discount based on new passenger: male - 10%, female 35%
        public decimal CalculateDiscount(Gender gender, decimal fair)
        {
            if (gender == Gender.Male)
            {
                NetAmount(fair, fair * 0.1m);
                return fair * 0.1m;
            }
            else
            {
                NetAmount(fair, fair * 0.35m);
                return fair * 0.35m;
            }
        }

        // Net Amount calculation method
        public decimal NetAmount(decimal fare, decimal discount)
        {
            return fare - discount;
        }

        public async Task<bool> AddAirlineReservationServices(AirlineReservationDtos airlineReservationDtos)
        {
            var discount = CalculateDiscount(airlineReservationDtos.Gender, airlineReservationDtos.Fair);
            var netAmount = NetAmount(airlineReservationDtos.Fair, discount);

            var reservarionDetails = new AirlineReservation()
            {
                TicketNo = await GenerateNextNumberAsync(),
                DateofJourney = airlineReservationDtos.DateofJourney,
                FlightNo = airlineReservationDtos.FlightNo,
                Source = airlineReservationDtos.Source,
                Destination = airlineReservationDtos.Destination,
                PassengerName = airlineReservationDtos.PassengerName,
                Email = airlineReservationDtos.Email,
                Gender = airlineReservationDtos.Gender,
                ContactNo = airlineReservationDtos.ContactNo,
                Fair = airlineReservationDtos.Fair,
                Discount = discount,
                NetAmount = netAmount,
                PaymentMode = airlineReservationDtos.PaymentMode,
                Status = airlineReservationDtos.Status,
                PNRNumber = GeneratePNRNumber(),
            };

            //Send email.
            //await SendEmailConfirmation.SendConfirmationEmail
            //                        (
            //                            reservarionDetails.PassengerName, reservarionDetails.Email,
            //                            reservarionDetails.FlightNo, reservarionDetails.Source, reservarionDetails.Destination, 
            //                            reservarionDetails.DateofJourney, reservarionDetails.PNRNumber, reservarionDetails.PaymentMode, 
            //                            reservarionDetails.Status, reservarionDetails.Gender, reservarionDetails.NetAmount, reservarionDetails.Discount,
            //                            reservarionDetails.Fair, reservarionDetails.ContactNo
            //                        );
            
            return await _airlineRepository.AddAirlineReservationRepositoryAsync(reservarionDetails);
        }
        #endregion

        # region 2. Update Part
        public async Task<List<AirlineReservationDtos>> SearchBookingTicketPNRWise(string pnrNumber)
        {
            var searchRecordList = await _airlineRepository.ViewAllRecordsRepositoryAsync();
            return searchRecordList.Where(x => x.PNRNumber == pnrNumber).Select(x => new AirlineReservationDtos
            {
                TicketNo = x.TicketNo,
                DateofJourney = x.DateofJourney,
                FlightNo = x.FlightNo,
                Source = x.Source,
                Destination = x.Destination,
                PassengerName = x.PassengerName,
                Email = x.Email,
                Gender = x.Gender,
                ContactNo = x.ContactNo,
                Fair = x.Fair,
                Discount = x.Discount,
                NetAmount = x.NetAmount,
                PaymentMode = x.PaymentMode,
                Status = x.Status,
                PNRNumber = x.PNRNumber,
            }).ToList();
        }

        public async Task EditFlightNumberServices(string pnrNumer, string flightNumber)
        {
            //await _airlineRepository.EditFlightNumberRepositoryAsync(pnrNumer, flightNumber);
            await _airlineRepository.UpdateCustomerDetailsAsync(pnrNumer, UpdateField.FlightNumber,flightNumber);
        }
        public async Task EditSourceServices(string pnrNumer, string source)
        {
            //await _airlineRepository.EditSourceRepositoryAsync(pnrNumer, source);
            await _airlineRepository.UpdateCustomerDetailsAsync(pnrNumer, UpdateField.Source, source);
        }
        public async Task EditDestinationServices(string pnrNumer, string destination)
        {
            //await _airlineRepository.EditDestinationRepositoryAsync(pnrNumer, destination);
            await _airlineRepository.UpdateCustomerDetailsAsync(pnrNumer, UpdateField.Destination, destination);
        }
        public async Task EditEmailServices(string pnrNumer, string email)
        {
            //await _airlineRepository.EditEmailRepositoryAsync(pnrNumer, email);
            await _airlineRepository.UpdateCustomerDetailsAsync(pnrNumer, UpdateField.Email, email);
        }
        public async Task EditContactServices(string pnrNumer, long contact)
        {
            //await _airlineRepository.EditContactRepositoryAsync(pnrNumer, contact);
            await _airlineRepository.UpdateCustomerDetailsAsync(pnrNumer, UpdateField.Contact, contact);
        }
        #endregion

        #region 3. view all record part
        public async Task<List<AirlineReservationDtos>> ViewAllRecordService()
        {
            var allRecordList = await _airlineRepository.ViewAllRecordsRepositoryAsync();
            return allRecordList.Select(x => new AirlineReservationDtos
            {
                TicketNo = x.TicketNo,
                DateofJourney = x.DateofJourney,
                FlightNo = x.FlightNo,
                Source = x.Source,
                Destination = x.Destination,
                PassengerName = x.PassengerName,
                Email = x.Email,
                Gender = x.Gender,
                ContactNo = x.ContactNo,
                Fair = x.Fair,
                Discount = x.Discount,
                NetAmount = x.NetAmount,
                PaymentMode = x.PaymentMode,
                Status = x.Status,
                PNRNumber = x.PNRNumber,
            }).ToList();
        }
        #endregion

        #region 4. Search All record

        public async Task<List<AirlineReservationDtos>> SearchRecordSourceDestinationWise(string source, string destination)
        {
            var searchRecordList = await _airlineRepository.SearchRecordSourceDestinationWiseAsync(source, destination);
            return searchRecordList.Select(x => new AirlineReservationDtos
            {
                TicketNo = x.TicketNo,
                DateofJourney = x.DateofJourney,
                FlightNo = x.FlightNo,
                Source = x.Source,
                Destination = x.Destination,
                PassengerName = x.PassengerName,
                Email = x.Email,
                Gender = x.Gender,
                ContactNo = x.ContactNo,
                Fair = x.Fair,
                Discount = x.Discount,
                NetAmount = x.NetAmount,
                PaymentMode = x.PaymentMode,
                Status = x.Status,
                PNRNumber = x.PNRNumber,
            }).ToList();
        }

        public async Task<List<AirlineReservationDtos>> SearchRecordFlightContactWise(string flightNo, DateTime dateofJourney)
        {
            var searchRecordList = await _airlineRepository.SearchRecordDateFlightWiseAsync(flightNo, dateofJourney);
            return searchRecordList.Select(x => new AirlineReservationDtos
            {
                TicketNo = x.TicketNo,
                DateofJourney = x.DateofJourney,
                FlightNo = x.FlightNo,
                Source = x.Source,
                Destination = x.Destination,
                PassengerName = x.PassengerName,
                Email = x.Email,
                Gender = x.Gender,
                ContactNo = x.ContactNo,
                Fair = x.Fair,
                Discount = x.Discount,
                NetAmount = x.NetAmount,
                PaymentMode = x.PaymentMode,
                Status = x.Status,
                PNRNumber = x.PNRNumber,
            }).ToList();
        }
        #endregion

        #region 5. delete record by pnr
        public async Task DeleteRecordByPNRService(string pnrNumber)
        {
             await _airlineRepository.DeleteRecordByPNRRepositoryAsync(pnrNumber);
        }
        #endregion
    }
}
