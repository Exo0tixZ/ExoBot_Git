using Sharkbite.Irc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace irc_bot
{
    class BanchoChat
    {
        public static Connection bancho;

        private bool _mapRequested;

        private irc_bot.Form1 form_irc = new irc_bot.Form1();

        
        
        
        public BanchoChat()
        {
            CreateConnection();

            bancho.Listener.OnRegistered += new RegisteredEventHandler(OnRegistered);
            
            bancho.Listener.OnPublic += new PublicMessageEventHandler(OnPublic);
            
            bancho.Listener.OnPrivate += new PrivateMessageEventHandler(OnPrivate);
           
            bancho.Listener.OnError += new ErrorMessageEventHandler(OnError);
            
            bancho.Listener.OnDisconnected += new DisconnectedEventHandler(OnDisconnected);
            
        }

        private void CreateConnection()
        {
            string server = "irc.ppy.sh";

            string nick = "exo";

  




            ConnectionArgs cargs = new ConnectionArgs(nick, server) { Nick = nick, UserName = nick, ServerPassword = password };

            bancho = new Connection(cargs, false, false);
        }

        public void start()
        {
            try
            {
                bancho.Connect();
                

            }
            catch (Exception e)
            {
                MessageBox.Show("Could not connect!");
            }
        }


        public void OnRegistered()
        {
            try
            {
                
                bancho.Sender.Join("#announce");
                bancho.Sender.PublicMessage("#announce", "/query exo");
            }
            catch(Exception e)
            {
                MessageBox.Show("Error in Onregistered(): " + e);
            }
        }

        public void StopConnection()
        {
           if(bancho.Connected) bancho.Disconnect("Bot stopped due to host pressing the \"Disconnect\" button");
        }


        

        public void SendMessage(string message)
        {
            bancho.Sender.PrivateMessage("exo", message);
        }
        public void OnPublic(UserInfo user, string channel, string message)
        {

        }


        public void OnPrivate(UserInfo user, string message)
        {
            if (message.ToLower() == "hi")
            {
                bancho.Sender.PrivateMessage(user.Nick, "Hello, " + user.Nick);
            }

        }

        public void OnError(ReplyCode code, string message)
        {
            MessageBox.Show("An error of type" + code + "due to " + message + "has occured.");
        }
        public void OnDisconnected()
        {
            
            
            
        }
    
    }
}
