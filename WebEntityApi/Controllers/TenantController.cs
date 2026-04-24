using Microsoft.AspNetCore.Mvc;
using WebEntityApi.Dtos;
using WebEntityApi.Service;

namespace WebEntityApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class TenantController : ControllerBase
{
    private TenantService TenantService;

    public TenantController(TenantService service)
    {
        TenantService = service;
    }

    [HttpGet]
    async public Task<IEnumerable<TenantDto>> Get()
    {
        return await TenantService.ListAll();
    }

    [HttpGet("{id}")]
    async public Task<IActionResult> GetTenant(int id)
    {
        var tenantDto = await TenantService.Get(id);
        return Ok(tenantDto);
    }

    [HttpPost]
    async public Task<IActionResult> Post([FromBody] CreateTenantDto createTenantDto)
    {
        var tenantDto = await TenantService.Create(createTenantDto);
        return CreatedAtAction(nameof(GetTenant), new { id = tenantDto.Id }, tenantDto);
    }

    [HttpPut("{id}")]
    async public Task<IActionResult> Put([FromBody] UpdateTenantDto updateTenantDto, int id)
    {
        await TenantService.Update(updateTenantDto, id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    async public Task<IActionResult> Delete(int id)
    {
        await TenantService.Delete(id);
        return NoContent();
    }
}
