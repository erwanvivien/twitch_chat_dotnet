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

    // public class IRCbot
    // {
    //     // server to connect to (edit at will)
    //     private readonly string _server;
    //     // server port (6667 by default)
    //     private readonly int _port;
    //     // user information defined in RFC 2812 (IRC: Client Protocol) is sent to the IRC server
    //     private readonly string _user;

    //     // the bot's nickname
    //     private readonly string _pass;
    //     // channel to join
    //     private readonly string _channel;

    //     private readonly int _maxRetries;

    //     public IRCbot(string server, int port, string user, string pass, string channel, int maxRetries = 3)
    //     {
    //         _server = server;
    //         _port = port;
    //         _user = user;
    //         _pass = pass;
    //         _channel = channel;
    //         _maxRetries = maxRetries;
    //     }

    //     public void Start()
    //     {
    //         var retry = false;
    //         var retryCount = 0;

    //         do
    //         {
    //             try
    //             {
    //                 Console.WriteLine($"IRC: Connecting to '{_server}:{_port}'");

    //                 using (var irc = new TcpClient(_server, _port))
    //                 using (var stream = irc.GetStream())
    //                 using (var reader = new StreamReader(stream))
    //                 using (var writer = new StreamWriter(stream))
    //                 {
    //                     Console.WriteLine($"IRC: Authenticating as '{_user}'");

    //                     writer.WriteLine($"USER {_user} {_user} {_user} :EMU_BOT");
    //                     writer.Flush();

    //                     Console.WriteLine($"IRC: Authenticating password");
    //                     writer.WriteLine($"PASS {_pass}");
    //                     writer.Flush();

    //                     Console.WriteLine($"IRC: Changing nick");
    //                     writer.WriteLine($"NICK {_user}");
    //                     writer.Flush();

    //                     Console.WriteLine($"IRC: Connecting to '{_channel}'");
    //                     writer.WriteLine($"JOIN {_channel}");
    //                     writer.Flush();

    //                     Console.WriteLine((stream.CanRead ? "can read" : "can't read"));

    //                     while (true)
    //                     {
    //                         string inputLine = null;
    //                         while ((inputLine = reader.ReadLine()) != null)
    //                         {
    //                             Console.WriteLine("<- " + inputLine);

    //                             // split the lines sent from the server by spaces (seems to be the easiest way to parse them)
    //                             string[] splitInput = inputLine.Split(new Char[] { ':' });

    //                             if (splitInput[0] == "PING")
    //                             {
    //                                 string PongReply = splitInput[1];
    //                                 writer.WriteLine("PONG " + PongReply);
    //                                 writer.Flush();
    //                             }
    //                         }
    //                     }
    //                 }
    //             }
    //             catch (Exception e)
    //             {
    //                 // shows the exception, sleeps for a little while and then tries to establish a new connection to the IRC server
    //                 Console.WriteLine(e.ToString());
    //                 Thread.Sleep(5000);

    //                 retry = ++retryCount <= _maxRetries;
    //             }
    //         } while (retry);
    //     }
    // }
}