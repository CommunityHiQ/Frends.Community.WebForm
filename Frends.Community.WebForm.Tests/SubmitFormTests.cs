using System.IO;
using Frends.Community.WebForm;
using NUnit.Framework;

namespace Frends.Helen.Customs.Tests
{
    [TestFixture]
    public class SubmitFormTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public void Down()
        {
        }

        [Test]
        public void AssertThatFormSentWithFile()
        {
            var address = "http://localhost/testform";
            var fileLocation = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\Test_files\test_file.txt");
            var options = new SubmitForm.Options { Address = address };
            var formParams = new SubmitForm.FormParameters() { InputName = "\"id\"", Value = "12345" };
            var formFileParams = new SubmitForm.FormFileParameters() { InputName = "\"file\"", FileLocation = fileLocation };
            var parameters = new SubmitForm.Parameters {FileParameteres = new []{formFileParams}, FormParameters = new []{formParams}};

            var result = SubmitForm.SendForm(parameters,options);
        }
    }
}