using AnHomes.Application.Dtos;

namespace AnHomes.Application.Interfaces;

public interface IContactService
{
    Task<ContactDto> CreateContactAsync(CreateContactRequest request, string? ipAddress = null, CancellationToken ct = default);
    Task<PagedResult<ContactDto>> GetAllContactsAsync(int page, int pageSize, string? status = null, CancellationToken ct = default);
    Task<ContactDto?> GetContactByIdAsync(Guid id, CancellationToken ct = default);
    Task<ContactDto?> UpdateContactStatusAsync(Guid id, string status, CancellationToken ct = default);
}
