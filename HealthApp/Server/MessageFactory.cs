﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class MessageFactory
    {
        List<string> message = new List<string>();
        public MessageFactory(List<string> recieved)
        {
            message = recieved;
        }
        public IMessage GetMessageType()
        {
            IMessage myMessage;
            switch (message[0])
            {
                case "01": //send username and password as two separate arguments to the validation method
                    myMessage = new LoginValidation(message[1], message[2]);
                    break;
                case "02": myMessage = new FindAppointments(message[1]);
                    break;
                case "03": myMessage = new DeleteAppointment(message[1]);
                    break;
                case "04": myMessage = new FindDoctors();
                    break;
                case "05": myMessage = new FindDates(message[1]);
                    break;
                case "06": myMessage = new FindTimes(message[1], message[2]);
                    break;
                case "07": Console.WriteLine("case 07 reached");
                    myMessage = new BookAppointment(message[1], message[2], message[3], message[4]);
                    break;
                default: myMessage = new LoginValidation("abc","123");
                    break;
            }
            return myMessage;
        }
    }
}