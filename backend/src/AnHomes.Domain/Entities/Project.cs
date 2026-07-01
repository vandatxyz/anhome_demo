namespace AnHomes.Domain.Entities;

public class Project
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public string Style { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public string ThumbnailPublicId { get; set; } = string.Empty;
    public bool IsFeatured { get; set; }
    public string Status { get; set; } = "draft"; // draft, published, archived
    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }

    public ICollection<ProjectImage> Images { get; set; } = new List<ProjectImage>();
}
