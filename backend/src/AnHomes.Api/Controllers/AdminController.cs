using AnHomes.Application.Dtos;
using AnHomes.Application.Dtos.Admin;
using AnHomes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnHomes.Api.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
	private readonly IAuthService _authService;
	private readonly IProjectService _projectService;
	private readonly IServiceService _serviceService;
	private readonly IPostService _postService;
	private readonly ICategoryService _categoryService;
	private readonly IContactService _contactService;
	private readonly ICloudinaryService _cloudinary;
	private readonly IBannerService _bannerService;
	private readonly IMediaService _mediaService;
	private readonly IUserService _userService;
	private readonly ISiteSettingsService _siteSettingsService;
	private readonly ISeoMetadataService _seoMetadataService;

	public AdminController(
		IAuthService authService,
		IProjectService projectService,
		IServiceService serviceService,
		IPostService postService,
		ICategoryService categoryService,
		IContactService contactService,
		ICloudinaryService cloudinary,
		IBannerService bannerService,
		IMediaService mediaService,
		IUserService userService,
		ISiteSettingsService siteSettingsService,
		ISeoMetadataService seoMetadataService)
	{
		_authService = authService;
		_projectService = projectService;
		_serviceService = serviceService;
		_postService = postService;
		_categoryService = categoryService;
		_contactService = contactService;
		_cloudinary = cloudinary;
		_bannerService = bannerService;
		_mediaService = mediaService;
		_userService = userService;
		_siteSettingsService = siteSettingsService;
		_seoMetadataService = seoMetadataService;
	}

	// ===== Auth =====
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

	// ===== Projects =====
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

	// ===== Services =====
	[HttpGet("services")]
	public async Task<ActionResult<IReadOnlyList<ServiceDto>>> GetServices(CancellationToken ct = default)
	{
		var result = await _serviceService.GetAllServicesAsync(1, 100, null, ct);
		return Ok(result);
	}

	[HttpPost("services")]
	public async Task<ActionResult<ServiceDto>> CreateService([FromBody] CreateServiceRequest request, CancellationToken ct = default)
	{
		var service = await _serviceService.CreateServiceAsync(request, ct);
		return Ok(service);
	}

	[HttpPut("services/{id}")]
	public async Task<ActionResult<ServiceDto>> UpdateService(Guid id, [FromBody] UpdateServiceRequest request, CancellationToken ct = default)
	{
		var service = await _serviceService.UpdateServiceAsync(id, request, ct);
		if (service == null) return NotFound(new { message = "Service not found" });
		return Ok(service);
	}

	[HttpDelete("services/{id}")]
	public async Task<IActionResult> DeleteService(Guid id, CancellationToken ct = default)
	{
		var deleted = await _serviceService.DeleteServiceAsync(id, ct);
		if (!deleted) return NotFound(new { message = "Service not found" });
		return NoContent();
	}

	// ===== Posts =====
	[HttpGet("posts")]
	public async Task<ActionResult<IReadOnlyList<PostDto>>> GetPosts([FromQuery] string? status = null, CancellationToken ct = default)
	{
		var result = await _postService.GetAllPostsAsync(1, 100, status, ct);
		return Ok(result);
	}

	[HttpPost("posts")]
	public async Task<ActionResult<PostDto>> CreatePost([FromBody] CreatePostRequest request, CancellationToken ct = default)
	{
		var post = await _postService.CreatePostAsync(request, ct);
		return Ok(post);
	}

	[HttpPut("posts/{id}")]
	public async Task<ActionResult<PostDto>> UpdatePost(Guid id, [FromBody] UpdatePostRequest request, CancellationToken ct = default)
	{
		var post = await _postService.UpdatePostAsync(id, request, ct);
		if (post == null) return NotFound(new { message = "Post not found" });
		return Ok(post);
	}

	[HttpDelete("posts/{id}")]
	public async Task<IActionResult> DeletePost(Guid id, CancellationToken ct = default)
	{
		var deleted = await _postService.DeletePostAsync(id, ct);
		if (!deleted) return NotFound(new { message = "Post not found" });
		return NoContent();
	}

	// ===== Categories =====
	[HttpGet("categories")]
	public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetCategories([FromQuery] string? type = null, CancellationToken ct = default)
	{
		if (!string.IsNullOrEmpty(type))
		{
			var result = await _categoryService.GetCategoriesByTypeAsync(type, ct);
			return Ok(result);
		}
		var all = await _categoryService.GetAllAsync(ct);
		return Ok(all);
	}

	[HttpPost("categories")]
	public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CreateCategoryRequest request, CancellationToken ct = default)
	{
		var cat = await _categoryService.CreateAsync(request, ct);
		return Ok(cat);
	}

	[HttpPut("categories/{id}")]
	public async Task<ActionResult<CategoryDto>> UpdateCategory(Guid id, [FromBody] UpdateCategoryRequest request, CancellationToken ct = default)
	{
		var cat = await _categoryService.UpdateAsync(id, ct);
		if (cat == null) return NotFound(new { message = "Category not found" });
		return Ok(cat);
	}

	[HttpDelete("categories/{id}")]
	public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken ct = default)
	{
		var deleted = await _categoryService.DeleteAsync(id, ct);
		if (!deleted) return NotFound(new { message = "Category not found" });
		return NoContent();
	}

	// ===== Banners =====
	[HttpGet("banners")]
	public async Task<ActionResult<IReadOnlyList<BannerDto>>> GetBanners(CancellationToken ct = default)
	{
		var result = await _bannerService.GetAllAsync(ct);
		return Ok(result);
	}

	[HttpPost("banners")]
	public async Task<ActionResult<BannerDto>> CreateBanner([FromBody] CreateBannerRequest request, CancellationToken ct = default)
	{
		var banner = await _bannerService.CreateAsync(request, ct);
		return Ok(banner);
	}

	[HttpPut("banners/{id}")]
	public async Task<ActionResult<BannerDto>> UpdateBanner(Guid id, [FromBody] UpdateBannerRequest request, CancellationToken ct = default)
	{
		var banner = await _bannerService.UpdateAsync(id, request, ct);
		if (banner == null) return NotFound(new { message = "Banner not found" });
		return Ok(banner);
	}

	[HttpDelete("banners/{id}")]
	public async Task<IActionResult> DeleteBanner(Guid id, CancellationToken ct = default)
	{
		var deleted = await _bannerService.DeleteAsync(id, ct);
		if (!deleted) return NotFound(new { message = "Banner not found" });
		return NoContent();
	}

	// ===== Media =====
	[HttpPost("media/upload")]
	public async Task<ActionResult<UploadResponse>> UploadMedia(IFormFile file, [FromQuery] string folder = "general", CancellationToken ct = default)
	{
		if (file == null || file.Length == 0)
			return BadRequest(new { message = "No file uploaded" });

		try
		{
			using var stream = file.OpenReadStream();
			var result = await _mediaService.UploadAsync(stream, file.FileName, file.ContentType, folder, ct);
			return Ok(new UploadResponse { Url = result.Url, PublicId = result.PublicId });
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

	[HttpGet("media")]
	public async Task<ActionResult<IReadOnlyList<MediaDto>>> GetMedia([FromQuery] string? folder = null, CancellationToken ct = default)
	{
		var result = await _mediaService.GetAllAsync(folder, ct);
		return Ok(result);
	}

	[HttpDelete("media/{id}")]
	public async Task<IActionResult> DeleteMedia(Guid id, CancellationToken ct = default)
	{
		var deleted = await _mediaService.DeleteAsync(id, ct);
		if (!deleted) return NotFound(new { message = "Media not found" });
		return NoContent();
	}

	// ===== Contacts =====
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

	// ===== Users =====
	[HttpGet("users")]
	public async Task<ActionResult<IReadOnlyList<UserDto>>> GetUsers(CancellationToken ct = default)
	{
		var result = await _userService.GetAllAsync(ct);
		return Ok(result);
	}

	[HttpPost("users")]
	public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserRequest request, CancellationToken ct = default)
	{
		var user = await _userService.CreateAsync(request, ct);
		return Ok(user);
	}

	[HttpPut("users/{id}")]
	public async Task<ActionResult<UserDto>> UpdateUser(Guid id, [FromBody] UpdateUserRequest request, CancellationToken ct = default)
	{
		var user = await _userService.UpdateAsync(id, request, ct);
		if (user == null) return NotFound(new { message = "User not found" });
		return Ok(user);
	}

	[HttpDelete("users/{id}")]
	public async Task<IActionResult> DeleteUser(Guid id, CancellationToken ct = default)
	{
		var deleted = await _userService.DeleteAsync(id, ct);
		if (!deleted) return NotFound(new { message = "User not found" });
		return NoContent();
	}

	// ===== Site Settings =====
	[HttpGet("site-settings")]
	public async Task<ActionResult<IReadOnlyList<SiteSettingDto>>> GetSiteSettings([FromQuery] string? group = null, CancellationToken ct = default)
	{
		if (!string.IsNullOrEmpty(group))
		{
			var result = await _siteSettingsService.GetByGroupAsync(group, ct);
			return Ok(result);
		}
		var all = await _siteSettingsService.GetAllAsync(ct);
		return Ok(all);
	}

	[HttpPost("site-settings")]
	public async Task<ActionResult<SiteSettingDto>> CreateSiteSetting([FromBody] CreateSiteSettingRequest request, CancellationToken ct = default)
	{
		var setting = await _siteSettingsService.CreateAsync(request, ct);
		return Ok(setting);
	}

	[HttpPut("site-settings/{id}")]
	public async Task<ActionResult<SiteSettingDto>> UpdateSiteSetting(Guid id, [FromBody] UpdateSiteSettingRequest request, CancellationToken ct = default)
	{
		var setting = await _siteSettingsService.UpdateAsync(id, request, ct);
		if (setting == null) return NotFound(new { message = "Setting not found" });
		return Ok(setting);
	}

	[HttpDelete("site-settings/{id}")]
	public async Task<IActionResult> DeleteSiteSetting(Guid id, CancellationToken ct = default)
	{
		var deleted = await _siteSettingsService.DeleteAsync(id, ct);
		if (!deleted) return NotFound(new { message = "Setting not found" });
		return NoContent();
	}

	// ===== SEO Metadata =====
	[HttpGet("seo-metadata")]
	public async Task<ActionResult<IReadOnlyList<SeoMetadataDto>> GetSeoMetadata(CancellationToken ct = default)
	{
		var result = await _seoMetadataService.GetAllAsync(ct);
		return Ok(result);
	}

	[HttpGet("seo-metadata/{page}")]
	public async Task<ActionResult<SeoMetadataDto>> GetSeoMetadataByPage(string page, CancellationToken ct = default)
	{
		var result = await _seoMetadataService.GetByPageAsync(page, ct);
		if (result == null) return NotFound(new { message = "SEO metadata not found" });
		return Ok(result);
	}

	[HttpPost("seo-metadata")]
	public async Task<ActionResult<SeoMetadataDto>> CreateSeoMetadata([FromBody] CreateSeoMetadataRequest request, CancellationToken ct = default)
	{
		var meta = await _seoMetadataService.CreateAsync(request, ct);
		return Ok(meta);
	}

	[HttpPut("seo-metadata/{id}")]
	public async Task<ActionResult<SeoMetadataDto>> UpdateSeoMetadata(Guid id, [FromBody] UpdateSeoMetadataRequest request, CancellationToken ct = default)
	{
		var meta = await _seoMetadataService.UpdateAsync(id, request, ct);
		if (meta == null) return NotFound(new { message = "SEO metadata not found" });
		return Ok(meta);
	}

	[HttpDelete("seo-metadata/{id}")]
	public async Task<IActionResult> DeleteSeoMetadata(Guid id, CancellationToken ct = default)
	{
		var deleted = await _seoMetadataService.DeleteAsync(id, ct);
		if (!deleted) return NotFound(new { message = "SEO metadata not found" });
		return NoContent();
	}
}