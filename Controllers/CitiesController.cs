using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;

namespace CityInfo.API.Controllers{

    [ApiController]

    [Route("api/cities")]
    public class CitiesController: ControllerBase{

        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities(){
                return Ok(CitiesDataStore.Current.Cities);
            
        }

        // get a single city
        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id){

            var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);

            // return 404 not found if the resource is not found
            if (cityToReturn == null){
                return NotFound();
            }
            return Ok(cityToReturn);
        }
    }
}