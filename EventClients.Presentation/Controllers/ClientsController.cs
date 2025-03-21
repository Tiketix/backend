using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;


namespace EventClients.Presentation.Controllers
{
[Route("api/clients")]
[ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IServiceManager _service;
        public ClientsController(IServiceManager service) => _service = service;
        
        
        [HttpGet]
        [Authorize(Roles ="Manager, Administrator")]
        public IActionResult GetClients()
        {
            try
            {
                var clients = 
                _service.ClientService.GetAllClients(trackChanges: false);
                return Ok(clients);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("by-email")]
        [Authorize(Roles ="Manager, Administrator")]

        public IActionResult GetClientByEmail(string email)
        {
            try
            {
                var client = _service.ClientService.GetClientByEmail(email, trackChanges: false);

                if (client is null)
                {
                    return NotFound("Client does not exist!!");
                }
                return Ok(client);
            }
            catch
            {
                return StatusCode(500, "Something is wrong somewhere.");
            }
        }

        [HttpGet]
        [Route("by-firstname")]
        [Authorize(Roles ="Manager, Administrator")]

        public IActionResult GetClientByFirstName(string firstName)
        {
            try
            {
                var client = _service.ClientService.GetClientByFirstName(firstName, trackChanges: false);
                if (client is null)
                    {
                        return NotFound("Client does not exist!!");
                    }
                    return Ok(client);
            }
            catch
            {
                return StatusCode(500, "Something is wrong somewhere.");
            }
        }

        [HttpPost]
        [Route("register")]
        public IActionResult AddClient([FromBody] AddClientDto client)
        {
            if (client is null)
                    return BadRequest("AddClientDto object is null");

            var newClient = _service.ClientService.AddClient(client);

            return Ok(newClient);
        }

        [HttpGet]
        [Route("login")]
        // [Authorize]

        public IActionResult ClientLogin(string email, string password)
        {
            try
            {
                var client = _service.ClientService.ClientLogin(email, password, trackChanges: false);
                if (client is null)
                    {
                        return NotFound("Incorrect Credentials");
                    }
                var response = new 
                {
                                   
                    User = new
                    {
                        client.Id,
                        client.FirstName,
                        client.LastName,
                        client.Phone,
                        client.Email
                    }
                };

                return Ok(response); //return insensitive deets.
            }
            catch
            {
                return StatusCode(500, "Something is wrong somewhere.");
            }
        }


        [HttpPut]
        [Route("update-name")]

        public IActionResult UpdateClientName(string email, [FromBody] UpdateClientNameDto client)
        {
            if (client is null)
                    return BadRequest("UpdateClientNameDto object is null");

            _service.ClientService.UpdateClientName(email, client, trackChanges: true);

            return Ok(client);

        }

        [HttpPut]
        [Route("update-contact")]

        public IActionResult UpdateClientContact(string email, [FromBody] UpdateClientContactDto client)
        {
            if (client is null)
                    return BadRequest("UpdateClientContactDto object is null");

            _service.ClientService.UpdateClientContact(email, client, trackChanges: true);

            return Ok(client);

        }

        [HttpPut]
        [Route("change-password")]

        public IActionResult UpdatePassword(string email, [FromBody] UpdatePasswordDto client)
        {
            if (client is null)
                    return BadRequest("UpdatePasswordDto object is null");

            _service.ClientService.UpdatePassword(email, client, trackChanges: true);

            return Ok("Password Updated");

        }

        [HttpDelete]
        [Route("delete-user")]
        [Authorize(Roles ="Manager, Administrator")]

        public IActionResult DeleteClient(string email)
        {
            _service.ClientService.DeleteClient(email, trackchanges: false);

            return Ok("Deleted successfully");
        }


    }
}

