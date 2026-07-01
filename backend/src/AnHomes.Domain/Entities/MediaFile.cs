namespace AnHomes.Domain.Entities;

public class MediaFile
{
 public Guid Id { get; set; } = Guid.NewGuid();
 public string Url { get; set; } = string.Empty;
 public string PublicId { get; set; } = string.Empty;
 public string? AltText { get; set; }
 public string? FileName { get; set; }
 public long FileSize { get; set; }
 public string? ContentType { get; set; }
 public string? Folder { get; set; }
 public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
