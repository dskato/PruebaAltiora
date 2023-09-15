using AutoMapper;

public class MapperConfig
{

    public IMapper ConfigureClientMapping()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ClientDto, ClientEntity>();
            cfg.CreateMap<ClientEntity, ClientDto>();

        });

        var mapper = configuration.CreateMapper();
        return mapper;
    }

    public IMapper ConfigureProductMapping()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ProductDto, ProductEntity>();
            cfg.CreateMap<ProductEntity, ProductDto>();

        });

        var mapper = configuration.CreateMapper();
        return mapper;
    }

}