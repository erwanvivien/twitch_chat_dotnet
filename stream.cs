using System;
using System.Collections.Generic;


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


        public Bot() { }
    }

    class Twitch : Bot
    {
        private IRC irc = null;

        private void set(string channel, string password, string server, string port, string bot_name)
        {
            int port_nb = Int32.Parse(port);

            this.channel = channel;
            this.password = password;
            this.server = server;
            this.port = port_nb;
            this.bot_name = bot_name;

            irc = new IRC(server, port_nb, bot_name, password, channel);
        }

        public override void platform()
        {
            Console.WriteLine("test");
        }

        public Twitch(string channel, string password)
        {
            set(channel, password, "irc.chat.twitch.tv", "6667", "EMU_DS");
        }

        public Twitch(string channel, string password, string server)
        {
            set(channel, password, server, "6667", "EMU_DS");
        }

        public Twitch(string channel, string password, string server, string port)
        {
            set(channel, password, server, port, "EMU_DS");
        }

        public Twitch(string channel, string password, string server, string port, string bot_name)
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
