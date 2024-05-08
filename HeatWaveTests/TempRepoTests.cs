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
        TemperatureMeasurement tM = new TemperatureMeasurement { Id = 6, InDoorTemperature = 17, OutDoorTemperature = 23, Date = new DateTime(2022, 6, 11, 10, 39, 12) };


        [TestMethod()]
        public void GetTempListTest()
        {
            var TempMeasurement = _Templist.GetTempList();
            Assert.AreEqual(5, TempMeasurement.Count);


        }

        public void GetId()
        {
            TemperatureMeasurement? tM = _Templist.GetTempID(1);
            Assert.IsNotNull(tM);
            Assert.AreEqual(1, tM.Id);
            Assert.IsNull(_Templist.GetTempID(100));


        }

        public void AddTempMeasurementTest()
        {
            _Templist.AddTempMeasurement(tM);
            Assert.AreEqual(6, _Templist.GetTempList().Count);
            Assert.AreEqual(6, _Templist.GetTempID(6).Id);
        }

        public void RemoveTempMeasurementTest()
        {
            _Templist.RemoveTempMeasurement(5);
            Assert.AreEqual(4, _Templist.GetTempList().Count);
            Assert.IsNull(_Templist.GetTempID(5));

        }

        public void UpdateTempMeasurementTest()
        {
            _Templist.UpdateTempMeasurement(1, tM);
            Assert.IsNotNull(_Templist.GetTempID(1));
            Assert.AreEqual(17.0, _Templist.GetTempID(1).InDoorTemperature);
            Assert.AreEqual(23.0, _Templist.GetTempID(1).OutDoorTemperature);
            Assert.AreEqual(new DateTime(2022, 6, 11, 10, 39, 12), _Templist.GetTempID(1).Date);

        }

        public override string ToString()
        {
            _Templist.GetTempList();
            Assert.AreEqual("Id: 1, InDoorTemperature: 20, OutDoorTemperature: 25, Date: 10-05-2021 08:38:16", _Templist.ToString());
        }


    }
}