namespace AnHomes.Domain.Entities;

public class Post
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? CoverImage { get; set; }
    public string? CoverImagePublicId { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
    public string Status { get; set; } = "draft";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }
}
