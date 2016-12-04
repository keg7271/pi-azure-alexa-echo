using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiAzureAlexa.Skills.Host.Models
{
    public class MyNumber
    {
        public string id { get; set; }

        public string DeviceId { get; set; }

        public int SomeValue { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}