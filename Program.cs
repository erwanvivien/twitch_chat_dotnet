using System;
using System.Collections.Generic;
using System.IO;

namespace ChatBot
{
    class Program
    {
        static private bool good_settings(Dictionary<string, string> settings)
        {
            // Log already printed if null
            if (settings == null)
                return false;

            // Displays every unset variables
            foreach (var d in settings)
            {
                if (d.Value == null)
                {
                    Console.Error.WriteLine("'" + d.Key + "' was found not having a value");
                    return false;
                }
            }

            // No error
            return true;
        }

        static private bool update_settings(string line, int count, Dictionary<string, string> settings)
        {
            // Skips empty lines (tab/whitespaces included)
            if (string.IsNullOrWhiteSpace(line))
                return true;

            // Bad format (key: value)
            if (!line.Contains(":"))
            {
                Console.Error.WriteLine($"FILE: Line {count} has no semicolon");
                return false;
            }

            // Extracts (key, value)
            string[] elements = line.Split(":", 2, StringSplitOptions.RemoveEmptyEntries);
            string el1 = elements[0].Trim(), el2 = elements[1].Trim();

            // Wrong key
            if (!settings.ContainsKey(el1))
            {
                Console.Error.WriteLine($"FILE: No such '{el1}' in line {count} as property\n\nPossible:");
                foreach (var tmp in settings)
                    Console.Error.WriteLine("- " + tmp.Key);

                return false;
            }

            settings[el1] = el2;
            return true;
        }

        static private Dictionary<string, string> read_settings()
        {
            var settings = new Dictionary<string, string> {
                {"platform", "twitch" },
                {"channel", null},
                {"bot name", "EMU_DS"},
                {"server", "irc.chat.twitch.tv"},
                {"port", "6667"},
                {"password", null},
            };

            System.IO.StreamReader file =
                new System.IO.StreamReader("settings");

            int count = 1;
            string line = null;
            while ((line = file.ReadLine()) != null)
            {
                if (!update_settings(line, count, settings))
                    return null;
                count++;
            }

            return settings;
        }

        static private void launch(Dictionary<string, string> settings)
        {
            string platform = settings["platform"];
            if (platform == "twitch")
            {
                Twitch tmp = new Twitch(settings);
                tmp.start();
            }

            // else if (platform == "youtube")
            //     Youtube chat = new Chatbot();
            else
                Console.Error.WriteLine($"Platform '{platform}' was not found");
        }


        static void Main(string[] args)
        {
            if (!File.Exists("settings"))
            {
                Console.Error.WriteLine("No 'settings' file found.");
                return;
            }

            var settings = read_settings();
            if (!good_settings(settings))
                return;

            launch(settings);
        }
    }
}
