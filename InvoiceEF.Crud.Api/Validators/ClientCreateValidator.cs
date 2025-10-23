using FluentValidation;
using InvoiceEF.Crud.Application.Dtos.Requests.Client;

public class ClientCreateDtoValidator : AbstractValidator<ClientCreateDto>
{
    public ClientCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

        RuleFor(x => x.Direction)
            .NotEmpty().WithMessage("Direction is required")
            .MaximumLength(200).WithMessage("Direction cannot exceed 200 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email must be valid");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone is required")
            .Matches(@"^\+?\d{7,15}$") // simple regex for phone numbers
            .WithMessage("Phone must be a valid number");
    }
}
