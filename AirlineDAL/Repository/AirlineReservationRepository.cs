using AirlineDAL.Context;
using AirlineDAL.Entities;
using Microsoft.EntityFrameworkCore;
using static AirLineShared.Enum.AirlineEnums;

namespace AirlineDAL.Repository
{
    public class AirlineReservationRepository
    {
        private readonly AirlineDbContext _airlineContext;
        public AirlineReservationRepository()
        {
            _airlineContext = new();
        }

        #region 1. Add Records
        public async Task<bool> CheckEmailExistTodayRepositoryAsync(string email)
        {
            return await _airlineContext.AirlineReservation.AnyAsync(x => x.Email == email);
        }

        public async Task<bool> CheckTicketNoExistRepositoryAsync(int ticketNo)
        {
            return await _airlineContext.AirlineReservation.AnyAsync(x => x.TicketNo == ticketNo);
        }


        public async Task<bool> AddAirlineReservationRepositoryAsync(AirlineReservation airlineReservation)
        {
            try
            {
                await _airlineContext.AirlineReservation.AddAsync(airlineReservation);
                await _airlineContext.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }
        #endregion

        #region 2. Update

        public enum UpdateField
        {
            FlightNumber,
            Source,
            Destination,
            Email,
            Contact
        }

        public async Task UpdateCustomerDetailsAsync(string pnrNumber, UpdateField field, object value)
        {
            var customerDetails = await _airlineContext.AirlineReservation
                .Where(x => x.PNRNumber == pnrNumber)
                .FirstOrDefaultAsync();

            if (customerDetails != null)
            {
                switch (field)
                {
                    case UpdateField.FlightNumber:
                        customerDetails.FlightNo = value as string;
                        break;
                    case UpdateField.Source:
                        customerDetails.Source = value as string;
                        break;
                    case UpdateField.Destination:
                        customerDetails.Destination = value as string;
                        break;
                    case UpdateField.Email:
                        customerDetails.Email = value as string;
                        break;
                    case UpdateField.Contact:
                        if (value is long contactNumber)
                        {
                            customerDetails.ContactNo = contactNumber;
                        }
                        break;
                    default:
                        throw new ArgumentException("Invalid field specified", nameof(field));
                }

                await _airlineContext.SaveChangesAsync();
            }
        }

        //public async Task EditFlightNumberRepositoryAsync(string pnrNumber, string flightNumber)
        //{
        //    var customerDetails = await _airlineContext.AirlineReservation.Where(x => x.PNRNumber == pnrNumber).FirstOrDefaultAsync();
        //    if (customerDetails != null)
        //    {
        //        customerDetails.FlightNo = flightNumber;
        //    }
        //    await _airlineContext.SaveChangesAsync();
        //}
        //public async Task EditSourceRepositoryAsync(string pnrNumber, string source)
        //{
        //    var customerDetails = await _airlineContext.AirlineReservation.Where(x => x.PNRNumber == pnrNumber).FirstOrDefaultAsync();
        //    if (customerDetails != null)
        //    {
        //        customerDetails.Source = source;
        //    }
        //    await _airlineContext.SaveChangesAsync();
        //}
        //public async Task EditDestinationRepositoryAsync(string pnrNumber, string destination)
        //{
        //    var customerDetails = await _airlineContext.AirlineReservation.Where(x => x.PNRNumber == pnrNumber).FirstOrDefaultAsync();
        //    if (customerDetails != null)
        //    {
        //        customerDetails.Destination = destination;
        //    }
        //    await _airlineContext.SaveChangesAsync();
        //}
        //public async Task EditEmailRepositoryAsync(string pnrNumber, string email)
        //{
        //    var customerDetails = await _airlineContext.AirlineReservation.Where(x => x.PNRNumber == pnrNumber).FirstOrDefaultAsync();
        //    if (customerDetails != null)
        //    {
        //        customerDetails.Email = email;
        //    }
        //    await _airlineContext.SaveChangesAsync();
        //}
        //public async Task EditContactRepositoryAsync(string pnrNumber, long contact)
        //{
        //    var customerDetails = await _airlineContext.AirlineReservation.Where(x => x.PNRNumber == pnrNumber).FirstOrDefaultAsync();
        //    if (customerDetails != null)
        //    {
        //        customerDetails.ContactNo = contact;
        //    }
        //    await _airlineContext.SaveChangesAsync();
        //}

        #endregion

        #region 3. View All Records
        public async Task<List<AirlineReservation>> ViewAllRecordsRepositoryAsync()
        {
            return await _airlineContext.AirlineReservation.ToListAsync();
        }
        #endregion

        #region 4. Search Records
        public async Task<List<AirlineReservation>> SearchRecordSourceDestinationWiseAsync(string source, string destination)
        {
            return await _airlineContext.AirlineReservation.Where(x => x.Source == source && x.Destination == destination).ToListAsync();
        }

        public async Task<List<AirlineReservation>> SearchRecordDateFlightWiseAsync(string flightNo, DateTime dateofJourney)
        {
            return await _airlineContext.AirlineReservation.Where(x => x.FlightNo == flightNo && x.DateofJourney == dateofJourney).ToListAsync();
        }
        #endregion

        #region 5. Delete Record
        public async Task DeleteRecordByPNRRepositoryAsync(string pnrNumber)
        {
            var record = await _airlineContext.AirlineReservation.Where(x => x.PNRNumber == pnrNumber).FirstOrDefaultAsync();
            if (record != null && record.Status != Status.Canceled)
            {
                record.Status = Status.Canceled;
                await _airlineContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Status is already canceled.");
            }
        }
        #endregion
    }
}
