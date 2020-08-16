using ContactAddress.Application.Features.Contacts.Queries;
using FluentValidation;

namespace ContactAddress.Application.Features.ContactFeatures.Queries.Validation
{
    public class GetContactByEmailQueryValidator : AbstractValidator<GetContactByEmail>
    {

        public GetContactByEmailQueryValidator()
        {
            RuleFor(x => x.EmailAddress)
                       .NotEmpty().WithMessage("Email address is required")
                       .EmailAddress().WithMessage("A valid email is required");
        }
    }
}
