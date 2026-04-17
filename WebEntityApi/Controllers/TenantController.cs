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
    async public Task<IEnumerable<TenantDto>> Get()
    {
        var tenants = await Tenants.ListAsync();
        return tenants.Select(t => t.ToDto());
    }

    [HttpGet("{id}")]
    async public Task<IActionResult> GetTenant(int id)
    {
        var tenant = await Tenants.FindAsync(t => t.Id == id);
        if (tenant == null) return NotFound();

        return Ok(tenant.ToDto());
    }

    [HttpPost]
    async public Task<IActionResult> Post([FromBody] CreateTenantDto createTenantDto)
    {
        var user = await Users.FindAsync(u => u.Id == createTenantDto.OwnerId);
        if (user == null) return NotFound("User not found");

        var tenant = createTenantDto.ToEntity(user);
        await Tenants.AddAsync(tenant);
        return CreatedAtAction(nameof(GetTenant), new { id = tenant.Id }, tenant.ToDto());
    }

    [HttpPut("{id}")]
    async public Task<IActionResult> Put([FromBody] UpdateTenantDto updateTenantDto, int id)
    {
        var tenant = await Tenants.FindAsync(t => t.Id == id);
        if (tenant == null) return NotFound();

        tenant.UpdateFromDto(updateTenantDto);
        await Tenants.Update(tenant);
        return NoContent();
    }

    [HttpDelete("{id}")]
    async public Task<IActionResult> Delete(int id)
    {
        var tenant = await Tenants.FindAsync(t => t.Id == id);
        if (tenant == null) return NotFound();

        await Tenants.Remove(tenant);
        return NoContent();
    }
}
