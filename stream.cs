using System;

namespace twitch_bot
{
    abstract class Stream
    {
        string password = null;
        string channel = null;
        string port = null;
        string server = null;
        string bot_name = "EMU_DS";

        public abstract void platform();
        public 

        public Stream(string channel, string password)
        {
            this.channel = channel;
            this.password = password;
        }
    }
}

