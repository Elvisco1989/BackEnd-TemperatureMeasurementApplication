using Microsoft.EntityFrameworkCore;

namespace HeatWave
{
    public class TempRepoDB
    {
        private readonly TempDBContext _context;


        public TempRepoDB(TempDBContext context)
        {
            _context = context;
        }

        public IEnumerable<TemperatureMeasurement> GetTempList(DateTime? date = null, string? orderBy = null)
        {
            IEnumerable<TemperatureMeasurement> result = new List<TemperatureMeasurement>(_context.TemperatureMeasurements);
            if (date != null)
            {
                result = result.Where(tM => tM.Date.HasValue && tM.Date.Value == date.Value);
            }

            if (orderBy != null)
            {
                switch (orderBy)
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
                }
            }
            return result;
        }

        public TemperatureMeasurement? GetID(int id)
        {
            return _context.TemperatureMeasurements.FirstOrDefault(tM => tM.Id == id);
        }

        public TemperatureMeasurement Add(TemperatureMeasurement tM)
        {
            tM.Validate();
            _context.TemperatureMeasurements.Add(tM);
            _context.SaveChanges();
            return tM;
        }

        public TemperatureMeasurement Delete(int id)
        {
            TemperatureMeasurement? tM = GetID(id);
            if (tM is null) 
            {
                return null;
            }
            _context.TemperatureMeasurements.Remove(tM);
            _context.SaveChanges();
            return tM;
        }

        public TemperatureMeasurement Update(int id, TemperatureMeasurement tM)
        {
            tM.Validate();
            TemperatureMeasurement? existingtM = GetID(id);
            if (existingtM is null)
            {
                return null;
            }
            existingtM.InDoorTemperature = tM.InDoorTemperature;
            existingtM.OutDoorTemperature = tM.OutDoorTemperature;
            existingtM.Date = tM.Date;
            _context.SaveChanges();
            return existingtM;
        }





    }
}
