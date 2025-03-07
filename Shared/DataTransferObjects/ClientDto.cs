namespace Shared.DataTransferObjects
{
    public record ClientDto(Guid Id, string FirstName, string LastName, 
                                    string Email, string Phone, string Password);

}