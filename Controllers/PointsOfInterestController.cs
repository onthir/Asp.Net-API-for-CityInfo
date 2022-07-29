using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;


namespace CityInfo.API.Controllers
{

    [Route("api/cities/{id}/pointsofinterest")]
    [ApiController]

    public class PointsOfInterestController : ControllerBase
    {

        [HttpGet]

        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointsOfInterest);
        }

        [HttpGet("{pid}", Name = "GetPointOfInterest")]

        // get single point of interest
        public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pid)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(
                c => c.Id == cityId
            );

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointsOfInterest
                .FirstOrDefault(c => c.Id == pid);

            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(pointOfInterest);
        }

        // create point of interest
        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId,
        PointOfInterestForCreationDto pointOfInterest)
        {


            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(
                c => c.PointsOfInterest
            ).Max(p => p.Id);


            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(finalPointOfInterest);
            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    pointOfInterestId = finalPointOfInterest.Id
                },
                finalPointOfInterest
            );
        }

        // update resource
        [HttpPut("{pid}")]

        public ActionResult UpdatePointOfInterest(
            int cityId, int pid, PointOfInterestForUpdateDto pointOfInterest
        )
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest
                    .FirstOrDefault(c => c.Id == pid);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            // update
            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent();
        }


        // partial update
        [HttpPatch("{pid}")]
        public ActionResult PartiallyUpdatePointOfInterest(
            int cityId, int pid, JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument
        ){
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest
                    .FirstOrDefault(c => c.Id == pid);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            // apply patch
            var pointOfInterestToPatch = new PointOfInterestForUpdateDto(){
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description
            };
            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            if(!TryValidateModel(pointOfInterestToPatch)){
                return BadRequest(ModelState);
            }

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }


        // deleting a resource
        [HttpDelete("{pid}")]
        public ActionResult DeletePointOfInterest(int cityId, int pid){
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest
                    .FirstOrDefault(c => c.Id == pid);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            // execute delete
            city.PointsOfInterest.Remove(pointOfInterestFromStore);
            return NoContent();
        }

    }
}