using Homework_19.Data;
using Microsoft.EntityFrameworkCore;

namespace Homework_19.Domain;

public class PersonContext(DbContextOptions<PersonContext> options) : DbContext(options)
{
    public DbSet<Person> Person { get; set; }
    public DbSet<Address> Address { get; set; }
}