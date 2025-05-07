using Microsoft.AspNetCore.Mvc;
using Shaghaf.Core.Dtos.MembershipDtos;
using Shaghaf.Core.Services.Contract;
using System.Threading.Tasks;
using Talabat.APIs.Controllers;


public class MembershipController : BaseApiController
{
    private readonly IMembershipService _membershipService;

    public MembershipController(IMembershipService membershipService)
    {
        _membershipService = membershipService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMembership([FromBody] MembershipToCreateDto membershipDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _membershipService.CreateMembershipAsync(membershipDto);
        return CreatedAtAction(nameof(GetMembershipById), new { id = result.Id }, result);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMembership(int id, [FromBody] MembershipDto membershipDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        membershipDto.Id = id;
        await _membershipService.UpdateMembershipAsync(membershipDto);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMembershipById(int id)
    {
        var membership = await _membershipService.GetMembershipByIdAsync(id);

        if (membership == null)
        {
            return NotFound();
        }

        return Ok(membership);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMemberships()
    {
        var result = await _membershipService.GetAllMembershipsAsync();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMembership(int id)
    {
        var success = await _membershipService.DeleteMembershipAsync(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
