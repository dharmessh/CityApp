namespace City.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ICollection<PlacesDto> Places { get; set; } = new List<PlacesDto>(); 
        public int NoOfPlaces
        {
            get { return Places.Count; }
        }
    }
}
