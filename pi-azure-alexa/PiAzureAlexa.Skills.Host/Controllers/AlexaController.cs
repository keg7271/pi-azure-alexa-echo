using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PiAzureAlexa.Skills.Host.Controllers
{
    using Speechlets;

    public class AlexaController : ApiController
    {
        [Route("alexa/pi-azure-alexa")]
        [HttpPost]
        public HttpResponseMessage ZoeSample()
        {
            var speechlet = new PiAzureAlexaSpeechlet();
            return speechlet.GetResponse(Request);
        }
    }
}
