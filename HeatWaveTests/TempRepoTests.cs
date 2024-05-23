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
    public class TempRepoTests
    {
        // Arrange - opretter en ny instans af TempRepo
        TempRepo _Templist = new TempRepo();

        // Arrange - opretter en ny instans af TemperatureMeasurement
        TemperatureMeasurement tMTest = new TemperatureMeasurement { Id = 6, InDoorTemperature = 17, OutDoorTemperature = 23, Date = new DateTime(2022, 6, 11, 10, 39, 12) };

        [TestMethod()]
        public void GetTempListTest()
        {
            // Act - kalder GetTempList metoden
            var TempMeasurement = _Templist.GetTempList();

            // Assert - tester at der ikke er nogen elementer i listen
            Assert.AreEqual(5, TempMeasurement.Count());

            // Act - kalder GetTempList metoden og sorterer efter dato
            var TempMeasurementDate = _Templist.GetTempList(orderby: "Date");

            // Assert - tester at der er 5 elementer i listen og at det første element er det forventede
            Assert.AreEqual(5, TempMeasurementDate.Count());
            Assert.AreEqual(TempMeasurementDate.First().Date, DateTime.Parse("10-05-2021 08:38:00"));

            // Act - kalder GetTempList metoden og sorterer efter indendørs temperatur
            var TempMeasurementInDoorTemperature = _Templist.GetTempList(orderby: "InDoorTemperature");

            // Assert - tester at der er 5 elementer i listen og at det første element er det forventede
            Assert.AreEqual(5, TempMeasurementInDoorTemperature.Count());
            Assert.AreEqual(TempMeasurementInDoorTemperature.First().InDoorTemperature, 10);

            // Act - kalder GetTempList metoden og sorterer efter udendørs temperatur
            var TempMeasurementOutDoorTemperature = _Templist.GetTempList(orderby: "OutDoorTemperature");

            // Assert - tester at der er 5 elementer i listen og at det første element er det forventede
            Assert.AreEqual(5, TempMeasurementOutDoorTemperature.Count());
            Assert.AreEqual(TempMeasurementOutDoorTemperature.First().OutDoorTemperature, -10);

            // Act - kalder GetTempList metoden og sorterer efter Id
            var TempMeasurementId = _Templist.GetTempList(orderby: "Id");

            // Assert - tester at der er 5 elementer i listen og at det første element er det forventede
            Assert.AreEqual(5, TempMeasurementId.Count());
            Assert.AreEqual(TempMeasurementId.First().Id, 1);

            // Act - kalder GetTempList metoden og sorterer efter indendørs temperatur i faldende rækkefølge
            var TempMeasurementInDoorTemperatureDesc = _Templist.GetTempList(orderby: "InDoorTemperatureDesc");

            // Assert - tester at der er 5 elementer i listen og at det første element er det forventede
            Assert.AreEqual(5, TempMeasurementInDoorTemperatureDesc.Count());
            Assert.AreEqual(TempMeasurementInDoorTemperatureDesc.First().InDoorTemperature, 23);

            // Act - kalder GetTempList metoden og sorterer efter udendørs temperatur i faldende rækkefølge
            var TempMeasurementOutDoorTemperatureDesc = _Templist.GetTempList(orderby: "OutDoorTemperatureDesc");

            // Assert - tester at der er 5 elementer i listen og at det første element er det forventede
            Assert.AreEqual(5, TempMeasurementOutDoorTemperatureDesc.Count());
            Assert.AreEqual(TempMeasurementOutDoorTemperatureDesc.First().OutDoorTemperature, 28);

            // Act - kalder GetTempList metoden og sorterer efter dato i faldende rækkefølge
            var TempMeasurementDateDesc = _Templist.GetTempList(orderby: "DateDesc");

            // Assert - tester at der er 5 elementer i listen og at det første element er det forventede
            Assert.AreEqual(5, TempMeasurementDateDesc.Count());
            Assert.AreEqual(TempMeasurementDateDesc.First().Date, DateTime.Parse("05-01-2023 02:12:11"));

            // Act - kalder GetTempList metoden og sorterer efter Id i faldende rækkefølge
            var TempMeasurementIdDesc = _Templist.GetTempList(orderby: "IdDesc");

            // Assert - tester at der er 5 elementer i listen og at det første element er det forventede
            Assert.AreEqual(5, TempMeasurementIdDesc.Count());
            Assert.AreEqual(TempMeasurementIdDesc.First().Id, 5);

        }

        [TestMethod()]
        public void GetIdTest()
        {
            // Act - kalder GetID metoden
            TemperatureMeasurement? tM = _Templist.GetID(1);

            // Assert - tester at der er et element i listen og at det er det forventede element 
            Assert.IsNotNull(tM);
            Assert.AreEqual(1, tM.Id);
            Assert.IsNull(_Templist.GetID(100));


        }

        [TestMethod()]
        public void AddTest()
        {
            // Act - kalder Add metoden
            _Templist.Add(tMTest);

            // Assert - tester at der er 6 elementer i listen og at det sidste element er det forventede element
            IEnumerable<TemperatureMeasurement> TempMeasurement = _Templist.GetTempList();
            Assert.AreEqual(6, TempMeasurement.Count());
            Assert.IsNotNull(_Templist.GetID(6));


        }

        [TestMethod()]
        public void RemoveTest()
        {
            // Act - kalder Remove metoden
            _Templist.Remove(5);

            // Assert - tester at der er 4 elementer i listen og at det sidste element er det forventede element
            IEnumerable<TemperatureMeasurement> TempMeasurement = _Templist.GetTempList();
            Assert.AreEqual(4, TempMeasurement.Count());
            Assert.IsNull(_Templist.GetID(5));
            Assert.IsNull(_Templist.Remove(100));


        }

        [TestMethod()]
        public void UpdateTest()
        {

            // Act - kalder Update metoden
            TemperatureMeasurement? tM = _Templist.Update(1, tMTest);

            // Assert - tester at der er et element i listen og at det er det forventede element og at det er opdateret korrekt
            Assert.IsNotNull(tM);
            Assert.AreEqual(1, tM.Id);
            Assert.AreEqual(17, tM.InDoorTemperature);
            Assert.AreEqual(23, tM.OutDoorTemperature);
            Assert.AreEqual(new DateTime(2022, 6, 11, 10, 39, 12), tM.Date);
            Assert.IsNull(_Templist.Update(100, tMTest));

        }

        [TestMethod()]
        public void ToStringTest()
        {
            // Act - kalder ToString metoden
            string str = _Templist.ToString();

            // Assert - tester at ToString metoden returnerer den forventede string
            Assert.IsTrue(str.Contains("Id: 1, InDoorTemperature: 20, OutDoorTemperature: 25, Date: 2021-05-10T08:38:00"));
        }


    }
}