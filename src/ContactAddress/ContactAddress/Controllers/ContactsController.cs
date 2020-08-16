using ContactAddress.Application.Features.ContactFeatures.Commands;
using ContactAddress.Application.Features.Contacts.Commands;
using ContactAddress.Application.Features.Contacts.Queries;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAddress.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : BaseController
    {

        [HttpGet("{emailAddress}")]
        public async Task<IActionResult> GetByEmailAddress(string emailAddress)
        {
            var response = await Mediator.Send(new GetContactByEmail(emailAddress));

            if (response == null)
            {
                return NotFound();
            }
            if (response.Errors != null && response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }
            return Ok(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateContactCommand command)
        {
            var response = await Mediator.Send(command);

            if (response.Errors != null && response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }
            return Created(Request.GetDisplayUrl(), response.Data);
        }

        [HttpDelete("{emailAddress}")]
        public async Task<IActionResult> Delete(string emailAddress)
        {
            var response = await Mediator.Send(new DeleteContactByEmailAddressCommand { EmailAddress = emailAddress });

            if (response.Errors != null && response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }
            return Ok(response.Data);
        }
    }
}
