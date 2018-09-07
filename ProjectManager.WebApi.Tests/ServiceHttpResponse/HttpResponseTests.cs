// -----------------------------------------------------------------------
// <copyright file="HttpResponseTests.cs" company="">
// Contains Service Http response test cases
// </copyright>
// -----------------------------------------------------------------------

namespace ProjectManager.WebApi.Tests.ServiceHttpResponse
{
    using NUnit.Framework;
    using System;
    using System.Configuration;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;

    /// <summary>
    /// Service Http Response tests
    /// </summary>
    public class HttpResponseTests
    {
        private HttpClient client;

        private HttpResponseMessage response;

        [SetUp]
        public void SetUP()
        {
            client = new HttpClient();
            
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["serviceBaseUri"]);
            response = client.GetAsync("User").Result;
        }

        [Test]
        public void GetResponseIsSuccess()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        [Test]
        public void GetResponseIsJson()
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);
        }

        [Test]
        public void GetAuthenticationStatus()
        {
            Assert.AreNotEqual(HttpStatusCode.Unauthorized, response.StatusCode);

        }
    }
}
