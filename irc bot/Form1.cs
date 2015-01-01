using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Newtonsoft.Json;
using osu_api;

namespace irc_bot
{
    public partial class Form1 : Form
    {
        public static string _lastMSG = "Currently not connected!", _channel = "#exo0tixz", _nowPlaying = "";
        private bool         _isConnected = false, _wasConnectedB = false, _gotTask = false, _apiThing = false;

        public static string _username;
         
        public ChatBot.ChatBot _bot;
        private irc_bot.BanchoChat _banchobot;

        public static bool _msgOSU = false;
        public static string _lastMsgOsu = "";

        public int _keyX = 0, _keyY = 0;

        public static int ppToday, ranksToday;

        public static double pp_raw = 0; public static int pp_rank = 0;
        GlobalKeyboardHook gHook;

        

        Process[] p;

        
        // // // // // // // // // // // 


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var info = System.Diagnostics.Process.GetProcessesByName("osu!").FirstOrDefault();

            gHook = new GlobalKeyboardHook(); // Create a new GlobalKeyboardHook
            // Declare a KeyDown Event
            gHook.KeyDown += new KeyEventHandler(gHook_KeyDown);
            // Add the keys you want to hook to the HookedKeys list
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                gHook.HookedKeys.Add(key);
            
        }

        public void gHook_KeyDown(object sender, KeyEventArgs e)
        {
            if(((char)e.KeyValue).ToString().ToLower() == "x")
            {
                _keyX++;
            }
            else if(((char)e.KeyValue).ToString().ToLower() == "y")
            {
                _keyY++;
            }
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            UpdateText();
            timer2.Enabled = true;
            timer1.Enabled = true;
            tmrApi.Enabled = true;
            
            if (!_isConnected)
            {
                
                try
                {
                    if (_wasConnectedB == false)
                    {       
                        _bot = new ChatBot.ChatBot();
                        _bot.start();
                    }
                    else
                    {  tmrStopbot.Enabled = true;}
                }
                catch
                {
                    MessageBox.Show("ERROR");
                }

                _lastMSG = "No messages since connection!";
                lblConnected.Text = "Connected"; lblConnected.ForeColor = Color.Green;
                cmdConnect.Text = "Disconnect";
                _isConnected = true;

                gHook.hook();
               

                //try
                //{
                    _banchobot = new irc_bot.BanchoChat();
                    _banchobot.start();
                //}
                //catch
                //{
                //    MessageBox.Show("Error during connection to banacho", "ERROR");
                //}
            }
            else
            {
                _bot.StopConnection();
            }
        }


        private void UpdateText()
        {
            _channel = "#" + txtChannel.Text;
        }
       


        private void UpdateLable()
        {
            _lblLastMSG.Text = "Last message: " + _lastMSG;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            UpdateLable();
        }

        public void cmdSend_Click(object sender, EventArgs e)
        {
            
        }

        private void tmrStopbot_Tick(object sender, EventArgs e)
        {
            _bot = new ChatBot.ChatBot();
            _bot.start();
            tmrStopbot.Enabled = false;
        }

        private void cmdConnectOsu_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            try
            {
                System.IO.File.WriteAllText("keyX.txt", "X: " + _keyX.ToString());
                System.IO.File.WriteAllText("keyY.txt", "Y: " + _keyY.ToString());
            }
            catch
            {

            }

            try
            {
                p = Process.GetProcessesByName("osu!");
                _gotTask = true;
                if (_gotTask == true)
                {
                    

                    if (p[0].MainWindowTitle.Length >= 28 && p != null) lblNP.Text = "Now Playing: " + p[0].MainWindowTitle.Substring(28);
                    else lblNP.Text = "Now Playing: ";
                    

                    
                    System.IO.File.WriteAllText("np.txt", "        Now Playing: " + p[0].MainWindowTitle.Substring(28));
                    
                    
                    
                }
                else
                {
                    
                    System.IO.File.WriteAllText("np.txt", "Now Playing: ");
                    
                    
                    lblNP.Text = "         Now Playing: ";
                }
            }
            catch
            {
                _gotTask = false;
            }

           if(_msgOSU == true)

               {
                   _banchobot.SendMessage("The user " + _username + " wants you to play [" + _lastMsgOsu + " This Map]");
                   _msgOSU = false;
               
               }

           _nowPlaying = lblNP.Text;
         }

        private void tmrApi_Tick(object sender, EventArgs e)
        {





            var api = new osuAPI(
            #region key
"ab9d55dd8c6ab9b4531f7d9e299cfa68ac70548d");
            #endregion
            var exo = api.GetUser("exo", Mode.osu);

            if (_apiThing == true)
            {
                if (exo.pp_raw != pp_raw)
                {
                    double pp = exo.pp_raw - pp_raw;
                    _bot.SendMessage("+" + (Math.Round(System.Convert.ToDouble(pp), 2).ToString() + "PP!"));
                }

                if (exo.pp_rank != pp_rank)
                {
                    if (exo.pp_rank > pp_rank)
                    {
                        int pp = pp_rank - exo.pp_rank;

                        _bot.SendMessage(Math.Abs(pp).ToString() + " ranks down!");
                    }
                    else if (exo.pp_rank < pp_rank)
                    {
                        int pp = exo.pp_rank - pp_rank;

                        _bot.SendMessage(Math.Abs(pp).ToString() + " ranks up!");
                    }
                }
            }
            else
            {
                ppToday = Convert.ToInt32(exo.pp_raw);
                ranksToday = exo.pp_rank;
                _apiThing = true;
            }
            pp_rank = exo.pp_rank;
            pp_raw = exo.pp_raw;
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(_isConnected == true)
            {
                _bot.StopConnection();
                _banchobot.StopConnection();
            }
            gHook.unhook();
            
        }




        
       
    }
}
