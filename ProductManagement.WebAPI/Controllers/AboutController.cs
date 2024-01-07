using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Entities.Models;

namespace ProductManagement.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AboutController : ControllerBase
{
    ProductManagementContext context 
        = new ProductManagementContext();
    
    [HttpGet] 
    [ProducesResponseType(typeof(About), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get()
    {
        var about = context.Abouts
            .FirstOrDefault(x => x.Status == true);
        if (about == null)
            return NotFound();
        return Ok(about);
    }

    [HttpPut]
    [ProducesResponseType(typeof(About), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Put(About model)
    {
        var about = context.Abouts
            .FirstOrDefault(x => x.Status == true);
        if (about == null)
            return NotFound();

        about.Title = model.Title;
        about.Description = model.Description;
        context.SaveChanges();
        return Ok(about);
    }
}
