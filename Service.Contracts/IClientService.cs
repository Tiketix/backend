using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IClientService
{
    IEnumerable<ClientDto> GetAllClients(bool trackChanges);

    ClientDto GetClientByEmail(string email, bool trackChanges);

    ClientDto GetClientByFirstName(string firstName, bool trackChanges);

    ClientDto AddClient(AddClientDto client);

    void DeleteClient(string email, bool trackchanges);

    void UpdateClientName(string email, UpdateClientNameDto updateClientName, bool trackChanges);

    void UpdateClientContact(string email, UpdateClientContactDto updateClientContact, bool trackChanges);

    void UpdatePassword(string email, UpdatePasswordDto updatePassword, bool trackChanges);

}
