namespace AnHomes.Application.Dtos;

// Project DTOs
public class ProjectDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string CategoryId { get; set; } = string.Empty;
    public string? CategoryName { get; set; }
    public string Style { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public List<ProjectImageDto> Images { get; set; } = new();
    public bool IsFeatured { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }
}

public class ProjectImageDto
{
    public Guid Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string PublicId { get; set; } = string.Empty;
    public string AltText { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}

public class CreateProjectRequest
{
    public string Title { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public string Style { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
    public bool IsFeatured { get; set; }
    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
}

public class UpdateProjectRequest : CreateProjectRequest
{
    public Guid Id { get; set; }
    public string Status { get; set; } = "draft";
}

// Service DTOs
public class ServiceDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsFeatured { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
    public DateTime CreatedAt { get; set; }
}

// Post DTOs
public class PostDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? CoverImage { get; set; }
    public string CategoryId { get; set; } = string.Empty;
    public string? CategoryName { get; set; }
    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }
}

// Category DTOs
public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? Description { get; set; }
 public bool IsActive { get; set; }
 public int SortOrder { get; set; }
}

// Contact DTOs
public class ContactDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string Need { get; set; } = string.Empty;
    public string? Budget { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class CreateContactRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string Need { get; set; } = string.Empty;
    public string? Budget { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class UpdateContactStatusRequest
{
    public string Status { get; set; } = string.Empty;
}

// Banner DTOs
public class BannerDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Subtitle { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string? LinkUrl { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}

// Home DTOs
public class HomeDto
{
    public List<BannerDto> Banners { get; set; } = new();
    public List<ProjectDto> FeaturedProjects { get; set; } = new();
    public List<ServiceDto> FeaturedServices { get; set; } = new();
    public List<PostDto> LatestPosts { get; set; } = new();
}

// Paginated response
public class PagedResult<T>
{
    public List<T> Data { get; set; } = new();
    public int Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)Total / PageSize);
}

// Upload DTOs
public class UploadResponse
{
    public string Url { get; set; } = string.Empty;
    public string PublicId { get; set; } = string.Empty;
}


// Service Request DTOs
public class CreateServiceRequest
{
    public string Title { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public bool IsFeatured { get; set; }
    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
}

public class UpdateServiceRequest : CreateServiceRequest
{
    public Guid Id { get; set; }
    public string Status { get; set; } = "draft";
}

// Post Request DTOs
public class CreatePostRequest
{
    public string Title { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
}

public class UpdatePostRequest : CreatePostRequest
{
    public Guid Id { get; set; }
    public string Status { get; set; } = "draft";
}
