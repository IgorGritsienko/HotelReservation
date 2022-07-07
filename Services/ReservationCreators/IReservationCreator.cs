using HotelReservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Services.ReservationCreators
{
    public interface IReservationCreator
    {
        void CreateReservation(Reservation reservation);
    }
}
