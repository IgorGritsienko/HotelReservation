using HotelReservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Services.ReservationProviders
{
    public interface IReservationProvider
    {
        IEnumerable<Reservation> GetAllReservations();
    }   
}
