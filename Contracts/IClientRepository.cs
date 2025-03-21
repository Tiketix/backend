using Entities.Models;

namespace Contracts
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients(bool trackChanges);
        
        Client GetClientByEmail(string email, bool trackChanges);

        Client GetClientByFirstName(string firstName, bool trackChanges);

        Client ClientLogin(string email, string password, bool trackChanges);

        void AddClient(Client client);

        void DeleteClient(Client client);
    }
}


