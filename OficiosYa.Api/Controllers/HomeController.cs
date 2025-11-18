using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class ProfesionalController : ControllerBase
{
    [HttpGet]
    public  IActionResult Get()
    {
        return Ok("Funciona!");
    }
}