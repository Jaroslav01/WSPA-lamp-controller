namespace CleanArchitecture.Application.User.Queries.Dto;

public class ApplicationUserDto
{
    public Guid Id { get; set; }
    public string FirstNmae { get; set; }
    public string LastNmae { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}