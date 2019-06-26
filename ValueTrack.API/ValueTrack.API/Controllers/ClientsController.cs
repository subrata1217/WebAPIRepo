﻿using Contracts;
using Entities.Extensions;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _repoWrapper.Client.GetAllClientsAsync();
            _logger.LogInfo($"Returned all clients from the database.");

            return Ok(clients);
        }

        [HttpGet("{id}", Name = "ClientById")]
        public async Task<IActionResult> GetClientById(Guid id)
        {
            var client = await _repoWrapper.Client.GetClientByIdAsync(id);

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

        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetClientWithDetails(Guid id)
        {
            var client = await _repoWrapper.Client.GetClientWithDetailsAsync(id);

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

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody]Client client)
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

            await _repoWrapper.Client.CreateClientAsyc(client);

            return CreatedAtRoute("ClientById", new { client.Id }, client);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(Guid id, [FromBody]Client client)
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

            var dbClient = await _repoWrapper.Client.GetClientByIdAsync(id);
            if (dbClient.IsEmptyObject())
            {
                _logger.LogError($"Client with id: {id}, has not been found in the database.");
                return NotFound();
            }

            await _repoWrapper.Client.UpdateClientAsync(dbClient, client);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            var client = await _repoWrapper.Client.GetClientByIdAsync(id);
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

            await _repoWrapper.Client.DeleteClientAsync(client);

            return NoContent();
        }
    }
}