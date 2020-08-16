using ContactAddress.Application.Core.Commands;
using ContactAddress.Application.Features.Contacts.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ContactAddress.API.Tests
{
    [TestClass]
    public class WhenCreatingNewContact : TestBase
    {

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            httpClient.BaseAddress = new System.Uri("http://localhost:5000");
        }

        [TestMethod]
        public async Task Create_Valid_Input_Should_Create_Contact()
        {

            var contact = new CreateContactCommand()
            {
                FirstName = "Test",
                MiddleName = "J",
                LastName = "API",
                EmailAddress = "testc@api.org",
                PhoneNumber = "432-221-1223"
            };

            HttpResponseMessage responseMessage = await httpClient.PostAsync("contacts", new StringContent(JsonSerializer.Serialize(contact), Encoding.UTF8, "application/json"));
          
            Assert.AreEqual(HttpStatusCode.Created, responseMessage.StatusCode);
        }

        [TestMethod]
        public async Task Create_Empty_FirstName_Should_Error()
        {

            var contact = new CreateContactCommand()
            {
                MiddleName = "J",
                LastName = "API",
                EmailAddress = "testc@api.org",
                PhoneNumber = "432-221-1223"
            };

            HttpResponseMessage responseMessage = await httpClient.PostAsync("contacts", new StringContent(JsonSerializer.Serialize(contact), Encoding.UTF8, "application/json"));
           
            Assert.AreEqual(HttpStatusCode.BadRequest, responseMessage.StatusCode);
            var content = await responseMessage.Content.ReadAsStringAsync();
            var errors = JsonSerializer.Deserialize<List<ValidationError>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("'First Name' must not be empty.", errors[0].Message);
            Assert.AreEqual("FirstName", errors[0].PropertyName);
        }

        [TestMethod]
        public async Task Create_Invalid_Email_Should_Error()
        {

            var contact = new CreateContactCommand()
            {
                FirstName = "Test",
                MiddleName = "J",
                LastName = "API",
                EmailAddress = "testc.org",
                PhoneNumber = "432-221-1223"
            };

            HttpResponseMessage responseMessage = await httpClient.PostAsync("contacts", new StringContent(JsonSerializer.Serialize(contact), Encoding.UTF8, "application/json"));

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
