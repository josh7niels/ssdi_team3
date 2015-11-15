using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Collections.Generic;

namespace HealthApp
{
    public class Core
    {
        public string ipAddress, username;
        public Core(IList<string> data)//constructor for storing session data
        {
            ipAddress = data[0];
            username = data[1];
        }
        public List<string> messageHandler(List<string> data)
        {
            //gets message from activity, calls functions to: format, send, and decodes, then returns recieved data to calling activity
            List<string> decoded = new List<string>();
            byte[] sending, recieved;
            sending = formatMessage(data);
            recieved = communicate(sending);
            decoded = decodeMessage(recieved);
            return decoded;
        }
        private List<string> decodeMessage(byte[] recieved)
        {
            //takes a byte array from the tcp socket and returns a string list of recieved data
            string s1, s2;
            List<string> decoded = new List<string>();
            s1 = Encoding.UTF8.GetString(recieved);
            s2 = s1.Split('~').First();
            decoded = s2.Split('^').ToList();
            return decoded;
        }
        private byte[] formatMessage(List<string> data)
        {
            //takes a list of data and concatenates into byte array to be sent
            string fmMessage = data[0];
            for (int i = 1; i < data.Count; i++)
                fmMessage += "^" + data[i];
            fmMessage += "~";
            byte[] bMessage = Encoding.UTF8.GetBytes(fmMessage);
            return bMessage;
        }
        private byte[] communicate(byte[] cMessage)
        {
            //establishes connection using session ip, sends data, recieves data, returns to message handler
            TcpClient tcpclnt = new TcpClient();
            byte[] recBuffer = new byte[2000];
            try
            {
                tcpclnt = new TcpClient();
                tcpclnt.Connect(ipAddress, 8001);
                Stream stm = tcpclnt.GetStream();
                stm.Write(cMessage, 0, cMessage.Length);
                stm.Flush();
                stm.Read(recBuffer, 0, recBuffer.Length);
                stm.Close();
                tcpclnt.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace + "\n");
            }
            return recBuffer;
        }
    }
}