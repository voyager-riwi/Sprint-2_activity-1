using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint2.Models;

[Table("users")]
public class User
{
    public int Id { get; set; }  
    [Column("first_name")]
    public string FirstName { get; set; }
    [Column("last_name")]
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string? Cellphone { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    [Column("zipcode")]
    public string? ZipCode { get; set; }
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public string? Password { get; set; }
    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}

public class UserNameAndEmailDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}