namespace IssueLab.Blazor.Shared.Models;

public class UserViewModel
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public AccountStatus Status { get; set; }

}
