using System;

namespace Repository.Models.AccuWeatherAPI.CurrentConditions
{
    public class Conditions
    {
        public DateTime LocalObservationDateTime { get; set; }
        public double EpochTime { get; set; }
        public string WeatherText { get; set; }
        public int WeatherIcon { get; set; }
        public bool HasPrecipitation { get; set; }
        public string PrecipitationType { get; set; }
        public bool IsDayTime { get; set; }
        public TemperatureData Temperature { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
    }
}
