using WebEntityApi.Models;

namespace WebEntityApi.Dtos;

public static class TenantMapper
{
    public static TenantDto ToDto(this Tenant tenant)
    {
        return new TenantDto
        {
            Id = tenant.Id,
            Name = tenant.Name,
            CreationTime = tenant.CreationTime,
            OwnerId = tenant.OwnerId
        };
    }

    public static Tenant ToEntity(this CreateTenantDto createTenantDto, User owner)
    {
        return new Tenant
        {
            Name = createTenantDto.Name,
            OwnerId = owner.Id,
            Owner = owner,
        };
    }

    public static void UpdateFromDto(this Tenant tenant, UpdateTenantDto updateTenantDto)
    {
        tenant.Name = updateTenantDto.Name ?? tenant.Name;
    }
}
