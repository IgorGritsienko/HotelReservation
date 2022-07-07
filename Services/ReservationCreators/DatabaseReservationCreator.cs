using HotelReservation.DbContexts;
using HotelReservation.DTO;
using HotelReservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Services.ReservationCreators
{
    internal class DatabaseReservationCreator : IReservationCreator
    {
        private readonly HotelReservationDbContextFactory _dbContextFactory;

        public DatabaseReservationCreator(HotelReservationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        
        // создание новой записи
        public void CreateReservation(Reservation reservation)
        {
            using (HotelReservationDbContext context = _dbContextFactory.CreateDbContext())
            {
                ReservationDTO reservationDTO = ToReservationDTO(reservation); 
                context.Reservations.Add(reservationDTO);
                context.SaveChanges();
            }
        }

        private ReservationDTO ToReservationDTO(Reservation reservation)
        {
            return new ReservationDTO()
            {
                FloorNumber = reservation.RoomID?.FloorNumber ?? 0,
                RoomNumber = reservation.RoomID?.RoomNumber ?? 0,
                Username = reservation.Username,
                StartTime = reservation.StartTime,
                EndTime = reservation.EndTime,
            };
        }
    }
}
