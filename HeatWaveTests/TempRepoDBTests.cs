using Microsoft.VisualStudio.TestTools.UnitTesting;
using HeatWave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace HeatWave.Tests
{
    [TestClass()]
    public class TempRepoDBTests
    {

        private const bool useDatabase = true;
        private static TempRepoDB _repoDB;

        [ClassInitialize]

        public static void InitializeOnce(TestContext context)
        {
            if (useDatabase)
            {
                var optionsBuilder = new DbContextOptionsBuilder<TempDBContext>();

                optionsBuilder.UseSqlServer(DBSecret.ConnectionStringSimply);

                TempDBContext _tempDBContext = new(optionsBuilder.Options);

                _tempDBContext.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.TemperatureMeasurement");

                _repoDB = new TempRepoDB(_tempDBContext);
            }
        }

        [TestMethod()]
        public void TempRepoDBTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetTempListTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetIDTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddTest()
        {
            TemperatureMeasurement temperatureMeasurementMinus = new TemperatureMeasurement { Date = new DateTime(2021, 5, 10, 8, 38, 0), InDoorTemperature = -17, OutDoorTemperature = -17 };
            _repoDB.Add(temperatureMeasurementMinus);
            Assert.IsTrue(temperatureMeasurementMinus.InDoorTemperature < 0);
            Assert.IsTrue(temperatureMeasurementMinus.OutDoorTemperature < 0);
            Assert.IsTrue(temperatureMeasurementMinus.Id > 0);
            IEnumerable<TemperatureMeasurement> result = _repoDB.GetTempList();
            Assert.IsTrue(result.Count() == 1);

        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Assert.Fail();
        }
    }
}