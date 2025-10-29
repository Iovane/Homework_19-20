using Homework_19.Data;
using Homework_19.Domain;
using Homework_19.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Homework_19.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PersonController : Controller
{
    private readonly AppDbContext _context;
    
    public PersonController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpPost("add/respondent")]
    [EndpointDescription("Add new respondent info to the file")]
    public IActionResult AddRespondent(Person person)
    {
        var validator = new PersonValidator();
        var result = validator.Validate(person);
        
        if (!result.IsValid)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            return BadRequest(errors);
        }
        
        _context.Person.Add(person);
        _context.SaveChanges();
        
        return Ok(person);
    }
    
    [HttpGet("get/respondents")]
    [EndpointDescription("Get all respondents from the file")]
    public IActionResult GetRespondents()
    {
        var respondents = _context.Person
            .Include(p => p.Address)
            .ToList();
        return Ok(respondents);
    }

    [HttpGet("get")]
    [EndpointDescription("Get respondent by id")]
    public IActionResult GetRespondent([FromQuery]int id)
    {
        var respondent = _context.Person.Where(x => x.Id == id)
            .Include(p => p.Address)
            .FirstOrDefault();
        
        return Ok(respondent);
    }

    [HttpGet("get/salary/{salary:double}")]
    [EndpointDescription("Get respodents whose salary is greater than the given one")]
    public IActionResult GetFilteredRespondents(double salary)
    {
        var respondents = _context.Person.Where(x => x.Salary >= salary);
        return Ok(respondents);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("delete/respondent")]
    [EndpointDescription("Delete respondent by id")]
    public IActionResult DeleteRespondent([FromQuery]int id)
    {
        var respondent = _context.Person.Find(id);
        if (respondent == null) return NotFound("Respondent not found");
        
        _context.Person.Remove(respondent);
        _context.SaveChanges();
        
        return Ok("Respondent deleted");
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("update/respondent")]
    [EndpointDescription("Update respondents info by id")]
    public IActionResult UpdateRespondent([FromBody]Person updatedPerson, [FromQuery]int id)
    {
        var respondent = _context.Person
            .Include(p => p.Address)
            .FirstOrDefault(p => p.Id == id);

        if (respondent == null)
            return NotFound("Respondent not found");

        respondent.Firstname = updatedPerson.Firstname;
        respondent.Lastname = updatedPerson.Lastname;
        respondent.JobPosition = updatedPerson.JobPosition;
        respondent.Salary = updatedPerson.Salary;
        respondent.WorkExperience = updatedPerson.WorkExperience;
        respondent.CreateDate = updatedPerson.CreateDate;
        
        respondent.Address.Country = updatedPerson.Address.Country;
        respondent.Address.City = updatedPerson.Address.City;
        respondent.Address.HomeNumber = updatedPerson.Address.HomeNumber;
        _context.Entry(respondent.Address).State = EntityState.Modified;
        
        _context.Update(respondent);
        _context.SaveChanges();
        
        return Ok(respondent);
    }
}