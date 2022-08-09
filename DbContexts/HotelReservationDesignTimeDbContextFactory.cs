using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.DbContexts
{
    public class HotelReservationDesignTimeDbContextFactory : IDesignTimeDbContextFactory<HotelReservationDbContext>
    {
        public HotelReservationDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data Source=HotelReservation.db").Options;

            return new HotelReservationDbContext(options);
        }
    }
}
