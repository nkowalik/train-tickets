using TrainTicketMachine.Infrastructure.Repositories;

namespace TrainTicketMachine.Api.Services
{
    public class TrainTicketService : ITrainTicketService
    {
        private readonly Infrastructure.Services.IStationsService _stationsService;
        private readonly IStationsRepository _stationsRepository;

        public TrainTicketService(Infrastructure.Services.IStationsService stationsService, 
            IStationsRepository stationsRepository)
        {
            _stationsService = stationsService;
            _stationsRepository = stationsRepository;
        }

        public void Run(string? uri)
        {
            if (uri == null)
            {
                Console.WriteLine("No response system found");
                return;
            }

            var stations = _stationsRepository.GetAllStations(uri);

            ShowMatchingStations(stations);
        }

        public char GetUserInput()
        {
            ConsoleKeyInfo keyInfo;
            keyInfo = Console.ReadKey();
            return keyInfo.KeyChar;
        }

        public void ShowMatchingStations(
            IEnumerable<Domain.Models.Station> stations)
        {
            Console.WriteLine("Insert characters:");
            string input = string.Empty;

            while (true)
            {
                Console.Write(input);
                input += GetUserInput();

                var (matchingStations, matchingCharacters) = GetMatchingData(input, stations);

                if (!matchingStations.Any() && !matchingCharacters.Any())
                {
                    Console.WriteLine("No matching characters");
                    Console.WriteLine("No matching stations");
                    break;
                }

                PrintMatchingCharacters(matchingCharacters);
                PrintMatchingStations(matchingStations);
            }
        }

        private (IEnumerable<Domain.Models.Station>, IEnumerable<char>) GetMatchingData(
            string input, 
            IEnumerable<Domain.Models.Station> stations)
        {
            var matchingStations = _stationsService.GetMatchingStations(input, stations);
            var matchingCharacters = _stationsService.GetMatchingCharacters(input, matchingStations);

            return (matchingStations, matchingCharacters);
        }

        private void PrintMatchingCharacters(
            IEnumerable<char> matchingCharacters)
        {
            Console.WriteLine();
            foreach (var matchingCharacter in matchingCharacters)
            {
                Console.WriteLine(matchingCharacter);
            }
        }

        private void PrintMatchingStations(
            IEnumerable<Domain.Models.Station> matchingStations)
        {
            foreach (var matchingStation in matchingStations)
            {
                Console.WriteLine(matchingStation);
            }
        }
    }
}
