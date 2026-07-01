namespace AnHomes.Domain.Entities;

public class Service
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public string? ImageUrl { get; set; }
    public string? ImagePublicId { get; set; }
    public bool IsFeatured { get; set; }
    public int SortOrder { get; set; } = 0;
    public string Status { get; set; } = "draft";
    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
