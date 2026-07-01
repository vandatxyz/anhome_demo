using AnHomes.Application.Dtos;
using AnHomes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnHomes.Api.Controllers;

[ApiController]
[Route("api/public")]
public class PublicController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IProjectService _projectService;
    private readonly IServiceService _serviceService;
    private readonly IPostService _postService;
    private readonly IContactService _contactService;

    public PublicController(ICategoryService categoryService, IProjectService projectService, IServiceService serviceService, IPostService postService, IContactService contactService)
    {
        _categoryService = categoryService;
        _projectService = projectService;
        _serviceService = serviceService;
        _postService = postService;
        _contactService = contactService;
    }

    [HttpGet("home")]
    public async Task<ActionResult<HomeDto>> GetHome(CancellationToken ct = default)
    {
        var featuredProjects = await _projectService.GetPublishedProjectsAsync(1, 6, category: null, style: null, ct: CancellationToken.None);
        var featuredServices = await _serviceService.GetPublishedServicesAsync(1, 6, ct: CancellationToken.None);
        var latestPosts = await _postService.GetPublishedPostsAsync(1, 3, category: null, ct: CancellationToken.None);

        return Ok(new HomeDto
        {
            Banners = new List<BannerDto>(), // TODO: Add banner service
            FeaturedProjects = featuredProjects.Data,
            FeaturedServices = featuredServices.Data,
            LatestPosts = latestPosts.Data
        });
    }

    [HttpGet("services")]
    public async Task<ActionResult<PagedResult<ServiceDto>>> GetServices([FromQuery] int page = 1, [FromQuery] int pageSize = 12, CancellationToken ct = default)
    {
        var result = await _serviceService.GetPublishedServicesAsync(page, pageSize, ct);
        return Ok(result);
    }

    [HttpGet("services/{slug}")]
    public async Task<ActionResult<ServiceDto>> GetService(string slug, CancellationToken ct = default)
    {
        var service = await _serviceService.GetPublishedServiceBySlugAsync(slug, ct);
        if (service == null) return NotFound(new { message = "Service not found" });
        return Ok(service);
    }

    [HttpGet("projects")]
    public async Task<ActionResult<PagedResult<ProjectDto>>> GetProjects([FromQuery] int page = 1, [FromQuery] int pageSize = 12, [FromQuery] string? category = null, [FromQuery] string? style = null, CancellationToken ct = default)
    {
        var result = await _projectService.GetPublishedProjectsAsync(page, pageSize, category, style, ct);
        return Ok(result);
    }

    [HttpGet("projects/{slug}")]
    public async Task<ActionResult<ProjectDto>> GetProject(string slug, CancellationToken ct = default)
    {
        var project = await _projectService.GetPublishedProjectBySlugAsync(slug, ct);
        if (project == null) return NotFound(new { message = "Project not found" });
        return Ok(project);
    }

    [HttpGet("posts")]
    public async Task<ActionResult<PagedResult<PostDto>>> GetPosts([FromQuery] int page = 1, [FromQuery] int pageSize = 12, [FromQuery] string? category = null, CancellationToken ct = default)
    {
        var result = await _postService.GetPublishedPostsAsync(page, pageSize, category, ct);
        return Ok(result);
    }

    [HttpGet("posts/{slug}")]
    public async Task<ActionResult<PostDto>> GetPost(string slug, CancellationToken ct = default)
    {
        var post = await _postService.GetPublishedPostBySlugAsync(slug, ct);
        if (post == null) return NotFound(new { message = "Post not found" });
        return Ok(post);
    }

    [HttpGet("categories")]
    public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetCategories([FromQuery] string type, CancellationToken ct)
    {
        var categories = await _categoryService.GetCategoriesByTypeAsync(type, ct);
        return Ok(categories);
    }

    [HttpPost("contact")]
    public async Task<ActionResult> SubmitContact([FromBody] CreateContactRequest request, CancellationToken ct = default)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var contact = await _contactService.CreateContactAsync(request, ipAddress, ct);
        return Ok(new { id = contact.Id, message = "Contact submitted successfully" });
    }
}
