using HotelReservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.ViewModels
{
    // определяет интерфейс INotifyPropertyChanged и НЕ использует бизнес-логику из Models.Reservation
    // привязка данных у ReservationListView через Reservation, т.к. Reservation - список ReservationViewModel
    internal class ReservationViewModel : ViewModelBase
    {
        private readonly Reservation _reservation;

        public string RoomID => _reservation.RoomID?.ToString();
        public string Username => _reservation.Username;
        public string StartDate => _reservation.StartTime.Date.ToString("d");
        public string EndDate => _reservation.EndTime.Date.ToString("d");

        public ReservationViewModel(Reservation reservation)
        {
            _reservation = reservation;
        }
    }
}
