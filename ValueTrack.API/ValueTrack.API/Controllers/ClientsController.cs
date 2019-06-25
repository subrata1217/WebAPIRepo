using Contracts;
using Entities.Extensions;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ValueTrack.API.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repoWrapper;

        public ClientsController(IRepositoryWrapper repositoryWrapper, ILoggerManager logger)
        {
            _repoWrapper = repositoryWrapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllClients()
        {
            try
            {
                var clients = _repoWrapper.Client.GetAllClients();
                _logger.LogInfo($"Returned all clients from the database.");

                return Ok(clients);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllClients action: {ex.Message}");
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet("{id}", Name = "ClientById")]
        public IActionResult GetClientById(Guid id)
        {
            try
            {
                var client = _repoWrapper.Client.GetClientById(id);

                if (client.IsEmptyObject())
                {
                    _logger.LogError($"Client with id: {id}, has not been found in the database.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned client with id: {id}");
                    return Ok(client);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetClientById action: {ex.Message}");
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet("{id}/details")]
        public IActionResult GetClientWithDetails(Guid id)
        {
            try
            {
                var client = _repoWrapper.Client.GetClientWithDetails(id);

                if (client.IsEmptyObject())
                {
                    _logger.LogError($"Client with id: {id}, has not been found in the database.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned client with details for id: {id}");
                    return Ok(client);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetClientWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpPost]
        public IActionResult CreateClient([FromBody]Client client)
        {
            try
            {
                if (client.IsObjectNull())
                {
                    _logger.LogError($"Client object sent from client is null");
                    return BadRequest("Client object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError($"Invalid Client object sent from client");
                    return BadRequest("Invalid Client object");
                }

                _repoWrapper.Client.CreateClient(client);
                _repoWrapper.Save();

                return CreatedAtRoute("ClientById", new { client.Id }, client);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateClient action: {ex.Message}");
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClient(Guid id, [FromBody]Client client)
        {
            try
            {
                if (client.IsObjectNull())
                {
                    _logger.LogError($"Client object sent from client is null");
                    return BadRequest("Client object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError($"Invalid Client object sent from client");
                    return BadRequest("Invalid Client object");
                }

                var dbClient = _repoWrapper.Client.GetClientById(id);
                if(dbClient.IsEmptyObject())
                {
                    _logger.LogError($"Client with id: {id}, has not been found in the database.");
                    return NotFound();
                }

                _repoWrapper.Client.UpdateClient(dbClient, client);
                _repoWrapper.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateClient action: {ex.Message}");
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient(Guid id)
        {
            try
            {
                var client = _repoWrapper.Client.GetClientById(id);
                if (client.IsEmptyObject())
                {
                    _logger.LogError($"Client with id: {id}, has not been found in the database.");
                    return NotFound();
                }

                if (_repoWrapper.ClientRegion.ClientRegionByClient(id).Any())
                {
                    _logger.LogError($"Cannot delete client with id: {id}. It has related regions. Delete those regions first");
                    return BadRequest("Cannot delete client. It has related regions. Delete those regions first");
                }

                _repoWrapper.Client.DeleteClient(client);
                _repoWrapper.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteClient action: {ex.Message}");
                return StatusCode(500, "Internal Server Error.");
            }
        }
    }
}