using AnHomes.Application.Interfaces;
using AnHomes.Domain.Entities;
using AnHomes.Infrastructure.Repositories;
using AnHomes.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace AnHomes.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AnHomes.Infrastructure.Data.AnHomesDbContext _context;
    private readonly ICloudinaryService _cloudinaryService;

    public UnitOfWork(AnHomes.Infrastructure.Data.AnHomesDbContext context, IConfiguration configuration)
    {
        _context = context;
        _cloudinaryService = new CloudinaryService(configuration);
    }

    public IRepository<User> Users => new Repository<User>(_context);
    public IRepository<RefreshToken> RefreshTokens => new Repository<RefreshToken>(_context);
    public IRepository<AuditLog> AuditLogs => new Repository<AuditLog>(_context);
    public IRepository<Category> Categories => new Repository<Category>(_context);
    public IRepository<Project> Projects => new Repository<Project>(_context);
    public IRepository<ProjectImage> ProjectImages => new Repository<ProjectImage>(_context);
    public IRepository<Service> Services => new Repository<Service>(_context);
    public IRepository<Post> Posts => new Repository<Post>(_context);
    public IRepository<Banner> Banners => new Repository<Banner>(_context);
    public IRepository<Contact> Contacts => new Repository<Contact>(_context);
    public IRepository<SiteSetting> SiteSettings => new Repository<SiteSetting>(_context);
 public IRepository<MediaFile> MediaFiles => new Repository<MediaFile>(_context);
 public IRepository<SeoMetadata> SeoMetadatas => new Repository<SeoMetadata>(_context);
    public ICloudinaryService Cloudinary => _cloudinaryService;

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        => await _context.SaveChangesAsync(ct);

    public void Dispose() => _context.Dispose();
}
