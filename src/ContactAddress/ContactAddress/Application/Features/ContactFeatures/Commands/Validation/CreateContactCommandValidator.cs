using ContactAddress.Application.Features.Contacts.Commands;
using FluentValidation;

namespace ContactAddress.Application.Features.ContactFeatures.Commands.Validation
{
    public class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
    {

        public CreateContactCommandValidator()
        {
            RuleFor(x => x.EmailAddress)
                      .NotEmpty().WithMessage("Email address is required")
                      .EmailAddress().WithMessage("A valid email is required");
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.MiddleName).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();

        }
    }
    public class DeleteContactByEmailAddressCommandValidator : AbstractValidator<DeleteContactByEmailAddressCommand>
    {
        public DeleteContactByEmailAddressCommandValidator()
        {
            RuleFor(x => x.EmailAddress)
                      .NotEmpty().WithMessage("Email address is required")
                      .EmailAddress().WithMessage("A valid email is required");

        }
    }
}
