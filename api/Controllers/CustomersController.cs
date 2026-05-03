using api.DTOs.Customers;
using core.Entities;
using Microsoft.AspNetCore.Mvc;
using core.Specifications;
using core.Interfaces;
using api.Helpers;
using AutoMapper;

namespace api.Controllers;

public class CustomersController(IUnitOfWork uow, IMapper mapper) : MDBBaseController
{
    [HttpGet()]
    public async Task<ActionResult> ListAllCustomers([FromQuery] CustomerSpecificationParams args)
    {
        var customers = await CreateResult(uow.Repository<Customer>(), new CustomerSpecification(args));
        var mappedCustomers = mapper.Map<IReadOnlyList<GetAllCustomersDto>>(customers.Result);

        return Resp(200, true, "Customers retrieved", customers, mappedCustomers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindCustomerById(string id)
    {
        var customer = await uow.Repository<Customer>().FindAsync(new CustomerSpecification(customerId: id));

        if (customer is null) return Resp(404, false, "Customer not found");

        var mappedCustomer = mapper.Map<GetCustomerDto>(customer);

        return Resp(200, true, "Customer found", new DataResult<Customer>(1, [customer]), [mappedCustomer]);
    }

    [HttpGet("{storeName}/order-history")]
    public async Task<ActionResult> FindCustomerWithOrders(string storeName)
    {

        // Lägg till logik för att hämta ut beställningshistorik


        var customer = await uow.Repository<Customer>().FindAsync(new CustomerSpecification(storeName: storeName));

        if (customer is null) return Resp(404, false, "Customer not found");

        var mappedCustomer = mapper.Map<GetCustomerDto>(customer);

        return Resp(200, true, "Customer found", new DataResult<Customer>(1, [customer]), [mappedCustomer]);
    }

    [HttpPost()]
    public async Task<ActionResult> AddCustomer(PostCustomerDto model)
    {
        if (model is null) return Resp(400, false, "Invalid input");

        if (await uow.Repository<Customer>().AnyAsync(new CustomerSpecification(storeName: model.StoreName)))
        {
            return Resp(409, false, "Customer already exists");
        }

        var customer = mapper.Map<Customer>(model);

        uow.Repository<Customer>().Add(customer);
        await uow.Complete();

        var mappedCustomer = mapper.Map<GetCustomerDto>(customer);

        return CreatedAtResp(
            nameof(FindCustomerById),
            new { id = customer.Id },
            "Created",
            new DataResult<Customer>(1, [customer]),
            [mappedCustomer]
        );
    }
}
