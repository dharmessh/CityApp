using City.Models;

namespace City
{
    public class LocalDataStorage
    {
        public List<CityDto> Cities { get; set; }
        public static LocalDataStorage Current { get;  } = new LocalDataStorage();

        public LocalDataStorage()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "Delhi",
                    Description = "India's Capital City.",
                    Places = new List<PlacesDto>()
                    {
                        new PlacesDto()
                        {
                            PlaceId = 1,
                            PlaceName = "Parliament House",
                            PlaceDescription = "Lok Sabha and Rajya Sabha"
                        },
                        new PlacesDto()
                        {
                            PlaceId = 2,
                            PlaceName = "Qutub Minar",
                            PlaceDescription = "One of the Ancient Construction in Delhi"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Mumbai",
                    Description = "India's Finance Capital.",
                    Places = new List<PlacesDto>()
                    {
                        new PlacesDto()
                        {
                            PlaceId = 1,
                            PlaceName = "Taj Mahel Hotel",
                            PlaceDescription = "One of the 5 Star Heritage Hotel Owned By Tatas"
                        },
                        new PlacesDto()
                        {
                            PlaceId = 2,
                            PlaceName = "Bandra Worli Sea Link",
                            PlaceDescription = "A Prestigious Bridge in Mumbai"
                        },
                        new PlacesDto()
                        {
                            PlaceId = 3,
                            PlaceName = "Western Naval Command",
                            PlaceDescription = "Headquaerters of Indian Navy's On of the Command"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Kolkatta",
                    Description = "India's Old Capital City.",
                    Places = new List<PlacesDto>()
                    {
                        new PlacesDto()
                        {
                            PlaceId = 1,
                            PlaceName = "Howrah Bridge",
                            PlaceDescription = "Bridge in West Bengal on Hugli River"
                        },
                        new PlacesDto()
                        {
                            PlaceId = 2,
                            PlaceName = "The Victoria Memorial",
                            PlaceDescription = "Pride of Kolkata"
                        }
                    }
                }
            };
        }
    }
}
