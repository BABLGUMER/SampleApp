using Microsoft.AspNetCore.Mvc;
using SampleApp.API.Entities;
using SampleApp.API.Interfaces;
using SampleApp.API.Validations;

namespace SampleApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleRepository _repo;

    public RolesController(IRoleRepository repo)
    {
        _repo = repo;
    }

    [HttpPost]
    public ActionResult CreateRole(Role role)
    {
        var validator = new RoleValidator();
        var result = validator.Validate(role);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors.First().ErrorMessage);
        }

        var createdRole = _repo.CreateRole(role);
        return CreatedAtAction(nameof(GetRoleById), new { id = createdRole.Id }, createdRole);
    }

    [HttpGet]
    public ActionResult GetRoles()
    {
        return Ok(_repo.GetRoles());
    }

    [HttpPut]
    public ActionResult UpdateRole(Role role)
    {
        var validator = new RoleValidator();
        var result = validator.Validate(role);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors.First().ErrorMessage);
        }

        return Ok(_repo.EditRole(role, role.Id));
    }

    [HttpGet("{id}")]
    public ActionResult GetRoleById(int id)
    {
        return Ok(_repo.FindRoleById(id));
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteRole(int id)
    {
        return Ok(_repo.DeleteRole(id));
    }
}
