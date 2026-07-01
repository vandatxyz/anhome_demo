namespace AnHomes.Application.Interfaces;

public interface ICloudinaryService
{
    Task<(string Url, string PublicId)> UploadImageAsync(byte[] fileBytes, string fileName, string folder, CancellationToken ct = default);
    Task<bool> DeleteImageAsync(string publicId, CancellationToken ct = default);
}
