using Microsoft.AspNetCore.Mvc;
using WebEntityApi.Dtos;
using WebEntityApi.Models;
using WebEntityApi.Repository;

namespace WebEntityApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class TenantController : ControllerBase
{
    public Dal<Tenant> Tenants { get; set; }
    public Dal<User> Users { get; set; }

    public TenantController(Dal<Tenant> tenants, Dal<User> users)
    {
        Tenants = tenants;
        Users = users;
    }

    [HttpGet]
    public IEnumerable<TenantDto> Get()
    {
        var tenants = Tenants.List();
        return tenants.Select(t => t.ToDto());
    }

    [HttpGet("{id}")]
    public IActionResult GetTenant(int id)
    {
        var tenant = Tenants.Find(t => t.Id == id);
        if (tenant == null) return NotFound();

        return Ok(tenant.ToDto());
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateTenantDto createTenantDto)
    {
        var user = Users.Find(u => u.Id == createTenantDto.OwnerId);
        if (user == null) return NotFound("User not found");

        var tenant = createTenantDto.ToEntity(user);
        Tenants.Add(tenant);
        return CreatedAtAction(nameof(GetTenant), new { id = tenant.Id }, tenant.ToDto());
    }

    [HttpPut("{id}")]
    public IActionResult Put([FromBody] UpdateTenantDto updateTenantDto, int id)
    {
        var tenant = Tenants.Find(t => t.Id == id);
        if (tenant == null) return NotFound();

        tenant.UpdateFromDto(updateTenantDto);
        Tenants.Update(tenant);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var tenant = Tenants.Find(t => t.Id == id);
        if (tenant == null) return NotFound();

        Tenants.Remove(tenant);
        return NoContent();
    }
}
