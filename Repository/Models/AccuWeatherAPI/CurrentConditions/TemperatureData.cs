namespace Repository.Models.AccuWeatherAPI.CurrentConditions
{
    public class TemperatureData
    {
        public MetricTemperatur Metric { get; set; }
        public ImperialTemperature Imperial { get; set; }
    }
}
