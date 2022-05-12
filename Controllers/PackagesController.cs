using DEVTRACKR.API.Entities;
using DEVTRACKR.API.Models;
using DEVTRACKR.API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DEVTRACKR.API.Controllers;

[Route("api/packages")]
public class PackagesController : ControllerBase
{
    private readonly DevTrackRContext _context;
    public PackagesController(DevTrackRContext context)
    {
        _context = context;
    }
    //GET api/packages
    [HttpGet]
    public IActionResult GetAll()
    {
        var packages = _context.Packages;
        
        return Ok(packages);
    }

    //GET api/packages/1234-5678-1234
    [HttpGet("{code}")]
    public IActionResult GetByCode(string code)
    {
        var package = _context.Packages.SingleOrDefault(x => x.Code == code);

        if (package == null)
            return NotFound();
        
        return Ok(package);
    }

    // POST api/packages
    [HttpPost]
    public IActionResult Post(AddPackageInputModel model)
    {
        if (model.Title.Length < 10)
            return BadRequest("Title Length must be at least 10 character");
        
        var package = new Package(model.Title, model.Weight);
        
        _context.Packages.Add((package));
        
        return CreatedAtAction("GetByCode", new { code = package.Code},
            package);
    }
    
    // POST api/packages/1234-5678-1234/updates
    [HttpPost("{code}/updates")]
    public IActionResult PostUpdate(string code, AddPackageUpdateInputModel model)
    {
        var package = _context.Packages.SingleOrDefault(x => x.Code == code);

        if (package == null)
            return NotFound();
        
        package.AddUpdate(model.Status, model.Delivered);
        
        return NoContent();
    }
}