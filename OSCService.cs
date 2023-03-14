using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Rug.Osc;

namespace OscVRCProxy
{
    internal class OSCService
    {
        OscReceiver receiver;
        Thread thread;
        public OSCService()
        {
          var conf=  ServiceConfig.LoadConfig();

            conf.EndpointData.ForEach(item => { item.Sender = new OscSender(IPAddress.Parse(item.Ip), 0, item.Port);
                item.Sender.Connect();
                Console.WriteLine(item.Ip + ":" + item.Port + " " + item.ParamPath + "  Started...");
            }); ;
           

        }
        public void StartReceive()
        {
            


            if (receiver != null)
                receiver.Close();

            receiver = new OscReceiver(ServiceConfig.Instance.RecvPort);
            
            thread = new Thread(new ThreadStart(ListenLoop));
            Console.WriteLine("Recieving on port : "+ ServiceConfig.Instance.RecvPort);

            receiver.Connect();
            thread.Start();


        }
        public void SendTest() {


        }
        void ListenLoop()
        {
            try
            {
                while (receiver.State != OscSocketState.Closed)
                {
                    // if we are in a state to recieve
                    if (receiver.State == OscSocketState.Connected)
                    {

                        OscMessage packet =(OscMessage) receiver.Receive();
                        var text = packet.ToString();
                        foreach (var item in ServiceConfig.Instance.EndpointData)
                        {
                            if (text.Contains(item.ParamPath)) {
                                item.Sender.Send(packet);
                            
                            }


                        }
                       // MessageReceived?.Invoke(packet);
                        Console.WriteLine(packet.ToString());
                       
                    }
                }
            }
            catch (Exception ex)
            {
              
                if (receiver.State == OscSocketState.Connected)
                {
                    Console.WriteLine("Exception in listen loop");
                    Console.WriteLine(ex.Message);
                }
            }
        }
  
        public async Task<bool> SendIntAsync(string path, int data,OscSender oscSender)
        {

            if (oscSender != null)
            {

                await Task.Run(() => { oscSender.Send(new OscMessage(path, data)); });

            }
            return true;
        }
        public async Task<bool> SendBoolAsync(string path, bool data, OscSender oscSender)
        {

            if (oscSender != null)
            {

                await Task.Run(() => { oscSender.Send(new OscMessage(path, data)); });

            }
            return true;
        }

        public async Task<bool> SendFloatAsync(string path, float data,OscSender oscSender)
        {
            if (oscSender != null)
            {

                await Task.Run(() => { oscSender.Send(new OscMessage(path, data)); });

            }
            return true;
        }



    }
}
