using System;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using NUnit.Framework;
using Moq;
using Moq.Protected;

namespace Frends.Community.WebForm.Tests
{
  [TestFixture]
  public class SubmitFormTests
  {

    [Test]
    public void AssertThatHttpRequestIsCorrectWithFile()
    {

      var handlerMock = new Mock<HttpMessageHandler>();

      Uri actualHttpRequestUri = null;
      HttpMethod actualHttpMethod = null;
      string actualHttpRequestContent = null;

      handlerMock
         .Protected()
         .Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
         )
        .Callback<HttpRequestMessage, CancellationToken>((httpRequestMessage, cancellationToken) =>
            {
              actualHttpRequestUri = httpRequestMessage.RequestUri;
              actualHttpMethod = httpRequestMessage.Method;
              actualHttpRequestContent = httpRequestMessage.Content
                .ReadAsStringAsync()
                .GetAwaiter()
                .GetResult();
            }
        )
         // prepare the expected response of the mocked http call
         .ReturnsAsync(new HttpResponseMessage()
         {
           StatusCode = HttpStatusCode.OK,
         })
         .Verifiable();

      var address = "http://localhost/testform";
      var fileLocation = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\Test_files\test_file.txt");
      var options = new Options { Address = address };
      var formParams = new FormParameters() { InputName = "\"id\"", Value = "12345" };
      var formFileParams = new FormFileParameters() { InputName = "\"file\"", FileLocation = fileLocation };
      var parameters = new Parameters { FileParameteres = new[] { formFileParams }, FormParameters = new[] { formParams } };

      var result = SubmitForm.SendFormWithHandler(handlerMock.Object, parameters, address);

      Assert.AreEqual(HttpStatusCode.OK, result.Result.StatusCode);

      var expectedUri = new Uri(address);
      Assert.AreEqual(expectedUri, actualHttpRequestUri);

      Assert.AreEqual(HttpMethod.Post, actualHttpMethod);

      // Check that request content contains right values
      Assert.That(actualHttpRequestContent, Does.Contain("id"));
      Assert.That(actualHttpRequestContent, Does.Contain("12345"));
      Assert.That(actualHttpRequestContent, Does.Contain("test_file.txt"));
      Assert.That(actualHttpRequestContent, Does.Contain("Lorem lipsum"));
      Assert.That(actualHttpRequestContent, Does.Contain("Sumpil merol"));
    }

    public void AssertThatFormSentWithFile()
    {

      // ****************************************
      //
      // NOTE: Set your web form somewhere to actually test this!
      //
      // ****************************************

      var address = "http://localhost/testform";
      var fileLocation = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\Test_files\test_file.txt");
      var options = new Options { Address = address };
      var formParams = new FormParameters() { InputName = "\"id\"", Value = "12345" };
      var formFileParams = new FormFileParameters() { InputName = "\"file\"", FileLocation = fileLocation };
      var parameters = new Parameters { FileParameteres = new[] { formFileParams }, FormParameters = new[] { formParams } };

      var result = SubmitForm.SendForm(parameters, options);
    }
  }
}