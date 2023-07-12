using Newtonsoft.Json;
using Polly;

namespace TrainTicketMachine.Infrastructure.Repositories
{
    public class StationsRepository : IStationsRepository
    {
        public StationsRepository() { }

        public IEnumerable<Domain.Models.Station> GetAllStations(string uri)
        {
            const int maxRetries = 3;
            const int retryDelayMs = 1000;

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetry(maxRetries, attempt => TimeSpan.FromMilliseconds(retryDelayMs));

            using var client = new HttpClient();
            try
            {
                var response = policy.Execute(() => GetResponseContent(client, uri));

                if (response == null)
                {
                    return Enumerable.Empty<Domain.Models.Station>();
                }

                return DeserializeStations(response.Result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"An error occurred while downloading the data: {e.Message}");
                throw;
            }
        }

        private static async Task<string> GetResponseContent(
            HttpClient client, 
            string uri)
        {
            var httpResponse = await client.GetAsync(uri);
            return await httpResponse.Content.ReadAsStringAsync();
        }

        private static IEnumerable<Domain.Models.Station> DeserializeStations(
            string response)
        {
            return JsonConvert.DeserializeObject<List<Domain.Models.Station>>(response) 
                ?? Enumerable.Empty<Domain.Models.Station>();
        }
    }
}
