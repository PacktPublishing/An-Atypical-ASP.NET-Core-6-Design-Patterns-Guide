using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly CustomerRepository _customerService = new CustomerRepository();

    // GET api/customers
    [HttpGet]
    public ActionResult<IEnumerable<CustomerSummaryDto>> Get()
    {
        var customers = _customerService.ReadAll();
        var dto = customers.Select(customer => new CustomerSummaryDto(
            Id: customer.Id,
            Name: customer.Name,
            TotalNumberOfContracts: customer.Contracts.Count,
            NumberOfOpenContracts: customer.Contracts.Count(x => x.Work.State != WorkState.Completed)
        )).ToArray();
        return dto;
    }

    // GET api/customers/1
    [HttpGet("{id}")]
    public ActionResult<CustomerDetailsDto> Get(int id)
    {
        var customer = _customerService.ReadOne(id);
        if (customer == default(Customer))
        {
            return NotFound();
        }
        var dto = new CustomerDetailsDto(
            Id: customer.Id,
            Name: customer.Name,
            Contracts: customer.Contracts.Select(contract => new ContractDetailsDto(
                Id: contract.Id,
                Name: contract.Name,
                Description: contract.Description,

                // Flattening PrimaryContact
                PrimaryContactEmail: contract.PrimaryContact.Email,
                PrimaryContactFirstname: contract.PrimaryContact.Firstname,
                PrimaryContactLastname: contract.PrimaryContact.Lastname,

                // Flattening Work
                WorkDone: contract.Work.Done,
                WorkState: contract.Work.State,
                WorkTotal: contract.Work.Total
            ))
        );
        return Ok(dto);
    }
}
