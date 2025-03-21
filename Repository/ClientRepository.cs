using Contracts;
using Entities.Models;

namespace Repository
{
    internal sealed class ClientRepository : RepositoryBase<Client>, IClientRepository
{
    public ClientRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public IEnumerable<Client> GetAllClients(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(c => c.FirstName)
            .ToList();


        #pragma warning disable CS8603 // Possible null reference return.
    public Client GetClientByEmail(string email, bool trackChanges) =>
            FindByCondition(c => c.Email == email, trackChanges)
            .SingleOrDefault();


        #pragma warning disable CS8603 // Possible null reference return.
    public Client GetClientByFirstName(string firstName, bool trackChanges) =>
            FindByCondition(c => c.FirstName.Equals(firstName), trackChanges)
            .SingleOrDefault();

    public Client ClientLogin(string email, string password, bool trackChanges) =>
            FindByCondition(c => c.Email.Equals(email) && c.Password.Equals(password), trackChanges)
            .SingleOrDefault();


    public void AddClient(Client client) => Create(client);

    public void DeleteClient(Client client) =>  Delete(client); 

    }

}

