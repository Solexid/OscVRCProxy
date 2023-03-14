using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Rug.Osc;

namespace OscVRCProxy
{
    internal class EndpointData
    {
       public string Ip { get; set; }
        public int Port { get; set; }
        public string ParamPath { get; set; }
        [JsonIgnore]
        public OscSender Sender { get; set; }
        public EndpointData() { }

    }
}
