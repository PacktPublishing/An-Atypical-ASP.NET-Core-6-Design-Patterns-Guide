using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenAPI.Models;
using OpenAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace OpenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ClientRepository _clientService = new ClientRepository();

        // GET api/clients
        [HttpGet]
        public ActionResult<IEnumerable<ClientSummaryDto>> Get()
        {
            var clients = _clientService.ReadAll();
            var dto = clients.Select(client => new ClientSummaryDto
            {
                Id = client.Id,
                Name = client.Name,
                TotalNumberOfContracts = client.Contracts.Count,
                NumberOfOpenContracts = client.Contracts.Count(x => x.Work.State != WorkState.Completed)
            }).ToArray();
            return dto;
        }

        // GET api/clients/1
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDetailsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var client = _clientService.ReadOne(id);
            if (client == default(Client))
            {
                return NotFound();
            }
            var dto = new ClientDetailsDto
            {
                Id = client.Id,
                Name = client.Name,
                Contracts = client.Contracts.Select(contract => new ContractDetailsDto
                {
                    Id = contract.Id,
                    Name = contract.Name,
                    Description = contract.Description,

                    // Flattening PrimaryContact
                    PrimaryContactEmail = contract.PrimaryContact.Email,
                    PrimaryContactFirstname = contract.PrimaryContact.Firstname,
                    PrimaryContactLastname = contract.PrimaryContact.Lastname,

                    // Flattening Work
                    WorkDone = contract.Work.Done,
                    WorkState = contract.Work.State,
                    WorkTotal = contract.Work.Total
                })
            };
            return Ok(dto);
        }
    }
}
