using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Models
{
    public class Reservation
    {
        public RoomID RoomID { get; }
        public string Username { get; }
        public DateTime StartTime  { get; }
        public DateTime EndTime { get; }

        public TimeSpan Length => EndTime.Subtract(StartTime);

        public Reservation(RoomID roomID, string username, DateTime startTime, DateTime endTime)
        {
            RoomID = roomID;
            Username = username;
            StartTime = startTime.Date;
            EndTime = endTime.Date;
        }

        internal bool Conflicts(Reservation reservation)
        {
            if (reservation.RoomID != RoomID)
            {
                return false;
            }
            return reservation.StartTime < EndTime && reservation.EndTime > StartTime;
        }

        public override bool Equals(object obj)
        {
            return obj is Reservation reservation &&
                RoomID == reservation.RoomID &&
                Username == reservation.Username &&
                StartTime == reservation.StartTime &&
                EndTime == reservation.EndTime;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RoomID, Username, StartTime, EndTime);

        }
    }
}
