namespace AnHomes.Application.Dtos;

public class BannerDto
{
 public Guid Id { get; set; }
 public string Title { get; set; } = string.Empty;
 public string? Subtitle { get; set; }
 public string ImageUrl { get; set; } = string.Empty;
 public string? LinkUrl { get; set; }
 public int SortOrder { get; set; }
 public bool IsActive { get; set; }
 public DateTime CreatedAt { get; set; }
}

public class CreateBannerRequest
{
 public string Title { get; set; } = string.Empty;
 public string? Subtitle { get; set; }
 public string ImageUrl { get; set; } = string.Empty;
 public string ImagePublicId { get; set; } = string.Empty;
 public string? LinkUrl { get; set; }
 public int SortOrder { get; set; }
 public bool IsActive { get; set; } = true;
}

public class UpdateBannerRequest : CreateBannerRequest
{
 public Guid Id { get; set; }
}

public class MediaDto
{
 public Guid Id { get; set; }
 public string Url { get; set; } = string.Empty;
 public string PublicId { get; set; } = string.Empty;
 public string? AltText { get; set; }
 public string? FileName { get; set; }
 public long FileSize { get; set; }
 public string? Folder { get; set; }
 public DateTime CreatedAt { get; set; }
}

public class SeoMetadataDto
{
 public Guid Id { get; set; }
 public string Page { get; set; } = string.Empty;
 public string? Title { get; set; }
 public string? Description { get; set; }
 public string? Keywords { get; set; }
 public string? OgImage { get; set; }
 public string? CanonicalUrl { get; set; }
 public string? SchemaJson { get; set; }
 public DateTime UpdatedAt { get; set; }
}

public class CreateSeoMetadataRequest
{
 public string Page { get; set; } = string.Empty;
 public string? Title { get; set; }
 public string? Description { get; set; }
 public string? Keywords { get; set; }
 public string? OgImage { get; set; }
 public string? CanonicalUrl { get; set; }
 public string? SchemaJson { get; set; }
}

public class UpdateSeoMetadataRequest : CreateSeoMetadataRequest
{
 public Guid Id { get; set; }
}

public class SiteSettingDto
{
 public Guid Id { get; set; }
 public string Key { get; set; } = string.Empty;
 public string Value { get; set; } = string.Empty;
 public string Group { get; set; } = string.Empty;
 public string? Description { get; set; }
 public DateTime UpdatedAt { get; set; }
}

public class CreateSiteSettingRequest
{
 public string Key { get; set; } = string.Empty;
 public string Value { get; set; } = string.Empty;
 public string Group { get; set; } = string.Empty;
 public string? Description { get; set; }
}

public class CreateCategoryRequest
{
 public string Name { get; set; } = string.Empty;
 public string Slug { get; set; } = string.Empty;
 public string Type { get; set; } = "project";
 public string? Description { get; set; }
 public int SortOrder { get; set; }
 public bool IsActive { get; set; } = true;
}

public class UpdateCategoryRequest : CreateCategoryRequest
{
 public Guid Id { get; set; }
}

public class UpdateSiteSettingRequest : CreateSiteSettingRequest
{
 public Guid Id { get; set; }
}

