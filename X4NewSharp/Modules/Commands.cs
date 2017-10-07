using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace X4Sharp.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        //public int ka = -1;

        [Command("help")]
        public async Task Help()
        {
            EmbedBuilder e = new EmbedBuilder();

            e.Author = new EmbedAuthorBuilder().WithIconUrl("https://images-ext-2.discordapp.net/external/Z8pafsoqXGAm3HUrpgedv02zhz9FxHQwKjNJxfn9CYE/https/cdn.discordapp.com/icons/238345584652713984/3458909894ab833b363ab21a6846b01d.jpg?width=80&height=80").WithName("X4Bot");
            e.Color = new Color(0x661967);
            e.AddField("Bot Prefix", "x4");
            e.AddField("Help", "Shows this message!");
            e.AddField("Sayd", "Repeats your message! (Use Quotation Marks)");
            e.AddField("Lb", "A memorial to the best role in existence, **Layout Builder**");
            e.AddField("Spam", "Spams a message in #spam. Input: sentence, (number of times this has to be spammed)");
            e.AddField("Speed", "Check the ping of the bot");
            e.AddField("Ilikeameme", "...");
            e.AddField("submitrecord", "Submits a record from Geometry Dash! Input: username, levelname, percentage, videolink");
            e.AddField("viewrecords", "Views records of the players. Input: username");
            e.AddField("level", "Shows the info of a level! Input: levelID");
            e.AddField("pcspecs", "Shows PC specs of registered users. Contact Monstahhh to add your specs.");
            e.AddField("penis", "Measures Penis size [name]");
            e.AddField("thybe", "cool guy's face");
            e.AddField("jake", "Jake's face lol");
            e.AddField("sysinfo", "Shows info about the computer where X4Bot runs on.");
            Embed a = e.Build();
            await ReplyAsync("", embed: a);
        }
        [Command("sayd")]
        public async Task Sayd(string input)
        {
           await Context.Channel.SendMessageAsync(input);
           await Context.Message.DeleteAsync(); 
        }
        [Command("lb")]
        public async Task LB()
        {
            await Context.Channel.SendMessageAsync("Here lies the remains of our beloved\n\n**Layout Builder**");
        }
        [Command("spam")]
        public async Task LBSpam(string ja, int times)
        {
            if (Context.Channel.Name.Contains("spam"))
            {
                if (Context.User.Id == 203587486025383938)
                {
                    await Context.Channel.SendMessageAsync("no jake");
                    return;
                } else
                {
                    if (times >= 25)
                    {
                        await Context.Channel.SendMessageAsync("Please use an amount under 25.");
                        return;
                    }
                    if (times < 0)
                    {
                        await Context.Channel.SendMessageAsync("Attempting abuse in the bot is bannable. Server Admins will decide whether this will kick you.");
                        return;
                    }
                    
                    int jaja = 0;
                    while (jaja != times)
                    {
                        jaja++;
                        await Context.Channel.SendMessageAsync(ja);
                    }
                    
                }
            } else
            {
                await Context.Channel.SendMessageAsync("This command can only be executed in #spam");
            }
        }                   
        [Command("speed")]
        public async Task IsUp()
        {
            await Context.Channel.SendMessageAsync("am up boi! " + "Ping: " + Context.Client.Latency.ToString());
        }
        [Command("badjokes")]
        public async Task BadJokes ()
        {
            await Context.Channel.SendMessageAsync("no fuck off this isn't about gay german.");
        }
        [Command("selfpromote")]
        public async Task SelfPromote()
        {
            await Context.Channel.SendMessageAsync("https://play.google.com/store/apps/details?id=com.MonstahGames.FlyingRocket");
        }
        [Command("ilikeameme")]
        public async Task Ilikeameme()
        {
            await Context.Channel.SendMessageAsync("not always.......\n\nbut i do every now and then");
        }
        [Command("submitrecord")]
        public async Task SubmitRecord(string username, string levelname, int percentage, string video)
        {
            if (percentage > 101)
            {
                await Context.Channel.SendMessageAsync("You can't have a value higher than 100");
                return;
            }
            HttpClient client = new HttpClient();
                    var values = new Dictionary<string, string>
                    {
                        {"usernamePost", username},
                        {"levelnamePost", levelname},
                        {"percentagePost", percentage.ToString()},
                        {"videoLinkPost", video}
                    };
            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("http://sigmastudios.tk/X4Bot/submitrecord.php", content);

            var responseString = await response.Content.ReadAsStringAsync();
            await Context.Channel.SendMessageAsync("Record Submitted!");
        }
        [Command("viewrecords")]
        public async Task ViewRecords(string username)
        {
            await Context.Channel.SendMessageAsync("Getting Records...");

            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string>
            {
                {"usernamePost", username}
            };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("http://sigmastudios.tk/X4Bot/viewrecords.php", content);

            string responseString = await response.Content.ReadAsStringAsync();
            string[] levelsArray = responseString.Split(';');
            EmbedBuilder e = new EmbedBuilder();
            e.Author = new EmbedAuthorBuilder().WithIconUrl("https://images-ext-2.discordapp.net/external/Z8pafsoqXGAm3HUrpgedv02zhz9FxHQwKjNJxfn9CYE/https/cdn.discordapp.com/icons/238345584652713984/3458909894ab833b363ab21a6846b01d.jpg?width=80&height=80").WithName("X4Bot");
            e.Color = new Color(0x661967);
            foreach (string level in levelsArray)
            {
                if (level == "")
                    continue;
                

                string[] kys = level.Split(',');
                e.AddField("Level Name", kys[0]);
                e.AddField("Percentage", kys[1]);
                e.AddInlineField("Video Link", kys[2]);
                e.AddField("End of Info", "**------------------------------**");
               
            }
            Embed a = e.Build();
            await ReplyAsync("", embed: a);

        }
        [Command("updaterecord")]
        public async Task UpdateRecord(string username, string levelname, int percentage)
        {
            await Context.Channel.SendMessageAsync("Updating Record...");
            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string>
            {
                {"usernamePost", username},
                { "levelnamePost", levelname},
                {"percentagePost", percentage.ToString()}
            };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("http://sigmastudios.tk/X4Bot/updaterecord.php", content);
            var responseString = await response.Content.ReadAsStringAsync();
            await Context.Channel.SendMessageAsync(responseString);
        }
        [Command("level")]
        public async Task GetLevel(int levelID)
        {
            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string>
            {
                {"gameVersion", "21"},
                {"type", "0"},
                {"str", levelID.ToString()},
                {"diff", "-"},
                {"len", "-"},
                {"page", "0"},
                {"total", "0"},
                {"uncompleted", "0"},
                {"onlyCompleted", "0"},
                {"featured", "0"},
                {"original", "0"},
                {"twoPlayer", "0"},
                {"coins", "0"},
                {"epic", "0"},
                {"secret", "Wmfd2893gb7"}
            };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("http://www.boomlings.com/database/getGJLevels21.php", content);
            var responseString = await response.Content.ReadAsStringAsync();

            string[] stuff = responseString.Split('#');
            string[] authorinfo = stuff[1].Split(':');
            string[] levelinfo = stuff[0].Split('|');
            string[] level = levelinfo[0].Split(':');

            EmbedBuilder e = new EmbedBuilder();
            e.Author = new EmbedAuthorBuilder().WithIconUrl("https://images-ext-2.discordapp.net/external/Z8pafsoqXGAm3HUrpgedv02zhz9FxHQwKjNJxfn9CYE/https/cdn.discordapp.com/icons/238345584652713984/3458909894ab833b363ab21a6846b01d.jpg?width=80&height=80").WithName("X4Bot");
            e.Color = new Color(0x661967);
            e.AddField("Level Name", level[3]);
            e.AddField("Level ID", level[1]);
            e.AddField("Creator", authorinfo[1]);
            e.AddField("Difficulty", level[27]);
            e.AddField("Downloads", level[13]);
            e.AddField("Likes", level[19]);
            Embed a = e.Build();
            await ReplyAsync("", embed: a);
        }
        [Command("pcspecs")]
        public async Task PCSpecs(string name)
        {
            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string>
            {
                {"namePost", name}
            };
            FormUrlEncodedContent content = new FormUrlEncodedContent(values);
            HttpResponseMessage response = await client.PostAsync("http://sigmastudios.tk/X4Bot/getpcspecs.php", content);
            string responseString = await response.Content.ReadAsStringAsync();
            string[] ja = responseString.Split(',');

            EmbedBuilder e = new EmbedBuilder();
            e.Author = new EmbedAuthorBuilder().WithIconUrl("https://images-ext-2.discordapp.net/external/Z8pafsoqXGAm3HUrpgedv02zhz9FxHQwKjNJxfn9CYE/https/cdn.discordapp.com/icons/238345584652713984/3458909894ab833b363ab21a6846b01d.jpg?width=80&height=80").WithName("X4Bot");
            e.Color = new Color(0x661967);
            e.AddField("CPU", ja[0]);
            e.AddField("CPU Cooler", ja[1]);
            e.AddField("Motherboard", ja[2]);
            e.AddField("Memory", ja[3]);
            e.AddField("Storage", ja[4]);
            e.AddField("GPU", ja[5]);
            e.AddField("Case", ja[6]);
            e.AddField("PSU", ja[7]);
            Embed a = e.Build();
            await Context.Channel.SendMessageAsync("", embed: a);
        }
        [Command("penis")]
        public async Task PenisCMD(string user)
        {
            Random r = new Random();
            int longs = r.Next(0, 101);

            EmbedBuilder e = new EmbedBuilder();
            e.Author = new EmbedAuthorBuilder().WithIconUrl("https://images-ext-2.discordapp.net/external/Z8pafsoqXGAm3HUrpgedv02zhz9FxHQwKjNJxfn9CYE/https/cdn.discordapp.com/icons/238345584652713984/3458909894ab833b363ab21a6846b01d.jpg?width=80&height=80").WithName("X4Bot");
            e.Color = new Color(0x661967);
            e.AddField("Penis", user + "'s dick is " + longs + "cm long.");
            Embed a = e.Build();
            await Context.Channel.SendMessageAsync("", embed: a);
        }
        [Command("ja")]
        public async Task JaCMD()
        {
            await Context.Channel.SendMessageAsync("ja");
        }
        //[Command("frlb")]
        //public async Task FRLBCMD ()
        //{
        //    HttpClient client = new HttpClient();
        //    var packet = new Dictionary<string, string>
        //    {
        //    };
        //    var content = new FormUrlEncodedContent(packet);
        //    var response = await client.PostAsync("http://sigmastudios.tk/FlyingRocket/display.php", content);
        //    string responseString = await response.Content.ReadAsStringAsync();
        //
        //    string[] results = responseString.Split(';');
        //    foreach (string result in results)
        //    {
        //        if (result == "")
        //            continue;
        //        string[] kys = result.Split(';');
        //        EmbedBuilder e = new EmbedBuilder();
        //        e.Author = new EmbedAuthorBuilder().WithIconUrl("https://images-ext-2.discordapp.net/external/Z8pafsoqXGAm3HUrpgedv02zhz9FxHQwKjNJxfn9CYE/https/cdn.discordapp.com/icons/238345584652713984/3458909894ab833b363ab21a6846b01d.jpg?width=80&height=80").WithName("X4Bot");
        //        e.Color = new Color(0x661967);
        //        e.AddField("Username", kys[0]);
        //        e.AddField("Score", kys[1]);
        //        e.AddField("-", "--------------------");
        //        await Context.Channel.SendMessageAsync("", embed: e);
        //
        //    }
        //    ka = -1;
        //}
        [Command("jakeface")]
        public async Task JakeFaceCMD ()
        {
            await Context.Channel.SendFileAsync("jake.png");
            await Context.Channel.SendMessageAsync("lmfao");
        }
        [Command("thybe")]
        public async Task ThybeCMD ()
        {
            await Context.Channel.SendFileAsync("monsteh.png");
        }
        [Command("sysinfo")]
        public async Task SysInfoCMD ()
        {
            PerformanceCounter perfCPUCount = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
            PerformanceCounter perfMemCount = new PerformanceCounter("Memory", "Available MBytes");

            string memUsage = perfMemCount.NextValue().ToString() + "MB";

            EmbedBuilder e = new EmbedBuilder();
            e.Author = new EmbedAuthorBuilder().WithIconUrl("https://images-ext-2.discordapp.net/external/Z8pafsoqXGAm3HUrpgedv02zhz9FxHQwKjNJxfn9CYE/https/cdn.discordapp.com/icons/238345584652713984/3458909894ab833b363ab21a6846b01d.jpg?width=80&height=80").WithName("X4Bot");
            e.Color = new Color(0x661967);
            e.AddField("CPU Usage", $"{perfCPUCount.NextValue()}%");
            e.AddField("Memory Usage", memUsage);
            Embed a = e.Build();
            await ReplyAsync("", embed: a);

        }
    }
}
