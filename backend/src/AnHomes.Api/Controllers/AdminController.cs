using AnHomes.Application.Dtos;
using AnHomes.Application.Dtos.Auth;
using AnHomes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnHomes.Api.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IProjectService _projectService;
    private readonly IContactService _contactService;
    private readonly ICloudinaryService _cloudinary;

    public AdminController(IAuthService authService, IProjectService projectService, IContactService contactService, ICloudinaryService cloudinary)
    {
        _authService = authService;
        _projectService = projectService;
        _contactService = contactService;
        _cloudinary = cloudinary;
    }

    [HttpPost("auth/login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request, CancellationToken ct = default)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var result = await _authService.LoginAsync(request, ipAddress, ct);
        if (result == null) return Unauthorized(new { message = "Invalid email or password" });
        return Ok(result);
    }

    [HttpPost("auth/refresh-token")]
    public async Task<ActionResult<RefreshTokenResponse>> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken ct = default)
    {
        var result = await _authService.RefreshTokenAsync(request.RefreshToken, ct);
        if (result == null) return Unauthorized(new { message = "Invalid or expired refresh token" });
        return Ok(result);
    }

    [HttpPost("auth/logout")]
    public async Task<IActionResult> Logout([FromBody] RefreshTokenRequest request, CancellationToken ct = default)
    {
        await _authService.LogoutAsync(request.RefreshToken, ct);
        return Ok(new { message = "Logged out successfully" });
    }

    [HttpGet("projects")]
    public async Task<ActionResult<PagedResult<ProjectDto>>> GetProjects([FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? status = null, CancellationToken ct = default)
    {
        var result = await _projectService.GetAllProjectsAsync(page, pageSize, status, ct);
        return Ok(result);
    }

    [HttpPost("projects")]
    public async Task<ActionResult<ProjectDto>> CreateProject([FromBody] CreateProjectRequest request, CancellationToken ct = default)
    {
        var project = await _projectService.CreateProjectAsync(request, ct);
        return CreatedAtAction(nameof(GetProjects), new { id = project.Id }, project);
    }

    [HttpPut("projects/{id}")]
    public async Task<ActionResult<ProjectDto>> UpdateProject(Guid id, [FromBody] UpdateProjectRequest request, CancellationToken ct = default)
    {
        var project = await _projectService.UpdateProjectAsync(id, request, ct);
        if (project == null) return NotFound(new { message = "Project not found" });
        return Ok(project);
    }

    [HttpDelete("projects/{id}")]
    public async Task<IActionResult> DeleteProject(Guid id, CancellationToken ct = default)
    {
        var deleted = await _projectService.DeleteProjectAsync(id, ct);
        if (!deleted) return NotFound(new { message = "Project not found" });
        return NoContent();
    }

    [HttpPost("media/upload")]
    public async Task<ActionResult<UploadResponse>> UploadMedia([FromBody] byte[] file, [FromQuery] string fileName, [FromQuery] string folder = "general", CancellationToken ct = default)
    {
        if (file == null || file.Length == 0)
            return BadRequest(new { message = "No file uploaded" });

        try
        {
            var (url, publicId) = await _cloudinary.UploadImageAsync(file, fileName, folder, ct);
            return Ok(new UploadResponse { Url = url, PublicId = publicId });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Upload failed: " + ex.Message });
        }
    }

    [HttpGet("contacts")]
    public async Task<ActionResult<PagedResult<ContactDto>>> GetContacts([FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? status = null, CancellationToken ct = default)
    {
        var result = await _contactService.GetAllContactsAsync(page, pageSize, status, ct);
        return Ok(result);
    }

    [HttpPut("contacts/{id}/status")]
    public async Task<ActionResult<ContactDto>> UpdateContactStatus(Guid id, [FromBody] UpdateContactStatusRequest request, CancellationToken ct = default)
    {
        var contact = await _contactService.UpdateContactStatusAsync(id, request.Status, ct);
        if (contact == null) return NotFound(new { message = "Contact not found" });
        return Ok(contact);
    }
}
