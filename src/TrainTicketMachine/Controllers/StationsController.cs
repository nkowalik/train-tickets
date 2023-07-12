using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TrainTicketMachine.Infrastructure.Repositories;

namespace TrainTicketMachine.Api.Controllers;

[Route("api/stations")]
[ApiController]
public class StationsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly Infrastructure.Services.IStationsService _stationsService;
    private readonly IStationsRepository _stationsRepository;

    public StationsController(IConfiguration configuration,
        Infrastructure.Services.IStationsService stationsService,
        IStationsRepository stationsRepository)
    {
        _configuration = configuration;
        _stationsService = stationsService;
        _stationsRepository = stationsRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<string>> GetMatchingStations(string input)
    {
        var uri = _configuration["CentralSystem"];
        var stations = _stationsRepository.GetAllStations(uri);
        var matchingStations = _stationsService.GetMatchingStations(input, stations);
        return matchingStations.Select(s => s.StationName).ToList();
    }
}
