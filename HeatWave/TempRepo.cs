namespace HeatWave
{
    public class TempRepo
    {
        private List<TemperatureMeasurement> _TempList = new List<TemperatureMeasurement>
        {
            new TemperatureMeasurement { Id = 1, InDoorTemperature = 20, OutDoorTemperature = 25, Date = new DateTime(2021, 5, 10, 8, 38, 16) },
            new TemperatureMeasurement { Id = 2, InDoorTemperature = 21, OutDoorTemperature = 26, Date = new DateTime(2021, 5, 11, 8, 38, 16) },
            new TemperatureMeasurement { Id = 3, InDoorTemperature = 22, OutDoorTemperature = 27, Date = new DateTime(2021, 5, 12, 8, 38, 16) },
            new TemperatureMeasurement { Id = 4, InDoorTemperature = 23, OutDoorTemperature = 28, Date = new DateTime(2021, 5, 13, 8, 38, 16) },
            new TemperatureMeasurement { Id = 5, InDoorTemperature = 24, OutDoorTemperature = 29, Date = new DateTime(2021, 5, 14, 8, 38, 16) }
        };

        public List<TemperatureMeasurement> GetTempList()
        {
            return _TempList;
        }




    }
}
