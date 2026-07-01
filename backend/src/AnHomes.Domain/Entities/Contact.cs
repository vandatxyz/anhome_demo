namespace AnHomes.Domain.Entities;

public class Contact
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string Need { get; set; } = string.Empty;
    public string? Budget { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Status { get; set; } = "new"; // new, in_progress, completed
    public string? IpAddress { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
