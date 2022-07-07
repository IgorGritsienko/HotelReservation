using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.DbContexts
{
    internal class HotelReservationDbContextFactory
    {
        private readonly string _connectionString;

        public HotelReservationDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public HotelReservationDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;

            return new HotelReservationDbContext(options);
        }
    }
}
