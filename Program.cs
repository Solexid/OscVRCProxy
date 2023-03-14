// See https://aka.ms/new-console-template for more information
using OscVRCProxy;

Console.WriteLine("Loading config file...");

var OSCService = new OSCService();
OSCService.StartReceive();
Console.ReadLine();
while (true) {

    OSCService.SendTest();
    Console.ReadLine();
}