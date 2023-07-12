using TrainTicketMachine.Infrastructure.Services;

namespace TrainTicketMachine.Infrastructure.Tests.Services
{
    public class StationsServiceTests
    {
        private readonly StationsService _stationsService;

        public StationsServiceTests()
        {
            _stationsService = new StationsService();
        }

        [Theory]
        [InlineData("Ab", 2, new[] { "Abcdef", "Abbey" })]
        [InlineData("De", 1, new[] { "Defghijk" })]
        [InlineData("XYZ", 0, new string[0])]
        public void GetMatchingStations_ShouldReturnMatchingStations(
            string input, int expectedCount, string[] expectedStationNames)
        {
            // Arrange
            var stations = new List<Domain.Models.Station>
            {
                new Domain.Models.Station("ABC", "Abcdef"),
                new Domain.Models.Station("ABE", "Abbey"),
                new Domain.Models.Station("DEF", "Defghijk")
            };

            // Act
            var result = _stationsService.GetMatchingStations(input, stations);

            // Assert
            Assert.Equal(expectedCount, result.Count());
            Assert.Equal(expectedStationNames, result.Select(station => station.StationName));
        }

        [Theory]
        [InlineData("Ab", new[] { 'C', 'B' })]
        [InlineData("De", new[] { 'F' })]
        [InlineData("XYZ", new char[0])]
        public void GetMatchingCharacters_ShouldReturnMatchingCharacters(
            string input, char[] expectedCharacters)
        {
            // Arrange
            var matchingStations = new List<Domain.Models.Station>
            {
                new Domain.Models.Station("ABC", "Abcdef"),
                new Domain.Models.Station("ABE", "Abbey"),
                new Domain.Models.Station("DEF", "Defghijk")
            };

            // Act
            var result = _stationsService.GetMatchingCharacters(input, matchingStations);

            // Assert
            Assert.Equal(expectedCharacters, result);
        }
    }
}
