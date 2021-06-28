using System.ComponentModel;
using System.Net.Http;
namespace Frends.Community.WebForm
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

}
