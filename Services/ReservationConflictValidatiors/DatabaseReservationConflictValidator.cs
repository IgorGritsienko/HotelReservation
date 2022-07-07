using HotelReservation.DbContexts;
using HotelReservation.DTO;
using HotelReservation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Services.ReservationConflictValidatiors
{
    internal class DatabaseReservationConflictValidator : IReservationConflictValidator
    {
        private readonly HotelReservationDbContextFactory _dbContextFactory;

        public DatabaseReservationConflictValidator(HotelReservationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Reservation GetConflictingReservation(Reservation reservation)
        {
            using (HotelReservationDbContext context = _dbContextFactory.CreateDbContext())
            {

#pragma warning disable CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
                ReservationDTO reservationDTO = context.Reservations
                    .Where(r => r.FloorNumber == reservation.RoomID.FloorNumber)
                    .Where(r => r.RoomNumber == reservation.RoomID.RoomNumber)
                    .Where(r => r.EndTime > reservation.StartTime)
                    .Where(r => r.StartTime < reservation.EndTime)
                    .FirstOrDefault();
#pragma warning restore CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.

                if (reservationDTO == null)
                {
                    return null;
                }

                return ToReservation(reservationDTO);
            }
        }

        private static Reservation ToReservation(ReservationDTO r)
        {
            return new Reservation(new RoomID(r.FloorNumber, r.RoomNumber), r.Username, r.StartTime, r.EndTime);
        }

    }
}
