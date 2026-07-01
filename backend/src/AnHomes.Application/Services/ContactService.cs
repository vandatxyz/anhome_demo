using AnHomes.Application.Dtos;
using AnHomes.Application.Interfaces;
using AnHomes.Domain.Entities;

namespace AnHomes.Application.Services;

public class ContactService : IContactService
{
    private readonly IUnitOfWork _uow;

    public ContactService(IUnitOfWork uow) => _uow = uow;

    public async Task<ContactDto> CreateContactAsync(CreateContactRequest request, string? ipAddress = null, CancellationToken ct = default)
    {
        var contact = new Contact
        {
            FullName = request.FullName,
            Phone = request.Phone,
            Email = request.Email,
            Need = request.Need,
            Budget = request.Budget,
            Message = request.Message,
            IpAddress = ipAddress
        };

        await _uow.Contacts.AddAsync(contact, ct);
        await _uow.SaveChangesAsync(ct);

        return MapToDto(contact);
    }

    public async Task<PagedResult<ContactDto>> GetAllContactsAsync(int page, int pageSize, string? status = null, CancellationToken ct = default)
    {
        var all = await _uow.Contacts.GetAllAsync(ct);
        var query = all.AsQueryable();
        if (!string.IsNullOrEmpty(status)) query = query.Where(c => c.Status == status);
        var total = query.Count();
        var items = query.OrderByDescending(c => c.CreatedAt).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return new PagedResult<ContactDto> { Data = items.Select(MapToDto).ToList(), Total = total, Page = page, PageSize = pageSize };
    }

    public async Task<ContactDto?> GetContactByIdAsync(Guid id, CancellationToken ct = default)
    {
        var contact = await _uow.Contacts.GetByIdAsync(id, ct);
        return contact == null ? null : MapToDto(contact);
    }

    public async Task<ContactDto?> UpdateContactStatusAsync(Guid id, string status, CancellationToken ct = default)
    {
        var contact = await _uow.Contacts.GetByIdAsync(id, ct);
        if (contact == null) return null;
        contact.Status = status;
        contact.UpdatedAt = DateTime.UtcNow;
        _uow.Contacts.Update(contact);
        await _uow.SaveChangesAsync(ct);
        return MapToDto(contact);
    }

    private static ContactDto MapToDto(Contact c) => new()
    {
        Id = c.Id,
        FullName = c.FullName,
        Phone = c.Phone,
        Email = c.Email,
        Need = c.Need,
        Budget = c.Budget,
        Message = c.Message,
        Status = c.Status,
        CreatedAt = c.CreatedAt
    };
}
