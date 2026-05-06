using api.Helpers;
using core.Entities;
using core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MDBBaseController : ControllerBase
{
    [NonAction]
    protected async Task<DataResult<T>> CreateResult<T>(
        IGenericRepository<T> repo,
        ISpecification<T> args
    ) where T : BaseEntity
    {
        var count = await repo.CountAsync(args);
        var result = await repo.ListAsync(args);
        var data = new DataResult<T>(count, result);

        return data;
    }

    [NonAction]
    protected ActionResult Resp(
        int statusCode,
        bool success,
        string message
    )
    {
        return StatusCode(statusCode, new
        {
            Success = success,
            StatusCode = statusCode,
            Message = message,
            Data = new { }
        });
    }

    [NonAction]
    protected ActionResult Resp<TEntity, TDto>(
        int statusCode,
        bool success,
        string message,
        DataResult<TEntity>? data = null,
        IEnumerable<TDto>? mappedData = null
    ) where TEntity : BaseEntity
    {
        return StatusCode(statusCode, new
        {
            Success = success,
            StatusCode = statusCode,
            Message = message,
            Data = new { Count = data?.Count ?? 0, Result = mappedData }
        });
    }

    [NonAction]
    protected ActionResult CreatedAtResp<TEntity, TDto>(
        string action,
        object route,
        string message,
        DataResult<TEntity>? data = null,
        IEnumerable<TDto>? mappedData = null
    ) where TEntity : BaseEntity
    {
        return CreatedAtAction(action, route, new
        {
            Success = true,
            StatusCode = 201,
            Message = message,
            Data = new { Count = data?.Count ?? 0, Result = mappedData }
        });
    }
}
