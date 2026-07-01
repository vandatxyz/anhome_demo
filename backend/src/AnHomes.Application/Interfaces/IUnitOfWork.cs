using AnHomes.Application.Interfaces;
using AnHomes.Domain.Entities;

namespace AnHomes.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IRepository<RefreshToken> RefreshTokens { get; }
    IRepository<AuditLog> AuditLogs { get; }
    IRepository<Category> Categories { get; }
    IRepository<Project> Projects { get; }
    IRepository<ProjectImage> ProjectImages { get; }
    IRepository<Service> Services { get; }
    IRepository<Post> Posts { get; }
    IRepository<Banner> Banners { get; }
    IRepository<Contact> Contacts { get; }
    IRepository<SiteSetting> SiteSettings { get; }
    ICloudinaryService Cloudinary { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
