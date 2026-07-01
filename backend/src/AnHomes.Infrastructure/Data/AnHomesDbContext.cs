using AnHomes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnHomes.Infrastructure.Data;

public class AnHomesDbContext : DbContext
{
    public AnHomesDbContext(DbContextOptions<AnHomesDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectImage> ProjectImages => Set<ProjectImage>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Banner> Banners => Set<Banner>();
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<SiteSetting> SiteSettings => Set<SiteSetting>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ApplyConfigurations(modelBuilder);
    }

    private static void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        modelBuilder.ApplyConfiguration(new AuditLogConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectImageConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceConfiguration());
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new BannerConfiguration());
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
        modelBuilder.ApplyConfiguration(new SiteSettingConfiguration());
 modelBuilder.ApplyConfiguration(new MediaFileConfiguration());
 modelBuilder.ApplyConfiguration(new SeoMetadataConfiguration());
    }
}

// Entity Configurations
file class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(u => u.Email).IsRequired().HasMaxLength(256);
        builder.Property(u => u.PasswordHash).IsRequired();
        builder.Property(u => u.FullName).IsRequired().HasMaxLength(256);
        builder.Property(u => u.Role).IsRequired().HasMaxLength(50);
        builder.Property(u => u.IsActive).HasDefaultValue(true);
        builder.Property(u => u.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        builder.HasMany(u => u.RefreshTokens).WithOne(rt => rt.User).HasForeignKey(rt => rt.UserId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(u => u.AuditLogs).WithOne(al => al.User).HasForeignKey(al => al.UserId).OnDelete(DeleteBehavior.Restrict);
    }
}

file class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");
        builder.HasKey(rt => rt.Id);
        builder.HasIndex(rt => rt.Token).IsUnique();
        builder.Property(rt => rt.Token).IsRequired();
        builder.Property(rt => rt.ExpiresAt).IsRequired();
        builder.Property(rt => rt.IsRevoked).HasDefaultValue(false);
        builder.Property(rt => rt.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
    }
}

file class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLogs");
        builder.HasKey(al => al.Id);
        builder.Property(al => al.Action).IsRequired().HasMaxLength(256);
        builder.Property(al => al.EntityType).IsRequired().HasMaxLength(256);
        builder.Property(al => al.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        builder.HasIndex(al => new { al.EntityType, al.EntityId });
    }
}

file class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => new { c.Slug, c.Type }).IsUnique();
        builder.Property(c => c.Name).IsRequired().HasMaxLength(256);
        builder.Property(c => c.Slug).IsRequired().HasMaxLength(256);
        builder.Property(c => c.Type).IsRequired().HasMaxLength(50);
        builder.Property(c => c.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
    }
}

file class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Slug).IsUnique();
        builder.Property(p => p.Title).IsRequired().HasMaxLength(512);
        builder.Property(p => p.Slug).IsRequired().HasMaxLength(512);
        builder.Property(p => p.ShortDescription).IsRequired().HasMaxLength(2000);
        builder.Property(p => p.Content).IsRequired();
        builder.Property(p => p.Style).IsRequired().HasMaxLength(256);
        builder.Property(p => p.Area).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Location).IsRequired().HasMaxLength(512);
        builder.Property(p => p.Year).IsRequired().HasMaxLength(10);
        builder.Property(p => p.Status).IsRequired().HasMaxLength(50).HasDefaultValue("draft");
        builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        builder.HasOne(p => p.Category).WithMany(c => c.Projects).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(p => p.Images).WithOne(pi => pi.Project).HasForeignKey(pi => pi.ProjectId).OnDelete(DeleteBehavior.Cascade);
    }
}

file class ProjectImageConfiguration : IEntityTypeConfiguration<ProjectImage>
{
    public void Configure(EntityTypeBuilder<ProjectImage> builder)
    {
        builder.ToTable("ProjectImages");
        builder.HasKey(pi => pi.Id);
        builder.Property(pi => pi.ImageUrl).IsRequired();
        builder.Property(pi => pi.PublicId).IsRequired();
        builder.Property(pi => pi.AltText).IsRequired().HasMaxLength(512);
        builder.Property(pi => pi.SortOrder).HasDefaultValue(0);
        builder.Property(pi => pi.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
    }
}

file class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("Services");
        builder.HasKey(s => s.Id);
        builder.HasIndex(s => s.Slug).IsUnique();
        builder.Property(s => s.Title).IsRequired().HasMaxLength(512);
        builder.Property(s => s.Slug).IsRequired().HasMaxLength(512);
        builder.Property(s => s.ShortDescription).IsRequired().HasMaxLength(2000);
        builder.Property(s => s.Content).IsRequired();
        builder.Property(s => s.Status).IsRequired().HasMaxLength(50).HasDefaultValue("draft");
        builder.Property(s => s.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
    }
}

file class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Slug).IsUnique();
        builder.Property(p => p.Title).IsRequired().HasMaxLength(512);
        builder.Property(p => p.Slug).IsRequired().HasMaxLength(512);
        builder.Property(p => p.ShortDescription).IsRequired().HasMaxLength(2000);
        builder.Property(p => p.Content).IsRequired();
        builder.Property(p => p.Status).IsRequired().HasMaxLength(50).HasDefaultValue("draft");
        builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        builder.HasOne(p => p.Category).WithMany(c => c.Posts).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict);
    }
}

file class BannerConfiguration : IEntityTypeConfiguration<Banner>
{
    public void Configure(EntityTypeBuilder<Banner> builder)
    {
        builder.ToTable("Banners");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Title).IsRequired().HasMaxLength(512);
        builder.Property(b => b.ImageUrl).IsRequired();
        builder.Property(b => b.ImagePublicId).IsRequired();
        builder.Property(b => b.SortOrder).HasDefaultValue(0);
        builder.Property(b => b.IsActive).HasDefaultValue(true);
        builder.Property(b => b.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
    }
}

file class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contacts");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.FullName).IsRequired().HasMaxLength(256);
        builder.Property(c => c.Phone).IsRequired().HasMaxLength(20);
        builder.Property(c => c.Email).HasMaxLength(256);
        builder.Property(c => c.Need).IsRequired().HasMaxLength(256);
        builder.Property(c => c.Message).IsRequired();
        builder.Property(c => c.Status).IsRequired().HasMaxLength(50).HasDefaultValue("new");
        builder.Property(c => c.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        builder.HasIndex(c => c.CreatedAt);
    }
}

file class SiteSettingConfiguration : IEntityTypeConfiguration<SiteSetting>
{
    public void Configure(EntityTypeBuilder<SiteSetting> builder)
    {
        builder.ToTable("SiteSettings");
        builder.HasKey(s => s.Id);
        builder.HasIndex(s => new { s.Key, s.Group }).IsUnique();
        builder.Property(s => s.Key).IsRequired().HasMaxLength(256);
        builder.Property(s => s.Value).IsRequired();
        builder.Property(s => s.Group).IsRequired().HasMaxLength(100);
        builder.Property(s => s.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
    }
}




file class MediaFileConfiguration : IEntityTypeConfiguration<MediaFile>
{
 public void Configure(EntityTypeBuilder<MediaFile> builder)
 {
 builder.ToTable("MediaFiles");
 builder.HasKey(m => m.Id);
 builder.Property(m => m.Url).IsRequired();
 builder.Property(m => m.PublicId).IsRequired();
 builder.Property(m => m.FileName).HasMaxLength(512);
 builder.Property(m => m.ContentType).HasMaxLength(100);
 builder.Property(m => m.Folder).HasMaxLength(256);
 builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
 }
}

file class SeoMetadataConfiguration : IEntityTypeConfiguration<SeoMetadata>
{
 public void Configure(EntityTypeBuilder<SeoMetadata> builder)
 {
 builder.ToTable("SeoMetadatas");
 builder.HasKey(s => s.Id);
 builder.HasIndex(s => s.Page).IsUnique();
 builder.Property(s => s.Page).IsRequired().HasMaxLength(256);
 builder.Property(s => s.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
 }
}

