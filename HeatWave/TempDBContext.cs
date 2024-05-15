using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HeatWave
{
    public class TempDBContext : DbContext
    {
        public TempDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TemperatureMeasurement> TemperatureMeasurements { get; set; }
    }
}
