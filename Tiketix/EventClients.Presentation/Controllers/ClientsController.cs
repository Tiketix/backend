using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;


namespace EventClients.Presentation.Controllers
{
[Route("api/clients")]
[ApiController]
[Authorize(Roles ="Manager, Administrator")]
    public class ClientsController : ControllerBase
    {
        private readonly IServiceManager _service;
        public ClientsController(IServiceManager service) => _service = service;
        
        
        [HttpGet]
        [Route("all-users")]
        public async Task<IActionResult> GetAllUsers() 
        {
            try
            {
                var clients = await _service.ClientService.GetAllUsers();
                return Ok(clients);
            }
            catch
            {
                return StatusCode(500, "Something Went Wrong");
            }
        }

        [HttpGet]
        [Route("user-by-email")]

        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _service.ClientService.GetUserByEmail(email);

            if (user is null)
            {
                return NotFound("User does not exist!!");
            }
            return Ok(user);
            
        }

        [HttpDelete("admin-delete-user")]
        public async Task<IActionResult> AdminDeleteUser(string email)
        {
            await _service.ClientService.AdminDeleteUser(email);

            return Ok("Account Deleted successfully");
        }





        

    }
}

