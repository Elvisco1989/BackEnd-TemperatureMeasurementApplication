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
            // Arrange - opretter en ny instans af TemperatureMeasurement
            TemperatureMeasurement tM = new TemperatureMeasurement { Id = 1, InDoorTemperature = 20, OutDoorTemperature = 25, Date = new DateTime(2021, 5, 10, 8, 38, 16) };

            // Act - kalder ToString metoden
            tM.ToString();

            // Assert - tester at ToString metoden returnerer den forventede string og at Date er null og at metoden ikke kaster en exception
            Assert.AreEqual("Id: 1, InDoorTemperature: 20, OutDoorTemperature: 25, Date: 2021-05-10T08:38:16", tM.ToString());
            tM.Date = null;
            tM.ToString();
            Assert.AreEqual("Id: 1, InDoorTemperature: 20, OutDoorTemperature: 25, Date: N/A", tM.ToString());
        }

        [TestMethod()]
        public void ValidateInDoorTemperatureTest()
        {
            // Arrange - opretter en ny instans af TemperatureMeasurement
            TemperatureMeasurement tM = new TemperatureMeasurement { Id = 1, InDoorTemperature = 20, OutDoorTemperature = 25, Date = new DateTime(2021, 5, 10, 8, 38, 16) };

            // Act - kalder ValidateInDoorTemperature metoden
            tM.ValidateInDoorTemperature();

            // Assert - tester at InDoorTemperature er 20 og at metoden ikke kaster en exception
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
            // Arrange - opretter en ny instans af TemperatureMeasurement
            TemperatureMeasurement tM = new TemperatureMeasurement { Id = 1, InDoorTemperature = 20, OutDoorTemperature = 25, Date = new DateTime(2021, 5, 10, 8, 38, 16) };

            // Act - kalder ValidateOutDoorTemperature metoden
            tM.ValidateOutDoorTemperature();


            // Assert - tester at OutDoorTemperature er 25 og at metoden ikke kaster en exception
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
            // Arrange - opretter en ny instans af TemperatureMeasurement
            TemperatureMeasurement tM = new TemperatureMeasurement { Id = 1, InDoorTemperature = 20, OutDoorTemperature = 25, Date = new DateTime(2021, 5, 10, 8, 38, 16) };

            // Act - kalder ValidateDateTime metoden
            tM.ValidateDateTime();

            // Assert - tester at Date er 2021-05-10T08:38:16 og at metoden ikke kaster en exception 
            Assert.AreEqual(new DateTime(2021, 5, 10, 8, 38, 16), tM.Date);

            tM.Date = new DateTime(2019, 12, 31);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tM.ValidateDateTime());

            tM.Date = null;
            Assert.ThrowsException<ArgumentNullException>(() => tM.ValidateDateTime());


        }

        [TestMethod()]
        public void ValidateTest()
        {
            // Arrange - opretter en ny instans af TemperatureMeasurement
            TemperatureMeasurement tM = new TemperatureMeasurement { Id = 1, InDoorTemperature = 20, OutDoorTemperature = 25, Date = new DateTime(2021, 5, 10, 8, 38, 16) };

            // Act - kalder Validate metoden
            tM.Validate();


            // Assert - tester at InDoorTemperature er 20, OutDoorTemperature er 25 og Date er 2021-05-10T08:38:16 og at metoden ikke kaster en exception
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