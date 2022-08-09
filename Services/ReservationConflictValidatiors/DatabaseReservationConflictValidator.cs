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

                ReservationDTO? reservationDTO = context.Reservations
                    .Where(r => r.FloorNumber == reservation.RoomID.FloorNumber)
                    .Where(r => r.RoomNumber == reservation.RoomID.RoomNumber)
                    .Where(r => r.EndTime > reservation.StartTime)
                    .Where(r => r.StartTime < reservation.EndTime)
                    .FirstOrDefault();

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
