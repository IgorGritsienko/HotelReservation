using HotelReservation.Exceptions;
using HotelReservation.Services.ReservationConflictValidatiors;
using HotelReservation.Services.ReservationCreators;
using HotelReservation.Services.ReservationDeleter;
using HotelReservation.Services.ReservationProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Models
{
    public class ReservationBook
    {
        private readonly IReservationProvider _reservationProvider;
        private readonly IReservationCreator _reservationCreator;
        private readonly IReservationConflictValidator _reservationConflictValidator;
        private readonly IReservationDeleter _reservationDeleter;

        public ReservationBook(IReservationProvider reservationProvider,
                               IReservationCreator reservationCreator,
                               IReservationDeleter reservationDeleter,
                               IReservationConflictValidator reservationConflictValidator)
        {
            _reservationProvider = reservationProvider;
            _reservationCreator = reservationCreator;
            _reservationDeleter = reservationDeleter;
            _reservationConflictValidator = reservationConflictValidator;
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return _reservationProvider.GetAllReservations();
        }

        public void AddReservation(Reservation reservation)
        {
            Reservation conflictingReservation = _reservationConflictValidator.GetConflictingReservation(reservation);

            if (conflictingReservation is not null)
            {
                throw new ReservationConflictException(conflictingReservation, reservation);
            }

            _reservationCreator.CreateReservation(reservation);
        }

        // удаление записи в БД
        // вызывается из Hotel
        public void DeleteReservation(Reservation reservation)
        {
             _reservationDeleter.DeleteReservation(reservation);
        }
    }
}
