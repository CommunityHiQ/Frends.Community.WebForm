using System;
using System.Net;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using Frends.Tasks.Attributes;

namespace Frends.Community.WebForm
{    /// <summary>
     /// SubmitForm
     /// </summary>
    public static class SubmitForm
    {
        /// <summary>
        /// Form parameters
        /// </summary>
        public class FormParameters
        {
            /// <summary>
            /// Input name
            /// </summary>
            public string InputName { get; set; }
            /// <summary>
            /// Value
            /// </summary>
            public string Value { get; set; }
        }

        /// <summary>
        /// Form file parameters if files are sent
        /// </summary>
        public class FormFileParameters
        {
            /// <summary>
            /// Input name
            /// </summary>
            public string InputName { get; set; }
            /// <summary>
            /// File location
            /// </summary>
            public string FileLocation { get; set; }
        }

        /// <summary>
        /// Form parameters
        /// </summary>
        public class Parameters
        {
            /// <summary>
            /// List of form input parameters
            /// </summary>
            public FormParameters[] FormParameters { get; set; }
            /// <summary>
            /// List of form file input parameters
            /// </summary>
            public FormFileParameters[] FileParameteres { get; set; }

        }

        /// <summary>
        /// Options for the call
        /// </summary>
        public class Options
        {

            /// <summary>
            /// Web service address
            /// </summary>
            public string Address { get; set; }

            /// <summary>
            /// Username
            /// </summary>
            public string Username { get; set; }

            /// <summary>
            /// Password
            /// </summary>
            [PasswordPropertyText]
            public string Password { get; set; }
        }

        /// <summary>
        /// Return object
        /// </summary>
        public class Output
        {
            /// <summary>
            /// Request result
            /// </summary>
            public HttpResponseMessage Result { get; set; }
        }

        /// <summary>
        /// Send form data.
        /// </summary>
        /// <returns>Object {string FilePath }  </returns>
        public static Output SendForm([CustomDisplay(DisplayOption.Tab)]Parameters parameters, [CustomDisplay(DisplayOption.Tab)]Options options)
        {
            HttpResponseMessage result = null;
            var handler = new HttpClientHandler();

            if (!string.IsNullOrEmpty(options.Username) || !string.IsNullOrEmpty(options.Password))
            {
                var credentials = new NetworkCredential(options.Username, options.Password);
                handler = new HttpClientHandler { Credentials = credentials };
            }

            using (var httpClient = new System.Net.Http.HttpClient(handler))
            {
                var form = new MultipartFormDataContent();

                foreach (var param in parameters.FormParameters)
                {
                    form.Add(new StringContent(param.Value), param.InputName);
                }

                foreach (var param in parameters.FileParameteres)
                {
                    if (File.Exists(param.FileLocation))
                    {
                        form.Add(new ByteArrayContent(File.ReadAllBytes(param.FileLocation)), param.InputName, "\""+Path.GetFileName(param.FileLocation)+"\"");
                    }
                    else
                    {
                        throw new FileNotFoundException("Couldn't find file: " + param.FileLocation);
                    }
                }
                try
                {
                    result = httpClient.PostAsync(options.Address, form).Result;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }

            return new Output() { Result = result };
        }
    }
}
