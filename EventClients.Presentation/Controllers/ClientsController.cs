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
        [Route("allUsers")]
        public async Task<IActionResult> GetAllUsers() 
        {
            var response = await _service.ClientService.GetAllUsers();
            
            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }

        [HttpGet]
        [Route("getUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var response = await _service.ClientService.GetUserByEmail(email);

            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }

        [HttpGet]
        [Route("getUserById")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var response = await _service.ClientService.GetUserById(id);

            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }

        [HttpDelete("deleteUser")]
        public async Task<IActionResult> AdminDeleteUser(string email)
        {
            var response = await _service.ClientService.AdminDeleteUser(email);

            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }





        

    }
}

