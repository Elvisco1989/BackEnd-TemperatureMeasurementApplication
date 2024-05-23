using Microsoft.VisualBasic;

namespace HeatWave

{
    public class TemperatureMeasurement
    {
        public int Id { get; set; }

        public double? InDoorTemperature { get; set; }
        public double? OutDoorTemperature { get; set; }

        public DateTime? Date { get; set; }


        public override string ToString()
        {
            // Return a concatenated string including values for Id, InDoorTemperature, OutDoorTemperature, and formatted date
            return "Id: " + Id + ", InDoorTemperature: " + InDoorTemperature + ", OutDoorTemperature: " + OutDoorTemperature + ", Date: " + (Date.HasValue ? Date.Value.ToString("yyyy-MM-ddTHH:mm:ss") : "N/A");
        }

        // Validate InDoorTemperature property
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

        // Validate OutDoorTemperature property
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

        // Validate DateTime property
        public void ValidateDateTime()
        {
            if (Date == null)
            {
                throw new ArgumentNullException("DateTime is required");
            }

            if (Date.Value.Year < 2020 || Date.Value.Year > 2024)
            {
                throw new ArgumentOutOfRangeException("DateTime must be between 2020 and 2024");
            }
        }

        // Validate InDoorTemperature, OutDoorTemperature og Date properties
        public void Validate()
        {
            ValidateInDoorTemperature();
            ValidateOutDoorTemperature();
            ValidateDateTime();
        }
    }
}
