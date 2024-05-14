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
            return "Id: " + Id + ", InDoorTemperature: " + InDoorTemperature + ", OutDoorTemperature: " + OutDoorTemperature + ", Date: " + (Date.HasValue ? Date.Value.ToString("G") : "N/A");
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

            if (Date.Value > DateTime.Now)
            {
                throw new ArgumentOutOfRangeException("DateTime cannot be in the future");
            }

            if (Date.Value.Year < 2020 || Date.Value.Year > 2024)
            {
                throw new ArgumentOutOfRangeException("DateTime must be between 2020 and 2024");
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
