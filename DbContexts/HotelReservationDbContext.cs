using HotelReservation.DTO;
using HotelReservation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.DbContexts
{
    internal class HotelReservationDbContext : DbContext
    {
        public HotelReservationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ReservationDTO> Reservations {get; set;}
    }
}
