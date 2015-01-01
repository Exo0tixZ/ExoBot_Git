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
using System.Net;
using Newtonsoft.Json;
using ChatterBotAPI;

namespace ChatBot
{
    public class ChatBot
    {
        ChatterBotFactory factory = new ChatterBotFactory();
       
        private Connection connection;
        string APIKey;


        private Random rnd = new Random();

        public ChatBot()
        {
            CreateConnection();

            connection.Listener.OnRegistered += new RegisteredEventHandler(OnRegistered);

            connection.Listener.OnPublic += new PublicMessageEventHandler(OnPublic);

            connection.Listener.OnPrivate += new PrivateMessageEventHandler(OnPrivate);

            connection.Listener.OnError += new ErrorMessageEventHandler(OnError);

            connection.Listener.OnDisconnected += new DisconnectedEventHandler(OnDisconnected);
        }

        private void CreateConnection()
        {
            string server = "irc.twitch.tv";

            string nick = "exothrall";

    




            ConnectionArgs cargs = new ConnectionArgs(nick, server) { Nick = nick, UserName = nick, ServerPassword = password };

            connection = new Connection(cargs, false, false);
        }

        public void start()
        {
            try
            {
                connection.Connect();
                

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
                
                connection.Sender.Join(irc_bot.Form1._channel.ToLower());
                MessageBox.Show("Joined channel " + irc_bot.Form1._channel);
            }
            catch(Exception e)
            {
                MessageBox.Show("Error in Onregistered(): " + e);
            }
        }

        public void StopConnection()
        {
           if(connection.Connected) connection.Disconnect("Bot stopped due to host pressing the \"Disconnect\" button");
        }

        public void OnPublic(UserInfo user, string channel, string message)
        {
            irc_bot.Form1._username = user.Nick;
            string[] _message = message.Split(new Char[] { ' ' });
            string _select = " ";
            //Command: "!rps" --> 'Rock, paper, Scissors'
            #region Rock, paper, scissors
            if (message.StartsWith("!rps") && message.Length < 14)
            {
                Random rnd = new Random();
                int     botSelect = rnd.Next(1, 4);
                string _botSelect= "";


                ////////////////////////////////
                if(botSelect == 1)
                {
                    _botSelect = "Rock";
                }
                else if(botSelect == 2)
                {
                    _botSelect = "Paper";
                }
                else if(botSelect == 3)
                {
                    _botSelect = "Scissors";
                }
                ////////////////////////////////




                ////////////////////////////////
                if (message != "!rps")
                {_select = _message[1]; }
                //
                if(_select == "Rock" || _select == "rock")
                {
                    if     (_botSelect == "Rock")
                    { connection.Sender.PublicMessage(channel, "You chose Rock and so did I! :)"); }

                    else if(_botSelect == "Paper")
                    { connection.Sender.PublicMessage(channel, "You chose Rock, but I killed it with my evil hugs ^.^"); }

                    else if(_botSelect == "Scissors")
                    { connection.Sender.PublicMessage(channel, "You chose Rock and smashed my scissors with it :("); }
                }

                else if(_select == "Paper" ||_select == "paper")
                {
                    if     (_botSelect == "Rock")
                    { connection.Sender.PublicMessage(channel, "You chose Paper and hugged my Rock to death :("); }

                    else if(_botSelect == "Paper")
                    { connection.Sender.PublicMessage(channel, "You chose Paper and so did I! :)"); }

                    else if(_botSelect == "Scissors")
                    { connection.Sender.PublicMessage(channel, "You chose Paper, but i cut it brutally with my Scissors ^.^"); }
                }
                
                else if(_select == "Scissors" || _select == "scissors" || _select == "scissor" ||_select == "Scissor")
                {
                    if(_botSelect == "Rock")
                    { connection.Sender.PublicMessage(channel, "You chose Scissors, but i smash them with my Rock "); }

                    else if(_botSelect == "Paper")
                    { connection.Sender.PublicMessage(channel, "You chose Scissors and cut my paper in half :(");  }

                    else if(_botSelect == "Scissors")
                    { connection.Sender.PublicMessage(channel, "You chose Scissors and so did I ^.^"); }
                }
                else
                {
                    connection.Sender.PublicMessage(channel, "Looks like you didn't chose either rock, paper or scissors! Please check you spelling.");
                }
                ////////////////////////////////

            }
            #endregion


            #region Commands
            #region Plain Text Commands
            //Command: "!setup" --> Tablet + Area
            else if(message == "!setup")
            {
                connection.Sender.PublicMessage(channel, "I'm using the Wacom Intuos Pen & Touch with this area: http://puu.sh/d2n96/036197d356.png !");
            }
            //Command: "!profile" --> Link to my profile
            else if (message == "!profile")
            {
                connection.Sender.PublicMessage(channel, "Here is the link to Exo's profile: https://osu.ppy.sh/u/3711889");
                
            }

            //Command: "!skin" --> Link to my skin
            else if(message == "!skin")
            {
                connection.Sender.PublicMessage(channel, "I'm using an edited version(http://puu.sh/dNA4k/dd5d636262.osk) of Slickcircles by Pannari: https://osu.ppy.sh/forum/t/184690 ");

            }

            //Command: "!keyboard" --> Tells the user what keyboard I am currently using
            else if(message == "!keyboard".ToLower())
            {
                connection.Sender.PublicMessage(channel, "CMStorm Trigger Z red");
            }       
            //Command: "!exothrall" --> Gives the user a basic information about who it is
            else if (message.ToLower() == "!exothrall")
            {
                connection.Sender.PublicMessage(channel, "Hey, I'm ExoThrall, and I'm a bot made by Exo0tixZ! Learn more about me in the description!");
            }
            #endregion

            //Command: "!np" --> Tells the user which song is playing right now
            else if(message == "!np")
            {
                if (irc_bot.Form1._nowPlaying == "Now Playing: ")
                {
                    connection.Sender.PublicMessage(channel, "No map is being played right now. If you see a map played on stream then wait a moment because the stream has a delay.");
                }
                else
                { connection.Sender.PublicMessage(channel, irc_bot.Form1._nowPlaying); }
            }



            // Tells the user how much ranks and PP the streamer gained since the bot has been started
            else if (message.ToLower() == "!today")
            {
                int RANKS = irc_bot.Form1.ranksToday - irc_bot.Form1.pp_rank;
                int PP    = Convert.ToInt32(irc_bot.Form1.pp_raw) - irc_bot.Form1.ppToday;

                if (!(irc_bot.Form1.ranksToday - irc_bot.Form1.pp_rank == 0) && !(Convert.ToInt32(irc_bot.Form1.pp_raw) - irc_bot.Form1.ppToday == 0))
                {
                    connection.Sender.PublicMessage(channel, "I got " + RANKS.ToString() + "( " + PP.ToString() + " pp) ranks today!" );
                }
                else
                {
                    connection.Sender.PublicMessage(channel, "No ranks today :(");
                }
            }
            
            // Lets the user roll a random generated number between 1 and 1000
            else if(message.ToLower() == "!roll")
            {
                connection.Sender.PublicMessage(channel, user.Nick + " rolled the number " + rnd.Next(1001).ToString());
            }

            // Tells the user his rank and when typed without suffix, the rank of the streamer, in this case "exo"
            else if(message.StartsWith("!rank".ToLower()))
            {
                if(message.ToLower() == "!rank")
                {
                    connection.Sender.PublicMessage(channel, getRank("!rank exo"));
                }

                connection.Sender.PublicMessage(channel, getRank(message));
            }
            

            // Lets the user talk with the bot via Cleverbot API
            else if(message.StartsWith("!t".ToLower()))
            {
                ChatterBot bot = factory.Create(ChatterBotType.CLEVERBOT);
                ChatterBotSession botsession = bot.CreateSession();

                connection.Sender.PublicMessage(channel, botsession.Think(message.Substring(3)));
            }

            #endregion


            #region Map requests
            //Osu map request.
            else if ( message.StartsWith("https://osu.ppy.sh/s") || message.StartsWith("https://osu.ppy.sh/b"))
            {

                
            

                if(message.StartsWith("https://osu.ppy.sh/s"))
                {
                    connection.Sender.PublicMessage(channel, getBeatmaps(message));
                }
                else if(message.StartsWith("https://osu.ppy.b/"))
                {
                    connection.Sender.PublicMessage(channel, getBeatmapb(message));
                }

                
                irc_bot.Form1._lastMsgOsu = message;
                irc_bot.Form1._msgOSU = true;
            }
            //Last Message label
            else
            {
                irc_bot.Form1._lastMSG = message;

            }
            #endregion


        }
        public string getBeatmapb(string message)
        {
            WebClient JSONFetch = new WebClient();

            string id = message.Substring(21);
            
            string result = "";


            string RawJSON;
            string Title;
            string Artist;
            string Difficulty;
            string Creator;
            string BPM;
            string Stars;

            RawJSON = JSONFetch.DownloadString("https://osu.ppy.sh/api/get_beatmaps?k=" + APIKey + "&b=" + id);

            

            Title      = ((dynamic)JsonConvert.DeserializeObject(RawJSON))[0].title;
            Artist     = ((dynamic)JsonConvert.DeserializeObject(RawJSON))[0].artist;
            Difficulty = ((dynamic)JsonConvert.DeserializeObject(RawJSON))[0].version;
            Creator    = ((dynamic)JsonConvert.DeserializeObject(RawJSON))[0].creator;
            BPM        = ((dynamic)JsonConvert.DeserializeObject(RawJSON))[0].bpm;
            Stars      = ((dynamic)JsonConvert.DeserializeObject(RawJSON))[0].difficultyrating;

            result = Artist + " - " + Title + "[" + Difficulty + "] (by" + Creator + "), " + BPM + "BPM, " + Stars + "stars";

            return result;
            
        }
        public string getBeatmaps(string message)
        {
            WebClient JSONFetch = new WebClient();

            string id = message.Substring(21);

            string result = "";


            string RawJSON;
            string Title;
            string Artist;
            string Difficulty;
            string Creator;
            string BPM;
            string Stars;

            RawJSON = JSONFetch.DownloadString("https://osu.ppy.sh/api/get_beatmaps?k=" + APIKey + "&s=" + id);



            Title = ((dynamic)JsonConvert.DeserializeObject(RawJSON))[0].title;
            Artist = ((dynamic)JsonConvert.DeserializeObject(RawJSON))[0].artist;
            Difficulty = ((dynamic)JsonConvert.DeserializeObject(RawJSON))[0].version;
            Creator = ((dynamic)JsonConvert.DeserializeObject(RawJSON))[0].creator;
            BPM = ((dynamic)JsonConvert.DeserializeObject(RawJSON))[0].bpm;
            Stars = ((dynamic)JsonConvert.DeserializeObject(RawJSON))[0].difficultyrating;

            result = Artist + " - " + Title + "[" + Difficulty + "] (by" + Creator + "), " + BPM + "BPM, " + Stars + "stars";

            return result;

        }
        public string getRank(string message)
        {
            try
            {
                WebClient JSONFetch = new WebClient();
               

                string RawJSON;
                string PP;
                string rank;
                string result;
                string username;

                

                RawJSON = JSONFetch.DownloadString("https://osu.ppy.sh/api/get_user?k=" + APIKey + "&u=" + message.Substring(6) + "&m=0&type=string");

                



                PP = ((dynamic)JsonConvert.DeserializeObject(RawJSON))[0].pp_raw;
                rank = ((dynamic)JsonConvert.DeserializeObject(RawJSON))[0].pp_rank;
                username = ((dynamic)JsonConvert.DeserializeObject(RawJSON))[0].username;


                result = username + " is rank " + rank + "! (" + PP + " pp!)";

                return result;
            }
            catch
            {
                return "Username not found.";
            }
        }

        public void OnPrivate(UserInfo user, string message)
        {


        }

        public void OnError(ReplyCode code, string message)
        {
            MessageBox.Show("An error of type" + code + "due to " + message + "has occured.");
        }
        public void OnDisconnected()
        {
            
            
            
        }
 
        
        public void SendMessage(string message)
        {
            connection.Sender.PublicMessage("#exo0tixz", message);
        }
    }
}