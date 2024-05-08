namespace HeatWave
{
    public class TempRepo
    {
        private int _nextId;
        private List<TemperatureMeasurement> _TempList = new List<TemperatureMeasurement>
        {
            new TemperatureMeasurement { Id = 1, InDoorTemperature = 20, OutDoorTemperature = 25, Date = new DateTime(2021, 5, 10, 8, 38, 16) },
            new TemperatureMeasurement { Id = 2, InDoorTemperature = 21, OutDoorTemperature = 26, Date = new DateTime(2021, 5, 11, 8, 38, 16) },
            new TemperatureMeasurement { Id = 3, InDoorTemperature = 22, OutDoorTemperature = 27, Date = new DateTime(2021, 5, 12, 8, 38, 16) },
            new TemperatureMeasurement { Id = 4, InDoorTemperature = 23, OutDoorTemperature = 28, Date = new DateTime(2021, 5, 13, 8, 38, 16) },
            new TemperatureMeasurement { Id = 5, InDoorTemperature = 24, OutDoorTemperature = 29, Date = new DateTime(2021, 5, 14, 8, 38, 16) }
        };

        public IEnumerable<TemperatureMeasurement> GetTempList(DateTime? date = null, string? orderby = null)
        {
            IEnumerable<TemperatureMeasurement> result = new List<TemperatureMeasurement>(_TempList);
            if (date != null)
            {
                result = result.Where(tM => tM.Date == date.Value.Date);
            }

            if (orderby != null)
            {
                switch (orderby)
                {
                    case "InDoorTemperature":
                        result = result.OrderBy(tM => tM.InDoorTemperature);
                        break;
                    case "OutDoorTemperature":
                        result = result.OrderBy(tM => tM.OutDoorTemperature);
                        break;
                    case "Date":
                        result = result.OrderBy(tM => tM.Date);
                        break;
                    case "Id":
                        result = result.OrderBy(tM => tM.Id);
                        break;
                    case "InDoorTemperatureDesc":
                        result = result.OrderByDescending(tM => tM.InDoorTemperature);
                        break;
                    case "OutDoorTemperatureDesc":
                        result = result.OrderByDescending(tM => tM.OutDoorTemperature);

                        break;
                    case "DateDesc":
                        result = result.OrderByDescending(tM => tM.Date);
                        break;
                    case "IdDesc":
                        result = result.OrderByDescending(tM => tM.Id);
                        break;
                    default:
                        break;
                }
            }
            return result;

        }

        public TemperatureMeasurement? GetID(int id)
        {

            return _TempList.Find(tM => tM.Id == id);
        }

        public TemperatureMeasurement Add(TemperatureMeasurement tM)
        {
            tM.Validate();
            tM.Id = _nextId++;
            _TempList.Add(tM);
            return tM;
        }

        public TemperatureMeasurement Remove(int Id)
        {
            TemperatureMeasurement? existingtM = GetID(Id);
            if (existingtM == null)
            {
                return null;
            }
            _TempList.Remove(existingtM);
            return existingtM;
        }

        public TemperatureMeasurement Update(int Id, TemperatureMeasurement tM)
        {
            tM.Validate();
            TemperatureMeasurement? existingtM = GetID(Id);
            if (existingtM == null)
            {
                return null;
            }
            existingtM.InDoorTemperature = tM.InDoorTemperature;
            existingtM.OutDoorTemperature = tM.OutDoorTemperature;
            existingtM.Date = tM.Date;
            return existingtM;
        }

        public override string ToString()
        {
            return string.Join("\n ", _TempList);
        }
    }
}
