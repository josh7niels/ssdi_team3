using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Server
{
    public class Server
    {
        static void Main(string[] args)
        {
            string ip = GetLocalIPAddress();
            IPAddress ipAd = IPAddress.Parse(ip);
            //Initialize listener on given IP and port
            TcpListener myList = new TcpListener(ipAd, 8001);
            //Start Listening at the specified port
            myList.Start();
            Console.WriteLine("The server is running at port 8001...");
            Console.WriteLine("The local End point is :" + myList.LocalEndpoint);
            //Endless listening for the clients to connect
            while (true)
            {
                try
                {
                    Console.WriteLine("Waiting for a connection.....");

                    //Start waiting for client to connect and
                    //return a socket for communciation between server and client
                    Socket s = myList.AcceptSocket();
                    Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);
                    //Reading information from client
                    byte[] buffer = new byte[1000];
                    int k = s.Receive(buffer);
                    List<string> dataList = new List<string>();
                    dataList = message_decode(buffer);
                    for (int i = 0; i < dataList.Count; i++)
                        Console.WriteLine(dataList[i]);
                    MessageFactory factory = new MessageFactory(dataList);
                    IMessage myType = factory.GetMessageType();
                    dataList.Clear();
                    dataList = myType.execute();
                    byte[] r = message_format(dataList);
                    for (int i = 0; i < dataList.Count; i++)
                        Console.WriteLine(dataList[i]);
                    //Console.WriteLine(Encoding.UTF8.GetString(r));
                    s.Send(r);
                    //This is also nessesary part
                    //but for this case, when we have
                    //endless listening in a single-threaded
                    //application. So I just commented it.
                    /*s.Close();
                    myList.Stop();*/
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error..... " + e.StackTrace);
                }
            }
        }
        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
        private static List<string> message_decode(byte[] recieved)
        {
            List<string> data = new List<string>();
            string s1 = Encoding.UTF8.GetString(recieved);
            string s2 = s1.Split('~').First();
            data = s2.Split('^').ToList();
            return data;
        }
        private static byte[] message_format(List<string> data)
        {
            string fmMessage = data[0];
            for (int i = 1; i < data.Count; i++)
                fmMessage += "^" + data[i];
            fmMessage += "~";
            byte[] bMessage = Encoding.UTF8.GetBytes(fmMessage);
            return bMessage;
        }
    }
}