namespace AnHomes.Domain.Entities;

public class SeoMetadata
{
 public Guid Id { get; set; } = Guid.NewGuid();
 public string Page { get; set; } = string.Empty;
 public string? Title { get; set; }
 public string? Description { get; set; }
 public string? Keywords { get; set; }
 public string? OgImage { get; set; }
 public string? CanonicalUrl { get; set; }
 public string? SchemaJson { get; set; }
 public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
