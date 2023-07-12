namespace TrainTicketMachine.Infrastructure.Services;

public interface IStationsService
{
    IEnumerable<char> GetMatchingCharacters(
        string input, 
        IEnumerable<Domain.Models.Station> matchingStations);

    IEnumerable<Domain.Models.Station> GetMatchingStations(
        string input,
        IEnumerable<Domain.Models.Station> stations);
}
