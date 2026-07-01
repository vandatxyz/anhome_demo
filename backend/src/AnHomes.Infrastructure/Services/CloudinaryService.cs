using AnHomes.Application.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AnHomes.Infrastructure.Services;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;
    private readonly ILogger<CloudinaryService> _logger;
    private const long MaxFileSizeBytes = 10 * 1024 * 1024;
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp", ".gif"];

    public CloudinaryService(IConfiguration configuration, ILogger<CloudinaryService>? logger = null)
    {
        _logger = logger ?? Microsoft.Extensions.Logging.Abstractions.NullLogger<CloudinaryService>.Instance;
        var cloudName = configuration["Cloudinary:CloudName"] ?? throw new InvalidOperationException("Cloudinary:CloudName missing");
        var apiKey = configuration["Cloudinary:ApiKey"] ?? throw new InvalidOperationException("Cloudinary:ApiKey missing");
        var apiSecret = configuration["Cloudinary:ApiSecret"] ?? throw new InvalidOperationException("Cloudinary:ApiSecret missing");
        var account = new Account(cloudName, apiKey, apiSecret);
        _cloudinary = new Cloudinary(account);
        _cloudinary.Api.Secure = true;
    }

    public async Task<(string Url, string PublicId)> UploadImageAsync(byte[] fileBytes, string fileName, string folder, CancellationToken ct = default)
    {
        if (fileBytes.Length == 0) throw new ArgumentException("File is empty", nameof(fileBytes));
        if (fileBytes.Length > MaxFileSizeBytes) throw new ArgumentException($"File size exceeds {MaxFileSizeBytes / 1024 / 1024}MB limit", nameof(fileBytes));

        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        if (!AllowedExtensions.Contains(ext)) throw new ArgumentException($"File type '{ext}' not allowed", nameof(fileName));

        using var ms = new MemoryStream(fileBytes);
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(fileName, ms),
            Folder = $"anhome/{folder}",
            Transformation = new Transformation().Quality("auto").FetchFormat("auto"),
            UseFilename = true,
            UniqueFilename = true,
            Overwrite = false,
        };
        var result = await _cloudinary.UploadAsync(uploadParams, ct);
        if (result.Error != null) throw new InvalidOperationException($"Cloudinary upload failed: {result.Error.Message}");
        return (result.SecureUrl.ToString(), result.PublicId);
    }

    public async Task<bool> DeleteImageAsync(string publicId, CancellationToken ct = default)
    {
        var result = await _cloudinary.DestroyAsync(new DeletionParams(publicId));
        return result.Result == "ok";
    }
}
