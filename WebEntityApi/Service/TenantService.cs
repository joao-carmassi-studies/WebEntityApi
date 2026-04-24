using WebEntityApi.Dtos;
using WebEntityApi.Exceptions;
using WebEntityApi.Models;
using WebEntityApi.Repository;

namespace WebEntityApi.Service;

public class TenantService
{
    public Dal<Tenant> Tenants { get; set; }
    public Dal<User> Users { get; set; }

    public TenantService(Dal<Tenant> tenants, Dal<User> users)
    {
        Tenants = tenants;
        Users = users;
    }

    async public Task<IEnumerable<TenantDto>> ListAll()
    {
        var tenants = await Tenants.ListAsync();
        return tenants.Select(t => t.ToDto());
    }

    async public Task<TenantDto> Get(int id)
    {
        var tenant = await Tenants.FindAsync(t => t.Id == id)
            ?? throw new NotFoundException($"Tenant with id {id} not found.");
        return tenant.ToDto();
    }

    async public Task<TenantDto> Create(CreateTenantDto createTenantDto)
    {
        var user = await Users.FindAsync(u => u.Id == createTenantDto.OwnerId)
            ?? throw new NotFoundException($"User with id {createTenantDto.OwnerId} not found.");

        var tenant = createTenantDto.ToEntity(user);
        await Tenants.AddAsync(tenant);
        return tenant.ToDto();
    }

    async public Task Update(UpdateTenantDto updateTenantDto, int id)
    {
        var tenant = await Tenants.FindAsync(t => t.Id == id)
            ?? throw new NotFoundException($"Tenant with id {id} not found.");

        tenant.UpdateFromDto(updateTenantDto);
        await Tenants.Update(tenant);
    }

    async public Task Delete(int id)
    {
        var tenant = await Tenants.FindAsync(t => t.Id == id)
            ?? throw new NotFoundException($"Tenant with id {id} not found.");

        await Tenants.Remove(tenant);
    }
}
