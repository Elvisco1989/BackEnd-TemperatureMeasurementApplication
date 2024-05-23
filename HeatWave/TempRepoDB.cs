using Microsoft.EntityFrameworkCore;

namespace HeatWave
{
    public class TempRepoDB
    {
        private readonly TempDBContext _context;

        //Constructor der initialiserer repository med en databasekontekst
        public TempRepoDB(TempDBContext context)
        {
            _context = context;
        }

        //Henter en liste a temperaturmålinger valgfrit filtreret på dato og sorteret efter angivet kriterie
        public IEnumerable<TemperatureMeasurement> GetTempList(DateTime? date = null, string? orderBy = null)
        {
            // Henter alle temperaturmålinger fra databasen
            IEnumerable<TemperatureMeasurement> result = new List<TemperatureMeasurement>(_context.TemperatureMeasurement);
            if (date != null)
            {
                //Filterer målinger efter dato hvis det er angivet
                result = result.Where(tM => tM.Date.HasValue && tM.Date.Value == date.Value);
            }

            //Sorter målingerne efter det angivne orderBy parameter
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

        //Henter en temperaturmåling ud fra et givet ID
        public TemperatureMeasurement? GetID(int id)
        {
            return _context.TemperatureMeasurement.FirstOrDefault(tM => tM.Id == id);
        }

        //Tilføjer en temperaturmåling til databasen
        public TemperatureMeasurement Add(TemperatureMeasurement tM)
        {
            // Validere temperatur målinger
            tM.Validate();

            //Tilføjer måling til databasekonteksten og gemmer ændringer i databasen
            _context.TemperatureMeasurement.Add(tM);
            _context.SaveChanges();
            return tM;
        }

        //Sletter en temperaturmåling fra databasen ud fra dens ID
        public TemperatureMeasurement Delete(int id)
        {

            //Henter temperaturmåling ud fra ID
            TemperatureMeasurement? tM = GetID(id);

            //Hvis målingen ikke findes, returneres null
            if (tM is null) 
            {
                return null;
            }

            //Fjerner måling fra databasekonteksten og gemmer ændringer i databasen
            _context.TemperatureMeasurement.Remove(tM);
            _context.SaveChanges();
            return tM;
        }

        //Opdaterer en temperaturmåling i databasen ud fra dens ID
        public TemperatureMeasurement Update(int id, TemperatureMeasurement tM)
        {
            //Validerer nye temperaturmålinger
            tM.Validate();

            //Henter eksisterende måling ud fra ID
            TemperatureMeasurement? existingtM = GetID(id);

            //Hvis målingen ikke findes, returneres null
            if (existingtM is null)
            {
                return null;
            }

            //Opdaterer målingens værdier og gemmer ændringer i databasen
            existingtM.InDoorTemperature = tM.InDoorTemperature;
            existingtM.OutDoorTemperature = tM.OutDoorTemperature;
            existingtM.Date = tM.Date;
            _context.SaveChanges();
            return existingtM;
        }
    }
}
