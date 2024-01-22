using City.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace City.Controllers
{
    [Route("api/cities/{cityId}/places")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PlacesDto>> GetPlaces(int cityId)
        {

            var cities = LocalDataStorage.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(cities == null)
            {
                return NotFound();
            }
            return Ok(cities.Places);
        }

        [HttpGet("{placeId}", Name = "GetPlaceByPlaceId")]
        public ActionResult<IEnumerable<PlacesDto>> GetPlaceByPlaceId(int cityId, int placeId)
        {
            var cities = LocalDataStorage.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(cities == null)
            {
                return NotFound();
            }

            var placeInCity = cities.Places.FirstOrDefault(p => p.PlaceId == placeId);
            if(placeInCity == null)
            {
                return NotFound();
            }

            return Ok(placeInCity);
        }

        [HttpPost]
        public ActionResult<PlacesDto> AddPlaceInACity(int cityId, [FromBody] PlaceCreation placeCreation)
        {
            //Either this
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            //or Add
            //Error Attributes in Model Fields 
            
            var checkCity = LocalDataStorage.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(checkCity == null)
            {
                return NotFound();
            }

            var lastPlaceIdInCity = LocalDataStorage.Current.Cities.SelectMany(c => c.Places).Max(m => m.PlaceId);

            var newPlaceToBeAdded = new PlacesDto()
            {
                PlaceId = lastPlaceIdInCity++,
                PlaceName = placeCreation.PlaceName,
                PlaceDescription = placeCreation.PlaceDescription
            };

            checkCity.Places.Add(newPlaceToBeAdded);

            return CreatedAtRoute("GetPlaceByPlaceId",
                new
                {
                    cityId = cityId,
                    placeId = newPlaceToBeAdded.PlaceId
                },
                newPlaceToBeAdded);
        }

        [HttpPut("{placeId}")]
        public ActionResult UpdatePlaceInCity(int cityId, int placeId, PlaceCreation placeCreation)
        {
            var cityCheck = LocalDataStorage.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(cityCheck == null)
            {
                return NotFound();
            }

            var placeCheck = cityCheck.Places.FirstOrDefault(p => p.PlaceId == placeId);
            if(placeCheck == null)
            {
                return NotFound();
            }

            placeCheck.PlaceName = placeCreation.PlaceName;
            placeCheck.PlaceDescription = placeCreation.PlaceDescription;

            return NoContent();
        }

        [HttpPatch("{placeId}")]
        public ActionResult PartialUpdatePlaceInCityWithJSONPatch(int cityId, int placeId, JsonPatchDocument<PlaceCreation> patchDocument)
        {
            var cityCheck = LocalDataStorage.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(cityCheck == null)
            {
                return NotFound();
            }

            var placeCheck = cityCheck.Places.FirstOrDefault(p => p.PlaceId ==placeId);
            if(placeCheck == null)
            {
                return NotFound();
            }

            var updatePlace = new PlaceCreation()
            {
                PlaceName = placeCheck.PlaceName,
                PlaceDescription = placeCheck.PlaceDescription
            };

            patchDocument.ApplyTo(updatePlace, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(!TryValidateModel(updatePlace))
            {
                return BadRequest();
            }

            placeCheck.PlaceName = updatePlace.PlaceName;
            placeCheck.PlaceDescription = updatePlace.PlaceDescription;

            return NoContent();
        }


        [HttpDelete("{placeId}")]
        public ActionResult DeletePlaceByPlaceId(int cityId, int placeId)
        {
            var cityCheck = LocalDataStorage.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(cityCheck == null)
            {
                return NotFound();
            }

            var placeCheck = cityCheck.Places.FirstOrDefault(p => p.PlaceId == placeId);
            if(placeCheck == null)
            {
                return NotFound();
            }

            cityCheck.Places.Remove(placeCheck);
            return NoContent();
        }
    }
}
