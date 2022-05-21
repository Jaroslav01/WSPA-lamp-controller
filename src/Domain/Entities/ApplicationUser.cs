namespace CleanArchitecture.Domain.Entities;


public class ApplicationUser : AuditableEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();

}
