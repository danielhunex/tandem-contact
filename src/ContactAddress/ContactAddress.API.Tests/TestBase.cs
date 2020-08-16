using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace ContactAddress.API.Tests
{
    [TestClass]
    public class TestBase
    {
        protected static readonly HttpClient httpClient = new HttpClient();
    }
}
