using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace HealthApp
{
    class Core
    {
        public bool loginHandler(string username, string password)
        {
            byte[] sending, recieved;
            string[] loginInfo = new string[3];
            loginInfo[0] = "01";
            loginInfo[1] = username;
            loginInfo[2] = password;
            sending = formatMessage(loginInfo, 3);
            recieved = communicate(sending);
            string[] returnInfo = decodeMessage(recieved);
            if (returnInfo[1] == "1")
                return true;
            else
                return false;
        }
        private string[] decodeMessage(byte[] recieved)
        {
            string s1, s2;
            s1 = Encoding.UTF8.GetString(recieved);
            s2 = s1.Split('~').First();
            string[] splits = s2.Split('^');
            return splits;
        }
        private byte[] formatMessage(string[] userInput, int num)
        {
            string fmMessage = userInput[0];
            for (int i = 1; i < num; i++)
                fmMessage += "^" + userInput[i];
            fmMessage += "~";
            byte[] bMessage = Encoding.UTF8.GetBytes(fmMessage);
            return bMessage;
        }
        private byte[] communicate(byte[] cMessage)
        {
            TcpClient tcpclnt = new TcpClient();
            byte[] recBuffer = new byte[1000];
            try
            {
                tcpclnt = new TcpClient();
                tcpclnt.Connect("10.0.2.2", 8001);
                Console.WriteLine("Connected...");
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