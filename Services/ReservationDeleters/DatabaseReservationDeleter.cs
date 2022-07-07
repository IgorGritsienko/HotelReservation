using HotelReservation.DbContexts;
using HotelReservation.DTO;
using HotelReservation.Models;
using HotelReservation.Services.ReservationDeleter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Services.ReservationDeleters
{
    internal class DatabaseReservationDeleter : IReservationDeleter
    {
        
        private readonly HotelReservationDbContextFactory _dbContextFactory;

        // устанавливаем контекст
        public DatabaseReservationDeleter(HotelReservationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        // поиск и удаление выбранной записи в БД
        // применяется в ReservationBook
        public void DeleteReservation(Reservation reservation)
        {
            using (HotelReservationDbContext context = _dbContextFactory.CreateDbContext())
            {
                ReservationDTO selected = context.Reservations.First(r => r.RoomNumber == reservation.RoomID.RoomNumber &&
                r.FloorNumber == reservation.RoomID.FloorNumber &&
                r.StartTime == reservation.StartTime &&
                r.EndTime == reservation.EndTime);

                context.Reservations.Remove(selected);
                context.SaveChanges();
            }
        }
    }
}
