using Homework_19.Data;
using Microsoft.EntityFrameworkCore;

namespace Homework_19.Domain;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Person> Person { get; set; }
    public DbSet<Address> Address { get; set; }
}