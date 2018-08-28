using System.IO;
using NUnit.Framework;

namespace Frends.Community.WebForm.Tests
{
    [TestFixture]
    public class SubmitFormTests
    {
        public void AssertThatFormSentWithFile()
        {

            // ****************************************
            //
            // NOTE: Set your web form somewhere to actually test this!
            //
            // ****************************************

            var address = "http://localhost/testform";
            var fileLocation = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\Test_files\test_file.txt");
            var options = new Options { Address = address };
            var formParams = new FormParameters() { InputName = "\"id\"", Value = "12345" };
            var formFileParams = new FormFileParameters() { InputName = "\"file\"", FileLocation = fileLocation };
            var parameters = new Parameters {FileParameteres = new []{formFileParams}, FormParameters = new []{formParams}};

            var result = SubmitForm.SendForm(parameters,options);
        }
    }
}