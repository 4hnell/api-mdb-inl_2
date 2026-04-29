using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class MDBBaseController : ControllerBase
{
    [NonAction]
    protected ObjectResult Resp(int statusCode, bool success, string message, int? count = null, object? data = null)
    {
        return StatusCode(statusCode, new
        {
            Success = success,
            StatusCode = statusCode,
            Message = message,
            Count = count,
            Data = data
        });
    }

    [NonAction]
    protected ObjectResult CreatedAtResp(string action, object route, string message, object data)
    {
        return CreatedAtAction(action, route, new
        {
            Success = true,
            StatusCode = 201,
            Message = message,
            Data = data
        });
    }
}
