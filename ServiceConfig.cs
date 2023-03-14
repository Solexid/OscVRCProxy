using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OscVRCProxy
{
    internal class ServiceConfig
    {
        public List<EndpointData> EndpointData { get; set; }=new List<EndpointData> (){
            new EndpointData { Ip = "127.0.0.1", ParamPath = "/", Port = 9001 },
             new EndpointData { Ip = "192.168.1.186", ParamPath = "/HeadPat", Port = 228 }
        };
        public int RecvPort { get; set; } = 9005;
        public static ServiceConfig Instance { get; set; }
        public static ServiceConfig LoadConfig() {
            try { 

                Instance = JsonSerializer.Deserialize<ServiceConfig>(File.ReadAllText("config.json"));

            }catch(Exception ex) 


            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Config recreated, cause file not found or not correct.");
                Instance=new ServiceConfig();
                File.WriteAllText("config.json",JsonSerializer.Serialize(Instance));


            }
            return Instance;
        }
    }
}
