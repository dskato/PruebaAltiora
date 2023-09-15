
using Microsoft.EntityFrameworkCore;

public class ClientService : IClientService
{
    private readonly AppDbContext _dbContext;
    AutoMapper.IMapper mapper;
    public ClientService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        MapperConfig mapperConfig = new MapperConfig();
        mapper = mapperConfig.ConfigureClientMapping();
    }

    public async Task<object> AddClient(ClientDto clientDto)
    {
        try
        {
            //Verify if user already exists by the name and lastname
            var clientEntity = _dbContext.ClientEntity.Where(x => x.Firstname == clientDto.Firstname && x.Lastname == clientDto.Lastname).FirstOrDefault();
            if (clientEntity != null)
            {
                return "Client already registered!";
            }
            var client = mapper.Map<ClientEntity>(clientDto);
            var result = await _dbContext.ClientEntity.AddAsync(client);
            await _dbContext.SaveChangesAsync();

            return "Client added succesfully!";
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<object> DeleteClientById(int id)
    {
        try
        {

            //Verify if user exits
            var client = await _dbContext.ClientEntity.Where(x => x.ClientId == id).FirstOrDefaultAsync();
            if (client == null)
            {
                return "Client not found!";
            }
            _dbContext.ClientEntity.Remove(client);
            await _dbContext.SaveChangesAsync();

            return "Client deleted succesfully!";

        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<object> GetAllClients()
    {
        try
        {
            return await _dbContext.ClientEntity.ToListAsync();
        }
        catch (Exception e)
        {
            return e;
        }

    }

    public async Task<object> GetClientById(int id)
    {

        try
        {
            var client = await _dbContext.ClientEntity.Where(x => x.ClientId == id).FirstOrDefaultAsync();
            if (client == null)
            {
                return "Client not found!";
            }
            return client;
        }
        catch (Exception e)
        {
            return e;
        }

    }

    public async Task<object> UpdateClient(ClientDto clientDto)
    {
        try
        {
            var client = await _dbContext.ClientEntity.Where(x => x.ClientId == clientDto.ClientId).FirstOrDefaultAsync();
            if (client == null)
            {
                return "Client not found!";
            }
            client = mapper.Map<ClientEntity>(clientDto);
            await _dbContext.SaveChangesAsync();
            
            return "Client updated!";
        }
        catch (Exception e)
        {
            return e;
        }
    }
}