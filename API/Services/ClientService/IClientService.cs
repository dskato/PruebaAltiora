public interface IClientService
{
    Task<object> AddClient(ClientDto clientDto);
    Task<object> GetAllClients();
    Task<object> GetClientById(int id);
    Task<object> UpdateClient(ClientDto clientDto);
    Task<object> DeleteClientById(int id);

}