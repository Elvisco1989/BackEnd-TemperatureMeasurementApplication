namespace HeatWave

{
    public class TemperatureMeasurement
    {
        public int Id { get; set; }

        public int? InDoorTemperature { get; set; }
        public int? OutDoorTemperature { get; set; }

        public DateTime? Date { get; set; }

        public override string ToString()
        {
            return "Id: " + Id + ", InDoorTemperature: " + InDoorTemperature + ", OutDoorTemperature: " + OutDoorTemperature + ", DateTime: " + DateTime.ToString("MM-dd-yyyy HH:mm:ss");
        }

        public void ValidateInDoorTemperature()
        {
            if (InDoorTemperature == null)
            {
                throw new ArgumentNullException("InDoorTemperature is required");
            }
            if (InDoorTemperature < -50 || InDoorTemperature > 50)
            {
                throw new ArgumentOutOfRangeException("InDoorTemperature must be between -50 and 50 degrees");
            }
        }

        public void ValidateOutDoorTemperature()
        {
            if (OutDoorTemperature == null)
            {
                throw new ArgumentNullException("OutDoorTemperature is required");
            }
            if (OutDoorTemperature < -50 || OutDoorTemperature > 50)
            {
                throw new ArgumentOutOfRangeException("OutDoorTemperature must be between -50 and 50 degrees");
            }
        }

        public void ValidateDateTime()
        {
            if (Date == null)
            {
                throw new ArgumentNullException("DateTime is required");
            }
            if (Date < new DateTime(2020, 1, 1) || Date > DateTime.Now)
            {
                throw new ArgumentOutOfRangeException("DateTime must be between 01-01-2020 and now");
            }
        }

        public void Validate()
        {
            ValidateInDoorTemperature();
            ValidateOutDoorTemperature();
            ValidateDateTime();
        }
    }
}
