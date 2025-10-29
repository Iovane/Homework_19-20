namespace Homework_19.Data;

public class User
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}

public static class Role
{
    public const string Admin = "Admin";
    public const string User = "User";
}