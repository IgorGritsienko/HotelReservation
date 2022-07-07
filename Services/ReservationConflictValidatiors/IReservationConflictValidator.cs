using HotelReservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Services.ReservationConflictValidatiors
{
    public interface IReservationConflictValidator
    {
        Reservation GetConflictingReservation(Reservation reservation);
    }
}
