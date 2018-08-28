using System;
using System.Net;
using System.ComponentModel;
using System.IO;
using System.Net.Http;

namespace Frends.Community.WebForm
{    /// <summary>
     /// SubmitForm
     /// </summary>
    public static class SubmitForm
    {
        /// <summary>
        /// Fill and send data to web forms.
        /// </summary>
        /// <returns>Object {string FilePath }  </returns>
        public static Output SendForm(Parameters parameters, Options options)
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
