using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace HeatWave
{
    public class TempDBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public TempDBContext(DbContextOptions<TempDBContext> options) : base(options)
        {
            
        }

        public Microsoft.EntityFrameworkCore.DbSet<TemperatureMeasurement> TemperatureMeasurements { get; set; }
    }
}
