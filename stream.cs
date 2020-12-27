using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;


namespace ChatBot
{

    abstract class Bot
    {
        protected string password = null;
        protected string channel = null;
        protected int port = 0;
        protected string server = null;
        protected string bot_name = "EMU_DS";

        public abstract void platform();
    }


    class Twitch : Bot
    {
        static public void upward()
        {
            Console.WriteLine("upward");
        }
        static public void downward()
        {
            Console.WriteLine("downward");
        }
        static public void left()
        {
            Console.WriteLine("left");
        }
        static public void right()
        {
            Console.WriteLine("right");
        }

        private IRC irc = null;

        private delegate void myDelegate();

        private Dictionary<string, myDelegate> possibilities =
            new Dictionary<string, myDelegate>
            {
                { "upward", new myDelegate(upward) },
                { "downward", new myDelegate(downward) },
                { "left", new myDelegate(left) },
                { "right", new myDelegate(right) },
            };


        private void set(string channel, string password, string server, string port, string bot_name)
        {
            int port_nb = Int32.Parse(port);

            this.channel = channel;
            this.password = password;
            this.server = server;
            this.port = port_nb;
            this.bot_name = bot_name;

            this.irc = new IRC(server, port_nb, bot_name, password, channel);
        }

        public override void platform()
        {
            while (true)
            {
                List<string[]> tmp = irc.read();
                if (tmp == null || tmp.Count == 0)
                    continue;

                foreach (var e in tmp)
                {
                    string name = e[0], content = e[1];
                    Console.WriteLine($"<- {name}: '{content}'");
                    if (possibilities.ContainsKey(content))
                        possibilities[content]();
                }
            }
        }

        public Twitch(string channel, string password, string server = "irc.chat.twitch.tv",
            string port = "6667", string bot_name = "EMU_DS")
        {
            set(channel, password, server, port, bot_name);
        }

        public Twitch(Dictionary<string, string> settings)
        {
            set(settings["channel"], settings["password"],
                settings["server"], settings["port"],
                settings["bot name"]);
        }
    }
}
