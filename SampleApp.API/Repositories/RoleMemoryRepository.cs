using SampleApp.API.Entities;
using SampleApp.API.Interfaces;

namespace SampleApp.API.Repositories;

public class RoleMemoryRepository : IRoleRepository
{
    public List<Role> Roles { get; set; } = new List<Role>();

    public Role CreateRole(Role role)
    {
        Roles.Add(role);
        return role;
    }

    public bool DeleteRole(int id)
    {
        var result = FindRoleById(id);
        Roles.Remove(result);
        return true;
    }

    public Role EditRole(Role role, int id)
    {
        var result = FindRoleById(id);
        result.Name = role.Name;
        result.Description = role.Description;
        return result;
    }

    public Role FindRoleById(int id)
    {
        var result = Roles.FirstOrDefault(r => r.Id == id);

        if (result == null)
        {
            throw new Exception($"Нет роли с id = {id}");
        }

        return result;
    }

    public List<Role> GetRoles()
    {
        return Roles;
    }
}
