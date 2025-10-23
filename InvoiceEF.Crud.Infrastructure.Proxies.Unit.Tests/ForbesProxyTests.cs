using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Infrastructure.Proxies.Contracts;
using InvoiceEF.Crud.Infrastructure.Proxies.Dtos;
using InvoiceEF.Crud.Infrastructure.Proxies.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace InvoiceEF.Crud.Tests.Infrastructure.Proxies
{
    [TestClass]
    public class ForbesProxyTests
    {
        private IHttpClientFactory _httpClientFactory = null!;
        private ILogger<ForbesProxy> _logger = null!;

        [TestInitialize]
        public void Setup()
        {
            _httpClientFactory = Substitute.For<IHttpClientFactory>();
            _logger = Substitute.For<ILogger<ForbesProxy>>();
        }

        [TestMethod]
        public async Task GetListAsync_ShouldReturnList_WhenApiReturnsSuccess()
        {
            var data = new List<ForbesPersonDto> { new() { PersonName = "Elon Musk", Rank = 1 } };
            var json = JsonSerializer.Serialize(data);

            var httpClient = new HttpClient(new FakeHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json)
            }))
            {
                BaseAddress = new Uri("https://example.com/")
            };

            _httpClientFactory.CreateClient("ForbesProxy").Returns(httpClient);
            var _forbesProxy = new ForbesProxy(_httpClientFactory, _logger);

            var result = await _forbesProxy.GetListAsync(CancellationToken.None);

            Assert.IsFalse(result.HasErrors);
            Assert.AreEqual(1, result.Result?.Count());
            Assert.AreEqual("Elon Musk", result.Result?.First().PersonName);
        }

        [TestMethod]
        public async Task GetListAsync_ShouldReturnError_WhenApiReturnsFailure()
        {
            var httpClient = new HttpClient(new FakeHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.InternalServerError)))
            {
                BaseAddress = new Uri("https://example.com/")
            };

            _httpClientFactory.CreateClient("ForbesProxy").Returns(httpClient);
            var _forbesProxy = new ForbesProxy(_httpClientFactory, _logger);

            var result = await _forbesProxy.GetListAsync(CancellationToken.None);

            Assert.IsTrue(result.HasErrors);
            Assert.AreEqual(500, result.Errors[0].Code);
        }

        [TestMethod]
        public async Task GetListAsync_ShouldReturnError_WhenJsonIsInvalid()
        {
            var httpClient = new HttpClient(new FakeHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("INVALID JSON")
            }))
            {
                BaseAddress = new Uri("https://example.com/")
            };

            _httpClientFactory.CreateClient("ForbesProxy").Returns(httpClient);
            var _forbesProxy = new ForbesProxy(_httpClientFactory, _logger);

            var result = await _forbesProxy.GetListAsync(CancellationToken.None);

            Assert.IsTrue(result.HasErrors);
            Assert.AreEqual(1002, result.Errors[0].Code);
            Assert.IsNotNull(result.Exception);
        }

        [TestMethod]
        public async Task GetListAsync_ShouldReturnError_WhenHttpRequestExceptionOccurs()
        {
            // Arrange
            var httpClient = new HttpClient(new ThrowingHandler(new HttpRequestException("Network error")))
            {
                BaseAddress = new Uri("https://example.com/")
            };

            _httpClientFactory.CreateClient("ForbesProxy").Returns(httpClient);
            var forbesProxy = new ForbesProxy(_httpClientFactory, _logger);

            // Act
            var result = await forbesProxy.GetListAsync(CancellationToken.None);

            // Assert
            Assert.IsTrue(result.HasErrors);
            Assert.AreEqual(1003, result.Errors[0].Code);
            Assert.IsInstanceOfType(result.Exception, typeof(HttpRequestException));
        }



        [TestMethod]
        public async Task GetListAsync_ShouldReturnError_WhenTaskCanceledExceptionOccurs()
        {
            var cts = new CancellationTokenSource();
            cts.Cancel();

            var httpClient = new HttpClient(new ThrowingHandler(new TaskCanceledException("Canceled")))
            {
                BaseAddress = new Uri("https://example.com/")
            };

            _httpClientFactory.CreateClient("ForbesProxy").Returns(httpClient);
            var _forbesProxy = new ForbesProxy(_httpClientFactory, _logger);

            var result = await _forbesProxy.GetListAsync(cts.Token);

            Assert.IsTrue(result.HasErrors);
            Assert.AreEqual(1004, result.Errors[0].Code);
            Assert.IsInstanceOfType(result.Exception, typeof(TaskCanceledException));
        }

        // --- Helpers ---
        private class FakeHttpMessageHandler : HttpMessageHandler
        {
            private readonly HttpResponseMessage _response;
            public FakeHttpMessageHandler(HttpResponseMessage response) => _response = response;

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
                => Task.FromResult(_response);
        }

        private class ThrowingHandler : HttpMessageHandler
        {
            private readonly Exception _exception;
            public ThrowingHandler(Exception ex) => _exception = ex;

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
                => throw _exception;
        }
    }
}
