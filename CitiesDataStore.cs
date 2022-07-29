using CityInfo.API.Models;

namespace CityInfo.API{

    public class CitiesDataStore{

        public List<CityDto> Cities {get; set;}

        public static CitiesDataStore Current {get;} = new CitiesDataStore();


        public CitiesDataStore(){
            Cities = new List<CityDto>(){
                new CityDto(){
                    Id = 1,
                    Name = "New York City",
                    Description = "Did you know? NYC has Central Park.",
                    PointsOfInterest = new List<PointOfInterestDto>(){
                        new PointOfInterestDto(){
                            Id=5,
                            Name="Central Park",
                            Description="Central Park is peaceful and beautiful."
                        },
                        new PointOfInterestDto(){
                            Id=6,
                            Name="Empire State Building",
                            Description="Empire State Building is where it's at."
                        },
                    }
                },
                new CityDto(){
                    Id = 2,
                    Name = "Monroe",
                    Description = "Did you know? Monroe has Bibek."
                },
                new CityDto(){
                    Id = 3,
                    Name = "Dallas",
                    Description = "Did you know? Dallas has heat stroke."
                },
            };
        }
    }
}