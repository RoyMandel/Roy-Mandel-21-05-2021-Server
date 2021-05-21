namespace Repository.Models.AccuWeatherAPI.AutoComplete
{
    public class Place
    {
        public int Version { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
        public int Rank { get; set; }
        public string LocalizedName { get; set; }
        public CountryInfo Country { get; set; }
        public AdministrativeAreaData AdministrativeArea { get; set; }
    }
}
