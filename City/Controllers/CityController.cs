using City.Models;
using City.Services;
using Microsoft.AspNetCore.Mvc;

namespace City.Controllers
{
    [ApiController]
    [Route("api/cities")]         //remain common to all the APIs for route otherwise need to add --> [HttpGet("api/cities")]
    public class CityController : ControllerBase
    {
        private readonly ILogger<CityController> _logger;
        private readonly ILocalMailService _mailService;
        public CityController(ILogger<CityController> logger, ILocalMailService localMailService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = localMailService ?? throw new ArgumentNullException(nameof(localMailService));

            //Request service from container directly
            //HttpContext.RequestServices.GetService()
        }

        //[HttpGet("api/cities")]
        [HttpGet]
        public JsonResult GetNames()
        {
            return new JsonResult(
                new List<object>()
                {
                    new {Id =1, Name = "NewYork"},
                    new {Id =2, Name = "Tokyo"},
                    new {Id =2, Name = "Jamnagar"}
                });
        }

        [HttpGet("all")]    //should be unique at each action level
        public JsonResult GetAllCities()
        {
            return new JsonResult(LocalDataStorage.Current.Cities);
        }

        //Json Based --> Basic One
        //[HttpGet("{Id}")]
        //public JsonResult GetCityById(int Id)
        //{
        //    return new JsonResult(LocalDataStorage.Current.Cities.FirstOrDefault(a => a.Id == Id));
        //}

        [HttpGet("action")]
        public ActionResult<IEnumerable<CityDto>> actionGetAllCities()
        {
            try
            {
                var cities = LocalDataStorage.Current.Cities;
                if (cities == null)
                {
                    _logger.LogInformation("actionGetAllCities :: Failure");
                    return NotFound();
                }
                _mailService.Send("Cities Sent","All the list of cities has been sent succesffuly from the controller.");
                return Ok(cities);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Error occured at actionGetAllCities {ex}");
                return StatusCode(500,"Error in Handling Request");
            }
        }

        [HttpGet("action/{Id}")]    //Paramaters name in curly braces shold match with input parameters of the action method
        public ActionResult<IEnumerable<CityDto>> actionGetCityById(int Id)
        {
            var cities = LocalDataStorage.Current.Cities.FirstOrDefault(a => a.Id == Id);
            if(cities == null)
            {
                return NotFound();
            }
            return Ok(cities);
        }
    }
}
