using ContactAddress.Application.Core;
using ContactAddress.Application.Core.Commands;
using ContactAddress.Application.Core.Models;
using MediatR;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;

namespace ContactAddress.Application.Features.Contacts.Commands
{
    public class CreateContactCommand : IRequest<BaseResponse<int>>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }
    }

    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, BaseResponse<int>>
    {
        private IRepository<Contact> _repository;
        public CreateContactCommandHandler(IRepository<Contact> repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<int>> Handle(CreateContactCommand createContactCommand, CancellationToken cancellationToken)
        {
            //TODO: consider automapper
            var contact = new Contact
            {
                FirstName = createContactCommand.FirstName,
                MiddleName = createContactCommand.MiddleName,
                LastName = createContactCommand.LastName,
                EmailAddress = createContactCommand.EmailAddress,
                PhoneNumber = createContactCommand.PhoneNumber,

            };

            return new BaseResponse<int>(await _repository.CreateAsync(contact));
        }
    }
}
