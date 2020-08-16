using ContactAddress.Application.Core.Commands;
using ContactAddress.Application.Features.ContactFeatures.Queries;
using ContactAddress.Application.Features.Contacts.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ContactAddress.API.Tests.ContactFeatures
{
    [TestClass]
    public class WhenGettingAContact : TestBase
    {
        private static string emailAddress = "something@yahoo.com";

        private static CreateContactCommand createRequestBody = new CreateContactCommand()
        {
            FirstName = "Test",
            MiddleName = "J",
            LastName = "API",
            EmailAddress = emailAddress,
            PhoneNumber = "432-221-1223"
        };

        [ClassInitialize]
        public static async Task ClassInitialize(TestContext testContext)
        {
            //TODO: delete after each test
            httpClient.BaseAddress = new System.Uri("http://localhost:5000");
            HttpResponseMessage responseMessage = await httpClient.PostAsync("contacts", new StringContent(JsonSerializer.Serialize(createRequestBody), Encoding.UTF8, "application/json"));
            Assert.AreEqual(HttpStatusCode.Created, responseMessage.StatusCode);
        }

        [ClassCleanup]
        public static async Task ClassCleanup()
        {
            httpClient.BaseAddress = new System.Uri("http://localhost:5000");
            HttpResponseMessage responseMessage = await httpClient.DeleteAsync($"contacts{emailAddress}");
            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);

        }

        [TestMethod]
        public async Task Get_Valid_Email_Should_Get_A_Contact()
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync($"contacts/{emailAddress}");

            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);

            var content = await responseMessage.Content.ReadAsStringAsync();

            var contact = JsonSerializer.Deserialize<ContactDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.AreEqual(createRequestBody.EmailAddress, contact.EmailAddress);
            Assert.AreEqual(createRequestBody.PhoneNumber, contact.PhoneNumber);
            Assert.AreEqual($"{createRequestBody.FirstName} {createRequestBody.MiddleName} {createRequestBody.LastName}", contact.Name);
        }


        [TestMethod]
        public async Task Get_InValid_Email_Should_Error()
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync($"contacts/invalidemail");

            Assert.AreEqual(HttpStatusCode.BadRequest, responseMessage.StatusCode);

            var content = await responseMessage.Content.ReadAsStringAsync();

            var errors = JsonSerializer.Deserialize<List<ValidationError>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("A valid email is required", errors[0].Message);
            Assert.AreEqual("EmailAddress", errors[0].PropertyName);
        }

    }
}
