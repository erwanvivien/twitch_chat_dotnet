using System;
using System.Collections.Generic;
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
        private IRC irc = null;

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
            // IRCbot irc = new IRCbot(server, port, bot_name, password, channel);

            // irc.Start();

            for (int i = 0; i < 10; i++)
            {
                string tmp = this.irc.read();
                Console.WriteLine("read: '" + tmp + "'\n======");
                Thread.Sleep(2000);
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
