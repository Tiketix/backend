namespace Shared.DataTransferObjects
{
    public record AddClientDto(string FirstName, string LastName, 
                                string Email, string Phone, string Password);

}