namespace TrainTicketMachine.Infrastructure.Repositories
{
    public interface IStationsRepository
    {
        IEnumerable<Domain.Models.Station> GetAllStations(string uri);
    }
}
