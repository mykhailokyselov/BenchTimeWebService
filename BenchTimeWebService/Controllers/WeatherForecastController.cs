using BenchTimeWebService.Database;
using BenchTimeWebService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BenchTimeWebService.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IRepository<People> _repository;
        private readonly IRepository<User> _userRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRepository<People> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //[HttpDelete(Name = "DeleteWeatherForecast")]
        [HttpPost("/search/{id}",Name = "searchweather")]
        public IEnumerable<WeatherForecast> searchweather()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("/people/get")]
        public People getmike()
        {
            People people = new People();
            people.Name = "Mike";
            people.SomeOtherName = "Kyselov";
            people.Id = "12345";
            return people;
        }

        [HttpPost("/add")]
        public People AddPeople()
        {
            People people = new People();
            people.Name = "Mike";
            people.SomeOtherName = "Kyselov";
            people.Id = "12345";
            _repository.Add(people);

            User user = new User() { 
            Password = "123",
            Username = "SomeUserName"};
            _userRepository.Add(user);
            return people;
        }
    }
}
