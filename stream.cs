using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;


namespace ChatBot
{
    abstract class Bot
    {
        // Usefull for future platforms
        public abstract List<string[]> read();
        public abstract void start();
    }

    class Twitch : Bot
    {
        private string password = null;
        private string channel = null;
        private int port = 0;
        private string server = null;
        private string bot_name = "EMU_DS";

        private IRC irc = null;

        private delegate void myDelegate();

        private Dictionary<string, myDelegate> possibilities =
            new Dictionary<string, myDelegate>
            {
                /// Movements
                { "up", new myDelegate(Winapi.act_upward) },
                { "down", new myDelegate(Winapi.act_downward) },
                { "left", new myDelegate(Winapi.act_left) },
                { "right", new myDelegate(Winapi.act_right) },

                { "a", new myDelegate(Winapi.act_a) },
                { "b", new myDelegate(Winapi.act_b) },
                { "x", new myDelegate(Winapi.act_x) },
                { "y", new myDelegate(Winapi.act_y) },

                { "l1", new myDelegate(Winapi.act_l1) },
                { "l2", new myDelegate(Winapi.act_l2) },

                { "r1", new myDelegate(Winapi.act_r1) },
                { "r2", new myDelegate(Winapi.act_r2) },

                { "help", new myDelegate(Winapi.act_help) },

                { "void", new myDelegate(Winapi.act_nothing) },
            };

        // This should be exactly like above one with zeroes (set in `set`)
        private Dictionary<string, int> possibilities_count = new Dictionary<string, int>();

        public override List<string[]> read()
        {
            return irc.read();
        }

        private void set(string channel, string password, string server, string port, string bot_name)
        {
            int port_nb = Int32.Parse(port);

            this.channel = channel;
            this.password = password;
            this.server = server;
            this.port = port_nb;
            this.bot_name = bot_name;

            // Inits IRC payload
            this.irc = new IRC(server, port_nb, bot_name, password, channel);

            // Inits `possibilities_count` with same KEY
            possibilities.Keys.ToList().ForEach(x => possibilities_count.Add(x, 0));
        }

        public override void start()
        {
            double timer = 2;
            double now = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            while (true)
            {
                // Checks if time is ()
                double current = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                if (now / 1000 + timer <= current / 1000)
                {
                    double passed = current - now;
                    Console.WriteLine($"ACT: {passed} miliseconds have passed");

                    // Resets timer
                    now = current;

                    /// Will return the max value
                    var keyOfMaxValue = possibilities_count.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                    // Console.WriteLine(keyOfMaxValue);
                    possibilities_count.Keys.ToList().ForEach(x => possibilities_count[x] = 0);

                    // Executes max key function
                    possibilities[keyOfMaxValue]();
                }

                // Reads chat (calls irc.read())
                List<string[]> tmp = read();

                // Does nothing if error / empty list
                if (tmp == null || tmp.Count == 0)
                    continue;

                // Updates counts
                foreach (var e in tmp)
                {
                    string name = e[0], content = e[1];
                    Console.WriteLine($"<- {name}: '{content}'");
                    content = content.ToLower();

                    if (possibilities_count.ContainsKey(content))
                        possibilities_count[content]++;
                }
            }
        }

        // Constructors
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
