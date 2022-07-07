using HotelReservation.DbContexts;
using HotelReservation.DTO;
using HotelReservation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Services.ReservationProviders
{
    internal class DatabaseReservationProvider : IReservationProvider
    {
        private readonly HotelReservationDbContextFactory _dbContextFactory;

        public DatabaseReservationProvider(HotelReservationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }


        // ассинхронное получение всех записей из БД
        public IEnumerable<Reservation> GetAllReservations()
        {
            using (HotelReservationDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ReservationDTO> reservationDTOs = context.Reservations.ToList();

                return reservationDTOs.Select(r => ToReservation(r));
            }
        }

        private static Reservation ToReservation(ReservationDTO r)
        {
            return new Reservation(new RoomID(r.FloorNumber, r.RoomNumber), r.Username, r.StartTime, r.EndTime);
        }
    }
}