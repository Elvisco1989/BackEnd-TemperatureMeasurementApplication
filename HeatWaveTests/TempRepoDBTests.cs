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
            IEnumerable<TemperatureMeasurement> result = _repoDB.GetTempList();
            Assert.IsTrue(result.Count() == 0);

            TemperatureMeasurement temperatureMeasurement = new TemperatureMeasurement { Date = new DateTime(2021, 5, 10, 8, 38, 0), InDoorTemperature = 11, OutDoorTemperature = 12 };
            _repoDB.Add(temperatureMeasurement);

            result = _repoDB.GetTempList();
            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.First().InDoorTemperature == 11);
            Assert.IsTrue(result.First().OutDoorTemperature == 12);
            Assert.IsTrue(result.First().Date == new DateTime(2021, 5, 10, 8, 38, 0));

        }

        [TestMethod()]
        public void GetIDTest()
        {
            TemperatureMeasurement tmAdd = _repoDB.Add( new TemperatureMeasurement { Date = new DateTime(2022, 6, 11, 7, 32, 0), InDoorTemperature = 9, OutDoorTemperature = 8 });
            TemperatureMeasurement? tm = _repoDB.GetID(tmAdd.Id);
            Assert.IsTrue(tm.Id == 1);
            Assert.IsTrue(tm.InDoorTemperature == 9);
            Assert.IsTrue(tm.OutDoorTemperature == 8);
            Assert.IsTrue(tm.Date == new DateTime(2022, 6, 11, 7, 32, 0));


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
            TemperatureMeasurement temperatureMeasurement = new TemperatureMeasurement { Date = new DateTime(2021, 5, 10, 8, 38, 0), InDoorTemperature = 11, OutDoorTemperature = 12 };
            _repoDB.Add(temperatureMeasurement);
            IEnumerable<TemperatureMeasurement> result = _repoDB.GetTempList();
            Assert.IsTrue(result.Count() == 1);
            _repoDB.Delete(temperatureMeasurement.Id);
            result = _repoDB.GetTempList();
            Assert.IsTrue(result.Count() == 0);

        }

        [TestMethod()]
        public void UpdateTest()
        {
            TemperatureMeasurement tM = new TemperatureMeasurement { Date = new DateTime(2021, 5, 10, 8, 38, 0), InDoorTemperature = 11, OutDoorTemperature = 12 };
            _repoDB.Add(tM);
            IEnumerable<TemperatureMeasurement> result = _repoDB.GetTempList();
            Assert.IsTrue(result.Count() == 1);
            tM.InDoorTemperature = 13;
            tM.OutDoorTemperature = 14;
            tM.Date = new DateTime(2021, 5, 10, 8, 38, 0);
            _repoDB.Update(tM);
            result = _repoDB.GetTempList();
            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.First().InDoorTemperature == 13);
            Assert.IsTrue(result.First().OutDoorTemperature == 14);
            Assert.IsTrue(result.First().Date == new DateTime(2021, 5, 10, 8, 38, 0));

        }
    }
}