using ContactAddress.Application.Core;
using ContactAddress.Application.Core.Commands;
using ContactAddress.Application.Core.Models;
using ContactAddress.Application.Features.ContactFeatures.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ContactAddress.Application.Features.Contacts.Queries
{

    public class GetContactByEmail : IRequest<BaseResponse<ContactDto>>
    {

        public GetContactByEmail(string emailAddress)
        {
            EmailAddress = emailAddress;
        }
        public string EmailAddress { get; set; }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetContactByEmail, BaseResponse<ContactDto>>
    {
        private readonly IRepository<Contact> _repository;
        public GetProductByIdQueryHandler(IRepository<Contact> repository)
        {
            _repository = repository;
        }

        //TODO: propagate cancellationtoken to the data access context
        public async Task<BaseResponse<ContactDto>> Handle(GetContactByEmail query, CancellationToken cancellationToken)
        {
            var contact = await _repository.GetByEmailAddressAync(query.EmailAddress);

            if (contact == null)
            {
                return default;
            }

            return new BaseResponse<ContactDto>(new ContactDto
            {
                ContactId = contact.Id,
                Name = $"{contact.FirstName} {contact.MiddleName} {contact.LastName}",
                EmailAddress = contact.EmailAddress,
                PhoneNumber = contact.PhoneNumber
            });
        }
    }
}
