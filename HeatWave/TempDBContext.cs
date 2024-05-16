using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Entity;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace HeatWave
{
    public class TempDBContext : DbContext
    {
        public TempDBContext(DbContextOptions options) : base(options)
        {
        }

        public Microsoft.EntityFrameworkCore.DbSet<TemperatureMeasurement> TemperatureMeasurements { get; set; }
    }
}
