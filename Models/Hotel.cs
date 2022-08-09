using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Models
{
    internal class Hotel
    {
        private readonly ReservationBook _reservationBook;

        public string Name { get; }


        public Hotel(string name, ReservationBook reservationBook)
        {
            Name = name;

            _reservationBook = reservationBook;
        }

        // получить все записи бронирований
        public IEnumerable<Reservation> GetAllReservations()
        {
            return _reservationBook.GetAllReservations();
        }

        // забронировать
        public void MakeReservation(Reservation reservation)
        {
           _reservationBook.AddReservation(reservation);
        }

        // удаление записи в БД
        // вызывается из Hotel
        public void DeleteReservation(Reservation reservation)
        {
             _reservationBook.DeleteReservation(reservation);
        }
    }
}
