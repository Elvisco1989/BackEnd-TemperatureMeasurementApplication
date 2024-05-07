using Microsoft.VisualStudio.TestTools.UnitTesting;
using HeatWave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatWave.Tests
{
    [TestClass()]
    public class TemperatureMeasurementTests
    {
        [TestMethod()]
        public void ToStringTest()
        {
            TemperatureMeasurement tM = new TemperatureMeasurement { Id = 1, InDoorTemperature = 20, OutDoorTemperature = 25, Date = new DateTime(2021, 5, 10, 8, 38, 16) };
            tM.ToString();

            Assert.AreEqual("Id: 1, InDoorTemperature: 20, OutDoorTemperature: 25, Date: 10-05-2021 08:38:16", tM.ToString());

            tM.Date = null;
            tM.ToString();
            Assert.AreEqual("Id: 1, InDoorTemperature: 20, OutDoorTemperature: 25, Date: N/A", tM.ToString());
        }

        [TestMethod()]
        public void ValidateInDoorTemperatureTest()
        {
            TemperatureMeasurement tM = new TemperatureMeasurement { Id = 1, InDoorTemperature = 20, OutDoorTemperature = 25, Date = new DateTime(2021, 5, 10, 8, 38, 16) };
            tM.ValidateInDoorTemperature();

            Assert.AreEqual(20, tM.InDoorTemperature);

            tM.InDoorTemperature = -51;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tM.ValidateInDoorTemperature());

            tM.InDoorTemperature = 51;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tM.ValidateInDoorTemperature());

            tM.InDoorTemperature = null;
            Assert.ThrowsException<ArgumentNullException>(() => tM.ValidateInDoorTemperature());
            
        }

        [TestMethod()]
        public void ValidateOutDoorTemperatureTest()
        {
            TemperatureMeasurement tM = new TemperatureMeasurement { Id = 1, InDoorTemperature = 20, OutDoorTemperature = 25, Date = new DateTime(2021, 5, 10, 8, 38, 16) };
            tM.ValidateOutDoorTemperature();

            Assert.AreEqual(25, tM.OutDoorTemperature);

            tM.OutDoorTemperature = -51;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tM.ValidateOutDoorTemperature());

            tM.OutDoorTemperature = 51;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tM.ValidateOutDoorTemperature());

            tM.OutDoorTemperature = null;
            Assert.ThrowsException<ArgumentNullException>(() => tM.ValidateOutDoorTemperature());
            
        }

        [TestMethod()]
        public void ValidateDateTimeTest()
        {
            TemperatureMeasurement tM = new TemperatureMeasurement { Id = 1, InDoorTemperature = 20, OutDoorTemperature = 25, Date = new DateTime(2021, 5, 10, 8, 38, 16) };
            tM.ValidateDateTime();

            Assert.AreEqual(new DateTime(2021, 5, 10, 8, 38, 16), tM.Date);

            tM.Date = new DateTime(2019, 12, 31);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tM.ValidateDateTime());

            tM.Date = new DateTime(2025, 1, 1);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tM.ValidateDateTime());

            tM.Date = DateTime.Now.AddSeconds(1);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tM.ValidateDateTime());

            tM.Date = null;
            Assert.ThrowsException<ArgumentNullException>(() => tM.ValidateDateTime());


            
        }

        [TestMethod()]
        public void ValidateTest()
        {
            TemperatureMeasurement tM = new TemperatureMeasurement { Id = 1, InDoorTemperature = 20, OutDoorTemperature = 25, Date = new DateTime(2021, 5, 10, 8, 38, 16) };
            tM.Validate();

            Assert.AreEqual(20, tM.InDoorTemperature);
            Assert.AreEqual(25, tM.OutDoorTemperature);
            Assert.AreEqual(new DateTime(2021, 5, 10, 8, 38, 16), tM.Date);

            tM.InDoorTemperature = -51;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tM.Validate());

            tM.InDoorTemperature = 20;
            tM.OutDoorTemperature = -51;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tM.Validate());

            tM.OutDoorTemperature = 25;
            tM.Date = new DateTime(2019, 12, 31);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tM.Validate());

            tM.Date = new DateTime(2021, 5, 10, 8, 38, 16);
            tM.InDoorTemperature = null;
            Assert.ThrowsException<ArgumentNullException>(() => tM.Validate());

            tM.InDoorTemperature = 20;
            tM.OutDoorTemperature = null;
            Assert.ThrowsException<ArgumentNullException>(() => tM.Validate());

            tM.OutDoorTemperature = 25;
            tM.Date = null;
            Assert.ThrowsException<ArgumentNullException>(() => tM.Validate());
            
        }
    }
}