using ContactAddress.Application.Core;
using ContactAddress.Application.Core.Commands;
using ContactAddress.Application.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ContactAddress.Application.Features.ContactFeatures.Commands
{
    public class DeleteContactByEmailAddressCommand : IRequest<BaseResponse<Contact>>
    {
        public string EmailAddress { get; set; }
    }
    public class DeleteContactByEmailAddressCommandHandler : IRequestHandler<DeleteContactByEmailAddressCommand, BaseResponse<Contact>>
    {
        private IRepository<Contact> _repository;
        public DeleteContactByEmailAddressCommandHandler(IRepository<Contact> repository)
        {
            _repository = repository;
        }
        public async Task<BaseResponse<Contact>> Handle(DeleteContactByEmailAddressCommand request, CancellationToken cancellationToken)
        {
            var contact = await _repository.DeleteEmailAddressAync(request.EmailAddress);
            return new BaseResponse<Contact>(contact);
        }
    }
}
