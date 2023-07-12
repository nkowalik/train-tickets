namespace TrainTicketMachine.Infrastructure.Services;

public class StationsService : IStationsService
{
    public IEnumerable<Domain.Models.Station> GetMatchingStations(
        string input, 
        IEnumerable<Domain.Models.Station> stations)
    {
        return stations
            .Where(station => station.StationName.StartsWith(input, StringComparison.OrdinalIgnoreCase))
            .Distinct();
    }

    public IEnumerable<char> GetMatchingCharacters(
        string input, 
        IEnumerable<Domain.Models.Station> matchingStations)
    {
        return matchingStations
            .Select(station => GetMatchingCharacter(input, station))
            .Where(matchingChar => matchingChar.HasValue)
            .Select(matchingChar => matchingChar!.Value)
            .Distinct();
    }

    private char? GetMatchingCharacter(
        string input, 
        Domain.Models.Station station)
    {
        var stationName = station.StationName;
        int index = stationName.IndexOf(input, StringComparison.OrdinalIgnoreCase);
        int indexOfElement = index + input.Length;
        if (index >= 0 && indexOfElement < stationName.Length)
        {
            return char.ToUpper(stationName[indexOfElement]);
        }
        return null;
    }
}
