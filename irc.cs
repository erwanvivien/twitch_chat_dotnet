using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatBot
{
    // Greatly inspired from https://codereview.stackexchange.com/questions/142653/simple-irc-bot-in-c
    class IRC
    {
        protected string pass = null;
        protected string chann = null;
        protected int port = 0;
        protected string server = null;
        protected string name = "EMU_DS";

        public TcpClient irc = null;
        public NetworkStream stream = null;

        public List<string[]> read()
        {
            // Do not close them (we always use the same ones anyway)
            var reader = new StreamReader(stream);
            var writer = new StreamWriter(stream);

            // All the lines - [0]: username - [1]: msg content
            List<string[]> lines = new List<string[]>();

            try
            {
                string inputLine = null;
                // Read many lines (if needed because many are spamming)
                while ((inputLine = reader.ReadLine()) != null)
                {
                    // lines from twitch look like this :
                    // :xiaojiba.tmi.twitch.tv 366 xiaojiba #xiaojiba :End of /NAMES list
                    // or
                    // PING :tmi.twitch.tv
                    // We split thanks to ':' and remove empty entries and then trim them
                    string[] splitInput = inputLine.Split(new Char[] { ':' }, 2,
                        StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                    Console.WriteLine(inputLine);

                    // If one message is a ping message we reply but do not add it to the message list
                    if (splitInput[0] == "PING")
                    {
                        string PongReply = splitInput[1];
                        // reply what was sent
                        writer.WriteLine("PONG " + PongReply);
                        writer.Flush();
                    }
                    else if (inputLine.Contains("PRIVMSG"))
                    {
                        // add the name / content to the list
                        string name = splitInput[0].Split("!")[0];
                        string content = splitInput[1];

                        lines.Add(new string[] { name, content });
                    }
                }

                return lines;
            }
            // Catches if we are at the end of the pipe.
            catch (System.IO.IOException)
            {
                return lines;
            }
            // If big errors (I don't knooow)
            // Should not happen
            catch (Exception e)
            {
                Console.Error.WriteLine("\n\n" + e.ToString());
                return null;
            }
        }

        public void send(string content, string username = null)
        {
            var writer = new StreamWriter(this.stream);
            if (username == null)
                writer.WriteLine(content);
            else
                writer.WriteLine($"PRIVMSG {username} :{content}");

            writer.Flush();
        }

        public IRC(string server, int port, string name, string pass, string chann)
        {
            this.chann = chann;
            this.pass = pass;
            this.server = server;
            this.port = port;
            this.name = name;

            try
            {
                Console.WriteLine($"IRC: Connecting to '{server}:{port}'");
                this.irc = new TcpClient(server, port);

                this.stream = this.irc.GetStream();

                // Sets timeout when reading
                this.stream.ReadTimeout = 5;
                var writer = new StreamWriter(this.stream);

                // Authenticating through IRC
                // USER, PASS, NICK and then JOIN (in this order), all mandatory
                Console.WriteLine($"IRC: Connecting as {name}");
                send($"USER {name} {name} {name} :EMU_BOT");

                Console.WriteLine($"IRC: Authenticating");
                send($"PASS {pass}");

                Console.WriteLine($"IRC: NICK {name}");
                send($"NICK {pass}");

                // Might be needed if shit computer
                Thread.Sleep(1 * 1000);

                Console.WriteLine($"IRC: Joining {chann} channel");
                send($"JOIN {chann}");

                Console.WriteLine("===============================================\n\n");
            }
            catch (Exception e)
            {
                // Should make the program crash but lazy
                Console.Error.WriteLine("IRC: Could not connect: \n\n" + e);
            }
        }

        ~IRC()
        {
            // Destructor if possible but there might be a problem of implementation on my side
            // TODO: Not working
            Console.WriteLine("~IRC: Destroying stream");
            this.stream.Close();
            Console.WriteLine("~IRC: Destroying irc");
            this.irc.Close();
            Console.WriteLine("Done");
        }
    }
}