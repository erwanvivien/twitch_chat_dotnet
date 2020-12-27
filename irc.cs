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
            var reader = new StreamReader(stream);
            var writer = new StreamWriter(stream);

            List<string[]> lines = new List<string[]>();
            try
            {
                string inputLine = null;
                while ((inputLine = reader.ReadLine()) != null)
                {
                    // split the lines sent from the server by spaces (seems to be the easiest way to parse them)
                    string[] splitInput = inputLine.Split(new Char[] { ':' }, 3);
                    if (splitInput[0] == "PING")
                    {
                        string PongReply = splitInput[1];
                        writer.WriteLine("PONG " + PongReply);
                        writer.Flush();
                    }
                    else if (inputLine.Contains("PRIVMSG"))
                    {
                        string name = splitInput[1].Split("!")[0];
                        string content = splitInput[2];

                        lines.Add(new string[] { name, content });
                    }
                }

                return lines;
            }
            catch
            {
                return lines;
            }
        }

        public string[] get_message()
        {
            List<string[]> line = read();
            if (line == null)
                return null;

            return new string[] { "" };
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

                Console.WriteLine($"IRC: Connecting as {name}");
                writer.WriteLine($"USER {name} {name} {name} :EMU_BOT");
                writer.Flush();

                Console.WriteLine($"IRC: Authenticating");
                writer.WriteLine($"PASS {pass}");
                writer.Flush();

                Console.WriteLine($"IRC: NICK {name}");
                writer.WriteLine($"NICK {pass}");
                writer.Flush();

                // Might be needed if shit computer
                Thread.Sleep(1 * 1000);

                Console.WriteLine($"IRC: Joining {chann} channel");
                writer.WriteLine($"JOIN {chann}");
                writer.Flush();

                Console.WriteLine("===============================================\n\n");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("IRC: Could not connect: \n\n" + e);
            }
        }

        ~IRC()
        {
            Console.WriteLine("~IRC: Destroying stream");
            this.stream.Close();
            Console.WriteLine("~IRC: Destroying irc");
            this.irc.Close();
            Console.WriteLine("Done");
        }
    }
}