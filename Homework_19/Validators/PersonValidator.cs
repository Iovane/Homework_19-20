using FluentValidation;
using Homework_19.Data;

namespace Homework_19.Validators;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(p => p.CreateDate)
            .LessThan(DateTime.Today);
        
        RuleFor(p => p.Firstname)
            .NotEmpty()
            .MinimumLength(0)
            .MaximumLength(50);
            
        RuleFor(p => p.Lastname)
            .NotEmpty() 
            .MinimumLength(0)
            .MaximumLength(50);
            
        RuleFor(p => p.JobPosition)
            .NotEmpty()
            .MinimumLength(0)
            .MaximumLength(50);

        RuleFor(p => p.Salary)
            .InclusiveBetween(0, 10000);

        RuleFor(p => p.WorkExperience)
            .NotEmpty();
        
        RuleFor(p => p.Address.City)
            .NotEmpty()
            .WithMessage("City is required");
        
        RuleFor(p => p.Address.Country)
            .NotEmpty()
            .WithMessage("Country is required");
        
        RuleFor(p => p.Address.HomeNumber)
            .NotEmpty()
            .WithMessage("Home number is required");
    }
}