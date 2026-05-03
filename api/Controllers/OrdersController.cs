using api.DTOs.Orders;
using core.Entities.Orders;
using Microsoft.AspNetCore.Mvc;
using core.Specifications;
using core.Interfaces;
using api.Helpers;
using AutoMapper;
using core.Entities;

namespace api.Controllers;

public class OrdersController(IUnitOfWork uow, IMapper mapper) : MDBBaseController
{
    [HttpGet()]
    public async Task<ActionResult> ListAllOrders([FromQuery] OrderSpecificationParams args)
    {
        var orders = await CreateResult(uow.Repository<Order>(), new OrderSpecification(args));
        var mappedOrders = mapper.Map<IReadOnlyList<GetAllOrdersDto>>(orders.Result);

        return Resp(200, true, "Orders retrieved", orders, mappedOrders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindOrderById(string id)
    {
        var order = await uow.Repository<Order>().FindAsync(new OrderSpecification(orderId: id));

        if (order is null) return Resp(404, false, "Order not found");

        var mappedOrder = mapper.Map<GetOrderDto>(order);

        return Resp(200, true, "Order found", new DataResult<Order>(1, [order]), [mappedOrder]);
    }

    [HttpGet("orderNumber/{orderNumber}")]
    public async Task<ActionResult> FindOrderByOrderNumber(int orderNumber)
    {
        var order = await uow.Repository<Order>().FindAsync(new OrderSpecification(orderNumber: orderNumber));

        if (order is null) return Resp(404, false, "Order not found");

        var mappedOrder = mapper.Map<GetOrderDto>(order);

        return Resp(200, true, "Order found", new DataResult<Order>(1, [order]), [mappedOrder]);
    }

    [HttpPost()]
    public async Task<ActionResult> AddOrder(PostOrderDto model)
    {
        if (model is null) return Resp(400, false, "Invalid input");

        var customer = await uow.Repository<Customer>().FindAsync(new CustomerSpecification(storeName: model.StoreName));

        if (customer is null) return Resp(404, false, "Customer not found");

        var order = mapper.Map<Order>(model);
        order.CustomerId = customer.Id;

        var latestOrder = await uow.Repository<Order>().FindAsync(new OrderSpecification(latestOnly: true));
        order.OrderNumber = (latestOrder is not null) ? latestOrder.OrderNumber + 1 : 10001;

        uow.Repository<Order>().Add(order);
        await uow.Complete();

        var mappedOrder = mapper.Map<GetOrderDto>(order);

        return CreatedAtResp(
            nameof(FindOrderById),
            new { id = order.Id },
            "Created",
            new DataResult<Order>(1, [order]),
            [mappedOrder]
        );
    }
}
