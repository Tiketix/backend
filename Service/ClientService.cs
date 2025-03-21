using AutoMapper;
using Contracts;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class ClientService : IClientService
{
    private readonly IRepositoryManager _repository; 
    private readonly IMapper _mapper;

    
    public ClientService(IRepositoryManager repository, IMapper mapper) 
    {
        _repository = repository;
        _mapper = mapper;
     
    }


     public IEnumerable<ClientDto> GetAllClients(bool trackChanges)
     {
        try
        {
            // var clients = 
            // _repository.Client.GetAllClients(trackChanges);
            // return clients; //this was before DTO introduction

            var clients = _repository.Client.GetAllClients(trackChanges);

            var clientsDto = _mapper.Map<IEnumerable<ClientDto>>(clients);

            return clientsDto;
        }
        catch (Exception)
        {
            throw new Exception($"There is an error somewhere");
        }
    }
    public ClientDto GetClientByEmail(string email, bool trackChanges)
    {
        try
        {
            var clients = _repository.Client.GetClientByEmail(email, trackChanges);
        
            var clientsDto = _mapper.Map<ClientDto>(clients);
                        
            return clientsDto;
        }
        catch (Exception)
        {
            throw new Exception("There is an error somewhere");
        }
    }

    public ClientDto GetClientByFirstName(string firstName, bool trackChanges)
    {
        var client = _repository.Client.GetClientByFirstName(firstName, trackChanges);
        
        var clientDto = _mapper.Map<ClientDto>(client);
                        
        return clientDto;
    }

    public ClientDto ClientLogin(string email, string password, bool trackChanges)
    {
        var client = _repository.Client.ClientLogin(email, password, trackChanges);
        
        var clientDto = _mapper.Map<ClientDto>(client);

        return clientDto;
    }


    public ClientDto AddClient(AddClientDto client)
    {
        var newClient = _mapper.Map<Client>(client);

        _repository.Client.AddClient(newClient);
        _repository.Save();

        var returnClient = _mapper.Map<ClientDto>(newClient);
        return returnClient;
    }


    public void DeleteClient(string email, bool trackchanges)
    {
        var client =  _repository.Client.GetClientByEmail(email, trackchanges);

        if (client is null)
            throw new Exception("email does not exist in database");
        


        _repository.Client.DeleteClient(client);
        _repository.Save();
    }

    public void UpdateClientName(string email, UpdateClientNameDto updateClientName, bool trackchanges)
    {
        var client = _repository.Client.GetClientByEmail(email, trackchanges);
        if (client is null)
            throw new Exception("email does not exist in database");

        _mapper.Map(updateClientName, client);
        _repository.Save();
    }

    public void UpdateClientContact(string email, UpdateClientContactDto updateClientContact, bool trackchanges)
    {
        var client = _repository.Client.GetClientByEmail(email, trackchanges);
        if (client is null)
            throw new Exception("email does not exist in database");

        _mapper.Map(updateClientContact, client);
        _repository.Save();
    }

    public void UpdatePassword(string email, UpdatePasswordDto updatePassword, bool trackchanges)
    {
        var client = _repository.Client.GetClientByEmail(email, trackchanges);
        if (client is null)
            throw new Exception("email does not exist in database");

        _mapper.Map(updatePassword, client);
        _repository.Save();
    }

    

}

