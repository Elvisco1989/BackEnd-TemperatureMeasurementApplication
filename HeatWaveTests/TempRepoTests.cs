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


        }

        [TestMethod()]
        public void UpdateTest()
        {
            //_Templist.Update(1, tM);
            //Assert.IsNotNull(_Templist.GetID(1));
            //Assert.AreEqual(17.0, _Templist.GetID(1).InDoorTemperature);
            //Assert.AreEqual(23.0, _Templist.GetID(1).OutDoorTemperature);
            //Assert.AreEqual(new DateTime(2022, 6, 11, 10, 39, 12), _Templist.GetID(1).Date);

            TemperatureMeasurement? tM = _Templist.Update(1, tMTest);
            Assert.IsNotNull(tM);
            Assert.AreEqual(1, tM.Id);
            Assert.AreEqual(17, tM.InDoorTemperature);
            Assert.AreEqual(23, tM.OutDoorTemperature);
            Assert.AreEqual(new DateTime(2022, 6, 11, 10, 39, 12), tM.Date);

        }

        [TestMethod()]
        public void ToStringTest()
        {
            IEnumerable<TemperatureMeasurement> TempMeasurement = _Templist.GetTempList();
            Assert.AreEqual("Id: 1, InDoorTemperature: 20, OutDoorTemperature: 25, Date: 10-05-2021 08:38:16", TempMeasurement.ElementAt(0).ToString());
        }


    }
}