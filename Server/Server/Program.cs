using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Server
{
    class Server
    {
        static void Main(string[] args)
        {
            //The IP adress of computer, where server is running
            IPAddress ipAd = IPAddress.Parse("127.0.0.1");
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
                    byte[] b = new byte[1000];
                    int k = s.Receive(b);
                    //this block added 10/20... created messageDecoder(), adding delimiter to end of string, and splitting
                    string s1 = Encoding.UTF8.GetString(b);
                    Console.WriteLine("Recieved...");
                    string s2 = s1.Split('~').First();
                    Console.WriteLine(s2);
                    string returnMessage;
                    Server myServ = new Server();
                    returnMessage = myServ.messageDecoder(s2);
                    returnMessage += '~';
                    byte[] r = Encoding.UTF8.GetBytes(returnMessage);
                    s.Send(r);
                    Console.WriteLine("sent: \"" + returnMessage + "\"");
                    //end block 10/20...
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
        private string messageDecoder(string message)
        {
            string[] splits = message.Split('^');
            string sendBack;
            string data;
            database myData = new database();
            switch (splits[0])
            {
                case "01": //send username and password as two separate arguments to the validation method
                    bool valid = myData.validate_Login(splits[1], splits[2]);
                    if (valid)
                        data = "1";
                    else
                        data = "2";
                    break;
                case "02":
                    data = "case '02'";
                    break;
                default:
                    data = "default";
                    break;
            }
            sendBack = splits[0] + "^" + data;
            return sendBack;
        }
    }
}