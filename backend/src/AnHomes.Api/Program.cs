using AnHomes.Application.Interfaces;
using AnHomes.Application.Services;
using AnHomes.Domain.Entities;
using AnHomes.Infrastructure;
using AnHomes.Infrastructure.Data;
using AnHomes.Infrastructure.Repositories;
using AnHomes.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default")
    ?? "Server=localhost;Database=AnHome;User ID=sa;Password=123456;Max Pool Size=500;Encrypt=False;";

builder.Services.AddDbContext<AnHomesDbContext>(options =>
    options.UseSqlServer(connectionString, sql =>
    {
        sql.EnableRetryOnFailure(3, TimeSpan.FromSeconds(10), null);
        sql.CommandTimeout(30);
    }));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IContactService, ContactService>();

var jwtSecret = builder.Configuration["Jwt:Secret"] ?? "AnHomesSecretKey2024!@#ChangeInProduction";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "AnHomes";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "AnHomesClient";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
        ClockSkew = TimeSpan.FromMinutes(2)
    };
});

builder.Services.AddAuthorization();

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
    ?? new[] { "http://localhost:3000", "http://localhost:3001", "https://noithatanhomes.vn" };

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(allowedOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseHsts();
app.Use(async (context, next) =>
{
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    context.Response.Headers["X-Frame-Options"] = "DENY";
    context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
    context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
    context.Response.Headers["Permissions-Policy"] = "camera=(), microphone=(), geolocation=()";
    await next();
});

app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AnHomesDbContext>();
    try
    {
        db.Database.EnsureCreated();
        await SeedDataAsync(db);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("Startup");
        logger.LogError(ex, "Database initialization failed");
    }
}

app.MapControllers();
app.Run();

static async Task SeedDataAsync(AnHomesDbContext db)
{
    if (!db.Users.Any())
    {
        var admin = new User
        {
            Id = Guid.NewGuid(),
            Email = "admin@anhome.vn",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            FullName = "Admin AnHomes",
            Role = "admin",
            IsActive = true
        };
        db.Users.Add(admin);
        await db.SaveChangesAsync();
    }

    if (!db.Categories.Any())
    {
        var categories = new[]
        {
            new Category { Name = "Tat ca", Slug = "tat-ca", Type = "project", SortOrder = 0 },
            new Category { Name = "Can ho cao cap", Slug = "can-ho-cao-cap", Type = "project", SortOrder = 1 },
            new Category { Name = "Biet thu", Slug = "biet-thu", Type = "project", SortOrder = 2 },
            new Category { Name = "Nha pho", Slug = "nha-pho", Type = "project", SortOrder = 3 },
            new Category { Name = "Van phong", Slug = "van-phong", Type = "project", SortOrder = 4 },
            new Category { Name = "Tin tuc", Slug = "tin-tuc", Type = "post", SortOrder = 10 },
            new Category { Name = "Tu van", Slug = "tu-van", Type = "post", SortOrder = 11 },
        };
        db.Categories.AddRange(categories);
        await db.SaveChangesAsync();
    }

    if (!db.Services.Any())
    {
        var services = new[]
        {
            new Service { Title = "Thiet Ke Noi That", Slug = "thiet-ke-noi-that", ShortDescription = "Thiet ke noi that theo phong cach Luxury Modern Minimal", Content = "<p>Chuyen thiet ke noi that cao cap...</p>", Icon = "design", IsFeatured = true, SortOrder = 1, Status = "published" },
            new Service { Title = "Thi Cong Noi That", Slug = "thi-cong-noi-that", ShortDescription = "Thi cong noi that tron goi, chat luong cao", Content = "<p>Thi cong noi that voi doi ngu chuyen nghiep...</p>", Icon = "construction", IsFeatured = true, SortOrder = 2, Status = "published" },
            new Service { Title = "Tu Van Noi That", Slug = "tu-van-noi-that", ShortDescription = "Tu van thiet ke noi that mien phi", Content = "<p>Tu van chuyen sau ve thiet ke...</p>", Icon = "consulting", IsFeatured = true, SortOrder = 3, Status = "published" },
            new Service { Title = "San Xuat Do Go", Slug = "san-xuat-do-go", ShortDescription = "San xuat do go cao cap theo yeu cau", Content = "<p>Xuong san xuat do go hien dai...</p>", Icon = "factory", IsFeatured = false, SortOrder = 4, Status = "published" },
        };
        db.Services.AddRange(services);
        await db.SaveChangesAsync();
    }

    if (!db.Projects.Any())
    {
        var catCanHo = db.Categories.First(c => c.Slug == "can-ho-cao-cap");
        var catBietThu = db.Categories.First(c => c.Slug == "biet-thu");
        var catNhaPho = db.Categories.First(c => c.Slug == "nha-pho");
        var catVanPhong = db.Categories.First(c => c.Slug == "van-phong");

        var projects = new[]
        {
            new Project { Title = "Penthouse The Landmark", Slug = "penthouse-the-landmark", ShortDescription = "Can ho penthouse 250m2 voi view toan canh", Content = "<p>Du an can ho penthouse tai The Landmark...</p>", CategoryId = catCanHo.Id, Style = "Luxury Modern", Area = "250m2", Location = "Quan 1, TP.HCM", Year = "2024", ThumbnailUrl = "", IsFeatured = true, Status = "published", PublishedAt = DateTime.UtcNow.AddDays(-30) },
            new Project { Title = "Biet Thu Phu My Hung", Slug = "biet-thu-phu-my-hung", ShortDescription = "Biet thu 500m2 phong cach Classic Luxury", Content = "<p>Biet thu cao cap tai Phu My Hung...</p>", CategoryId = catBietThu.Id, Style = "Classic Luxury", Area = "500m2", Location = "Quan 7, TP.HCM", Year = "2024", ThumbnailUrl = "", IsFeatured = true, Status = "published", PublishedAt = DateTime.UtcNow.AddDays(-60) },
            new Project { Title = "Nha Pho Thao Dien", Slug = "nha-pho-thao-dien", ShortDescription = "Nha pho 180m2 phong cach Minimalist", Content = "<p>Nha pho hien dai tai Thao Dien...</p>", CategoryId = catNhaPho.Id, Style = "Minimalist", Area = "180m2", Location = "Quan 2, TP.HCM", Year = "2024", ThumbnailUrl = "", IsFeatured = true, Status = "published", PublishedAt = DateTime.UtcNow.AddDays(-45) },
            new Project { Title = "Can Ho Landmark 81", Slug = "can-ho-landmark-81", ShortDescription = "Can ho cao cap 200m2 tai Landmark 81", Content = "<p>Can ho cao cap tai toa nha Landmark 81...</p>", CategoryId = catCanHo.Id, Style = "Contemporary", Area = "200m2", Location = "Quan Binh Thanh, TP.HCM", Year = "2024", ThumbnailUrl = "", IsFeatured = true, Status = "published", PublishedAt = DateTime.UtcNow.AddDays(-15) },
            new Project { Title = "Van Phong CEO Group", Slug = "van-phong-ceo-group", ShortDescription = "Thiet ke van phong 800m2 cho CEO Group", Content = "<p>Van phong lam viec hien dai...</p>", CategoryId = catVanPhong.Id, Style = "Modern Office", Area = "800m2", Location = "Quan 1, TP.HCM", Year = "2024", ThumbnailUrl = "", IsFeatured = false, Status = "published", PublishedAt = DateTime.UtcNow.AddDays(-90) },
        };
        db.Projects.AddRange(projects);
        await db.SaveChangesAsync();
    }

    if (!db.Posts.Any())
    {
        var catTinTuc = db.Categories.First(c => c.Slug == "tin-tuc");
        var posts = new[]
        {
            new Post { Title = "Xu Huong Thiet Ke Noi That 2024", Slug = "xu-huong-thiet-ke-noi-that-2024", ShortDescription = "Kham pha cac xu huong thiet ke noi that moi nhat nam 2024", Content = "<p>Nam 2024 mang den nhung xu huong moi...</p>", CategoryId = catTinTuc.Id, CoverImage = "", Status = "published", PublishedAt = DateTime.UtcNow.AddDays(-10) },
            new Post { Title = "Cach Chon Do Go Cho Nha Dep", Slug = "cach-chon-do-go-cho-nha-dep", ShortDescription = "Meo chon do go phu hop voi khong gian song", Content = "<p>Do go la lua chon pho bien...</p>", CategoryId = catTinTuc.Id, CoverImage = "", Status = "published", PublishedAt = DateTime.UtcNow.AddDays(-20) },
            new Post { Title = "Bao Tri Noi That Nha", Slug = "bao-tri-noi-that-nha", ShortDescription = "Huong dan bao tri noi that de ben dep lau", Content = "<p>Bao tri noi that la can thiet...</p>", CategoryId = catTinTuc.Id, CoverImage = "", Status = "published", PublishedAt = DateTime.UtcNow.AddDays(-30) },
        };
        db.Posts.AddRange(posts);
        await db.SaveChangesAsync();
    }

    if (!db.SiteSettings.Any())
    {
        var settings = new[]
        {
            new SiteSetting { Key = "site_name", Value = "An Homes", Group = "general" },
            new SiteSetting { Key = "site_phone", Value = "0909.xxx.xxx", Group = "contact" },
            new SiteSetting { Key = "site_email", Value = "contact@noithatanhomes.vn", Group = "contact" },
            new SiteSetting { Key = "site_address", Value = "HCMC, Viet Nam", Group = "contact" },
            new SiteSetting { Key = "site_zalo", Value = "https://zalo.me/0909xxx", Group = "contact" },
        };
        db.SiteSettings.AddRange(settings);
        await db.SaveChangesAsync();
    }
}
