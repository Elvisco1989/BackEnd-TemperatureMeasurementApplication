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
        TempRepo _Templist = new TempRepo();
        TemperatureMeasurement tMTest = new TemperatureMeasurement { Id = 6, InDoorTemperature = 17, OutDoorTemperature = 23, Date = new DateTime(2022, 6, 11, 10, 39, 12) };

        [TestMethod()]
        public void GetTempListTest()
        {
            var TempMeasurement = _Templist.GetTempList();
            Assert.AreEqual(5, TempMeasurement.Count());

            var TempMeasurementDate = _Templist.GetTempList(orderby: "Date");
            Assert.AreEqual(5, TempMeasurementDate.Count());
            Assert.AreEqual(TempMeasurementDate.First().Date, DateTime.Parse("10-05-2021 08:38:16"));

            var TempMeasurementInDoorTemperature = _Templist.GetTempList(orderby: "InDoorTemperature");
            Assert.AreEqual(5, TempMeasurementInDoorTemperature.Count());
            Assert.AreEqual(TempMeasurementInDoorTemperature.First().InDoorTemperature, 10);

            var TempMeasurementOutDoorTemperature = _Templist.GetTempList(orderby: "OutDoorTemperature");
            Assert.AreEqual(5, TempMeasurementOutDoorTemperature.Count());
            Assert.AreEqual(TempMeasurementOutDoorTemperature.First().OutDoorTemperature, -10);

            var TempMeasurementId = _Templist.GetTempList(orderby: "Id");
            Assert.AreEqual(5, TempMeasurementId.Count());
            Assert.AreEqual(TempMeasurementId.First().Id, 1);

            var TempMeasurementInDoorTemperatureDesc = _Templist.GetTempList(orderby: "InDoorTemperatureDesc");
            Assert.AreEqual(5, TempMeasurementInDoorTemperatureDesc.Count());
            Assert.AreEqual(TempMeasurementInDoorTemperatureDesc.First().InDoorTemperature, 23);

            var TempMeasurementOutDoorTemperatureDesc = _Templist.GetTempList(orderby: "OutDoorTemperatureDesc");
            Assert.AreEqual(5, TempMeasurementOutDoorTemperatureDesc.Count());
            Assert.AreEqual(TempMeasurementOutDoorTemperatureDesc.First().OutDoorTemperature, 28);

            var TempMeasurementDateDesc = _Templist.GetTempList(orderby: "DateDesc");
            Assert.AreEqual(5, TempMeasurementDateDesc.Count());
            Assert.AreEqual(TempMeasurementDateDesc.First().Date, DateTime.Parse("05-01-2022 02:12:11"));

            var TempMeasurementIdDesc = _Templist.GetTempList(orderby: "IdDesc");
            Assert.AreEqual(5, TempMeasurementIdDesc.Count());
            Assert.AreEqual(TempMeasurementIdDesc.First().Id, 5);

        }

        [TestMethod()]
        public void GetIdTest()
        {
            TemperatureMeasurement? tM = _Templist.GetID(1);
            Assert.IsNotNull(tM);
            Assert.AreEqual(1, tM.Id);
            Assert.IsNull(_Templist.GetID(100));


        }

        [TestMethod()]
        public void AddTest()
        {
            _Templist.Add(tMTest);
            IEnumerable<TemperatureMeasurement> TempMeasurement = _Templist.GetTempList();
            Assert.AreEqual(6, TempMeasurement.Count());
            Assert.IsNotNull(_Templist.GetID(6));


        }

        [TestMethod()]
        public void RemoveTest()
        {
            _Templist.Remove(5);
            IEnumerable<TemperatureMeasurement> TempMeasurement = _Templist.GetTempList();
            Assert.AreEqual(4, TempMeasurement.Count());
            Assert.IsNull(_Templist.GetID(5));
            Assert.IsNull(_Templist.Remove(100));


        }

        [TestMethod()]
        public void UpdateTest()
        {

            TemperatureMeasurement? tM = _Templist.Update(1, tMTest);
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
            string str = _Templist.ToString();
            Assert.IsTrue(str.Contains("Id: 1, InDoorTemperature: 20, OutDoorTemperature: 25, Date: 10-05-2021 08:38:16"));
        }


    }
}