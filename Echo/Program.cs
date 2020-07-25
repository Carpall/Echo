using System;
using System.Collections.Generic;
using System.Linq;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Echo.Properties;

namespace Echo
{
    enum Encryptions
    {
        Rot13,
    }
    enum Channels
    {
        c0,
        c1,
        c2,
        c3,
        c4,
        c5,
    }
    class Package
    {
        /// Private
        private Encryptions _currenc = Encryptions.Rot13;

        // Public
        public string CurrentEncryption
        {
            get
            {
                return _currenc.ToString();
            }
            set
            {
                switch (Program.toCmd(value)) {

                    case "rot13":
                        _currenc = Encryptions.Rot13;
                        Program.encryptionSaved(Encryptions.Rot13);
                        break;
                    default:
                        Program.except("Bad syntax!", "Try 'ge' to get all avaiable encryptions.");
                        break;

                }
                Settings.Default.CurrentEncryption = CurrentEncryption;
                Settings.Default.Save();
            }
        }

        public string Info { get; set; } = "Echo build 1 - auth: Carpal repo: https://github.com/Carpall/Echo.git";

        // PublicInfo
        public Encryptions[] AvaiableEncryptions = { Encryptions.Rot13 };
        public Channels[] AvaiableChannels = { Channels.c0, Channels.c1, Channels.c2, Channels.c3, Channels.c4, Channels.c5 };
    }
    class Program
    {
        public static void encryptionSaved(Encryptions enc)
        {
            success($"Encryption saved as {enc.ToString()}");
        }
        public static void setColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
        public static void except(string err, string help)
        {
            setColor(ConsoleColor.DarkRed);
            println($"Except: {err}\nHelp: {help}");
            setColor(ConsoleColor.White);
        }
        public static void println(string arg)
        {
            Console.WriteLine(arg);
        }
        public static void print(string arg)
        {
            Console.Write(arg);
        }
        public static void info(string inf)
        {
            setColor(ConsoleColor.DarkBlue);
            Console.WriteLine(inf);
            setColor(ConsoleColor.White);
        }
        public static string readCmd()
        {
            return toCmd(Console.ReadLine());
        }
        public static string read()
        {
            return Console.ReadLine();
        }
        public static string toCmd(string arg)
        {
            return arg.Replace(" ", "").ToLower();
        }
        public static void success(string succ)
        {
            setColor(ConsoleColor.DarkGreen);
            println($"Success: {succ}");
            setColor(ConsoleColor.White);
        }
        public static void clear()
        {
            Console.Clear();
            setColor(ConsoleColor.White);
        }

        static void Main(string[] args)
        {
            // -$>
            Package Echo = new Package();
            Host Server = new Host();
            Echo.CurrentEncryption = Settings.Default.CurrentEncryption;
            Server.CurrentChannel = Settings.Default.CurrentChannel;
            clear();
            bool IsRun = true;
            info(Echo.Info);

            Server.Start();

            while (IsRun) {
                print("@> ");
                Server.ReceiveMessage();
                switch (readCmd()) {

                    case "help":
                        println("ref -> refresh 'echo' to get messages of currect channel");
                        println("send -> send a message on current channel");
                        println("sendfile/send file -> send a message contains a file on current channel");
                        println("res -> reset current chat history");
                        println("ce -> change encryption");
                        println("ge -> get all avaiables encryptions");
                        println("gce -> get current encryption");
                        println("cc -> change channel");
                        println("gc -> get all avaiables encryptions");
                        println("gcc -> get current encryption");
                        println("csl -> clear console");
                        println("exit -> exit from 'echo'");
                        println("help -> get all commands describetion");
                        break;
                    case "exit":
                        print("y/n\n|> ");
                        IsRun = (readCmd()[0] == 'y') ? false:true;
                        break;
                    case "res":
                        Server.Reset();
                        break;
                    case "cls":
                        clear();
                        break;
                    case "cc":
                        print("|> ");
                        Server.CurrentChannel = readCmd();
                        break;
                    case "gcc":
                        info($"Current channel: {Server.CurrentChannel}");
                        break;
                    case "gc":
                        for(int i=0;i<Echo.AvaiableChannels.Count();i++)
                            info(Echo.AvaiableChannels[i].ToString());
                        break;
                    case "send":
                        print("|> ");
                        Server.SendMessage($"[m]{read()}[nl]");
                        break;
                    case "ref":
                        setColor(ConsoleColor.DarkMagenta);
                        print("Messages:\n");
                        setColor(ConsoleColor.DarkYellow);
                        string[] separeMessages = Server.Messages.Replace("[nl]", "⁕").Split('⁕');
                        try {
                            foreach (string splittingPart in separeMessages)
                                println(splittingPart.Substring(splittingPart.IndexOf("[m]")));
                        } catch (ArgumentOutOfRangeException) { }
                        setColor(ConsoleColor.White);
                        break;
                    case "ge":
                        for (int i = 0; i < Echo.AvaiableEncryptions.Count(); i++) {
                            println($"{i}.{Echo.AvaiableEncryptions[i]}");
                        }
                        break;
                    case "gce":
                        info($"Current encryption method: {Echo.CurrentEncryption.ToString()}");
                        break;
                    case "ce":
                        print("|> ");
                        Echo.CurrentEncryption = readCmd();
                        break;
                }
            }
        }
    }
    class Host
    {
        public IFirebaseClient Client;
        public string[] pass = System.IO.File.ReadAllText(@"C:\Users\Mondelli\Documents\Visual Studio 2015\Projects\firebase.pass").Split('|');
        public void Start()
        {
            Client = new FirebaseClient(new FirebaseConfig {
                AuthSecret = pass[0],
                BasePath = pass[1],
            });
            if (Client == null) Program.except("Echo can not connect with main server.", "Check your connection!");
            else Program.success("Echo connected without error!");
        }
        // Public
        public string Messages = "";
        public string CurrentChannel
        {
            get {
                return _currcha.ToString();
            }
            set {
                switch (Program.toCmd(value)) {
                    case "c0": _currcha = Channels.c0; break;
                    case "c1": _currcha = Channels.c1; break;
                    case "c2": _currcha = Channels.c2; break;
                    case "c3": _currcha = Channels.c3; break;
                    case "c4": _currcha = Channels.c4; break;
                    case "c5": _currcha = Channels.c5; break;
                    default:Program.except($"'{value}' is not a valid channel.", "Type 'gc' to get all avaiable channels"); break;
                }
                Settings.Default.CurrentChannel = CurrentChannel;
                Settings.Default.Save();
            }
        }
        // Private
        private Channels _currcha = Channels.c0;
        public async void ReceiveMessage()
        {
            try {
                FirebaseResponse Response = await Client.GetTaskAsync(_currcha.ToString());
                Messages = Response.Body;
            } catch (Exception) {
                Program.except("Connection, to main server, failed.", "Check your connection!");
            }
        }
        public async void Reset()
        {
            Program.print("pass|> ");
            if (Program.read() == pass[0]) {
                ReceiveMessage();
                Program.success("Pass Correct!");
                try {
                    SetResponse response = await Client.SetTaskAsync(_currcha.ToString(), "");
                    Client.Set<string>(_currcha.ToString(), "");
                } catch (Exception) {
                    Program.except("Connection, to main server, failed.", "Check your connection!");
                }
            } else {
                Program.except("Pass Incorrect!", "Your probably aren't the Owner\nContact Carpal on github or discord.");
            }
        }
        public async void SendMessage(string message)
        {
            ReceiveMessage();
            try {
                FirebaseResponse response = await Client.PushTaskAsync(_currcha.ToString(), message);
                //Client.Push<string>(_currcha.ToString(), message);
            } catch (Exception) {
                Program.except("Connection, to main server, failed.", "Check your connection!");
            }
        }
    }
    class Encryption
    {
    }
}