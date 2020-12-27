using System;
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

        public string read()
        {
            var reader = new StreamReader(stream);
            var writer = new StreamWriter(stream);

            try
            {
                StringBuilder sb = new StringBuilder();

                // return reader.ReadToEnd();

                string inputLine = null;
                while ((inputLine = reader.ReadLine()) != null)
                {
                    Console.WriteLine("<- " + inputLine);

                    // split the lines sent from the server by spaces (seems to be the easiest way to parse them)
                    string[] splitInput = inputLine.Split(new Char[] { ':' });

                    if (splitInput[0] == "PING")
                    {
                        string PongReply = splitInput[1];
                        writer.WriteLine("PONG " + PongReply);
                        writer.Flush();
                    }

                    sb.Append(inputLine + "\n");
                }
                return sb.ToString();
            }
            catch
            {
                return null;
            }
        }

        // public string[] get_message()
        // {
        //     string line = read();
        //     if (line == null)
        //         return null;

        //     return new string[] { "" };
        // }

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
                // Thread.Sleep(2 * 1000);

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