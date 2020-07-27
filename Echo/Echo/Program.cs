﻿using System;
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
        // Private
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
                        Program.except("Bad syntax!", "Try 'gete' to get all avaiable encryptions.");
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
            setColor(ConsoleColor.DarkMagenta);
            print("> ");
            setColor(ConsoleColor.White);
            return toCmd(Console.ReadLine());
        }
        public static string read()
        {
            setColor(ConsoleColor.DarkMagenta);
            print("> ");
            setColor(ConsoleColor.White);
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
        public static void warm(string wrm)
        {
            setColor(ConsoleColor.DarkYellow);
            println(wrm);
            setColor(ConsoleColor.White);
        }
        public static void clear()
        {
            Console.Clear();
            setColor(ConsoleColor.White);
        }
        public static void ask(string msg)
        {
            setColor(ConsoleColor.DarkMagenta);
            println(msg);
            setColor(ConsoleColor.White);
        }


        [STAThread]
        public static void Main(string[] args)
        {
            Echo.CurrentEncryption = Settings.Default.CurrentEncryption;
            clear();
            Server.CurrentChannel = Settings.Default.CurrentChannel;
            info("Echo Build 1 -> Carpal: https://github.com/Carpall/Echo");
            Server.Start();
            ProcessLoop();
        }
        static Host Server = new Host();
        static Package Echo = new Package();
        static bool isRun { get; set; } = true;
        public static void ProcessLoop()
        {
            while (isRun) {
                Server.ReceiveMessage();
                print("@> ");
                switch (toCmd(Console.ReadLine())) {
                    case "help":
                        setColor(ConsoleColor.DarkBlue);
                        println("------------------Help------------------");
                        setColor(ConsoleColor.DarkGreen);
                        println(" Current Channel: "+Server.CurrentChannel);
                        println(" Current Encryption: " + Echo.CurrentEncryption);
                        setColor(ConsoleColor.DarkBlue);
                        println("|- cls     | clear console             |");
                        println("|- setc    | change current channel    |");
                        println("|- sete    | change current encryption |");
                        println("|- getc    | get avaible channel       |");
                        println("|- gete    | get avaible encryption    |");
                        println("|- sendm   | send messages             |");
                        println("|- sendf   | send files                |");
                        println("|- rec     | receive messages          |");
                        println("|- reset   | reset channel             |");
                        println("|- down    | donwload files            |");
                        println("|- exit    | exit from Echo            |");
                        println("|- help    | get all commands          |");
                        println("----------------------------------------");
                        setColor(ConsoleColor.White);
                        break;
                    case "cls":
                        clear();
                        break;
                    case "setc":
                        ask("What channel? 'getc' to get aviable channels");
                        Server.CurrentChannel = readCmd();
                        break;
                    case "sete":
                        ask("What encryption? 'gete' to get aviable encryptions");
                        Echo.CurrentEncryption = readCmd();
                        break;
                    case "getc":
                        setColor(ConsoleColor.DarkBlue);
                        println("------Channels------");
                        println("|- c0 | Testing    |");
                        println("|- c1 | Testing    |");
                        println("|- c2 | Testing    |");
                        println("|- c3 | Testing    |");
                        println("|- c4 | Testing    |");
                        println("|- c5 | Testing    |");
                        println("--------------------");
                        setColor(ConsoleColor.White);
                        break;
                    case "gete":
                        setColor(ConsoleColor.DarkBlue);
                        println("------Encryptions------");
                        println("|- rot13 | Testing    |");
                        println("-----------------------");
                        setColor(ConsoleColor.White);
                        break;
                    case "sendm":
                        string mess = "";
                        while (true) {
                            string r = read();
                            if (string.IsNullOrWhiteSpace(r)) break;
                            mess += "[m]" + r + '⚹';
                        }
                        Server.SendMessage(mess);
                        break;
                    case "down":
                        System.Windows.Forms.FolderBrowserDialog x = new System.Windows.Forms.FolderBrowserDialog();
                        if (x.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                            Server.ReceiveFile(x.SelectedPath);
                        }
                        warm("Downloading...");
                        break;
                    case "sendf":
                        System.Windows.Forms.OpenFileDialog y = new System.Windows.Forms.OpenFileDialog();
                        if (y.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                            Server.SendFile(System.IO.File.ReadAllText(y.FileName));
                        } warm("Loading...");
                        break;
                    case "reset":
                        ask("Databse Access Token Request:");
                        Server.Reset();
                        break;
                    case "rec":
                        System.Text.StringBuilder m = new System.Text.StringBuilder();
                        string[] split = Server.Messages.Split('⚹');
                        try {
                            for (int i = 0; i < split.Length; i++) {
                                m.AppendLine(split[i].Substring(split[i].IndexOf("[m]")));
                            }
                        } catch (ArgumentOutOfRangeException) { }
                        warm(m.ToString());
                        break;
                    default:
                        warm("Bad command!");
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
        public string Files = "";
        public string CurrentChannel
        {
            get
            {
                return _currcha.ToString();
            }
            set
            {
                switch (Program.toCmd(value)) {
                    case "c0": _currcha = Channels.c0; break;
                    case "c1": _currcha = Channels.c1; break;
                    case "c2": _currcha = Channels.c2; break;
                    case "c3": _currcha = Channels.c3; break;
                    case "c4": _currcha = Channels.c4; break;
                    case "c5": _currcha = Channels.c5; break;
                    default: Program.except($"'{value}' is not a valid channel.", "Type 'getc' to get all avaiable channels"); break;
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
                Messages = (await Client.GetTaskAsync(_currcha.ToString())).Body;
            } catch (Exception) {
                Program.except("Connection, to main server, failed.", "Check your connection!\n");
            }
        }
        public void ReceiveFile(string path)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(path+"/EchoFile")) {
                string f = Client.Get("Files").Body.Replace("\\r", "\r").Replace("\\n", "\n").Replace("\\t", "\t").Replace("\\a", "\a").Replace("\\v", "\v").Replace("\\b", "\b").Replace("\\f", "\f").Replace("\\\"", "\"");
                sw.Write(f.Substring(1, f.Count() - 2));
            }Program.success("Downloaded!");
        }
        public void Reset()
        {
            if (Program.read() == pass[0]) {
                ReceiveMessage();
                Program.success("Pass Correct!");
                try {
                    Client.Set(_currcha.ToString(), "");
                    Program.success($"Reset {CurrentChannel}");
                } catch (Exception) {
                    Program.except("Connection, to main server, failed.", "Check your connection!");
                }
            } else {
                Program.except("Pass Incorrect!", "Your probably aren't the Owner\nContact Carpal on github or discord.");
            }
        }
        public async void SendMessage(string message)
        {
            try {
                await Client.PushTaskAsync(_currcha.ToString(), message);
            } catch (Exception) {
                Program.except("Connection, to main server, failed.", "Check your connection!");
            }
        }

        public void SendFile(string fileContent)
        {
            try {
                Client.Set("Files", fileContent);
            } catch (Exception) {
                Program.except("Connection, to main server, failed.", "Check your connection!");
            }
        }
    }
    class Encryption
    {
        public static string Encrypt(string toEn, Encryptions en)
        {
            switch (en) {
                case Encryptions.Rot13:
                    return Rot13.Encrypt(toEn);
                default:
                    return toEn;
            }
        }
        public static string Decrypt(string toDe, Encryptions en)
        {
            switch (en) {
                case Encryptions.Rot13:
                    return Rot13.Decrypt(toDe);
                default:
                    return toDe;
            }
        }
        static string alphabet = "abcdefghijklmnopqrstuvwxyz";
        public static class Rot13
        {
            public static string Encrypt(string text)
            {
                string result = "";
                for (int i = 0; i < text.Length; i++) {
                    if (!alphabet.Contains(text[i].ToString())) {
                        char chr = text[i].ToString().ToLower()[0];
                        if (alphabet.Contains(chr.ToString())) result += (alphabet.IndexOf(chr) + 13 >= 26) ? alphabet[13 - (26 - alphabet.IndexOf(chr))].ToString().ToUpper()[0] : alphabet[alphabet.IndexOf(chr) + 13].ToString().ToUpper()[0];
                        else result += text[i];
                    } else result += (alphabet.IndexOf(text[i]) + 13 >= 26) ? alphabet[13 - (26 - alphabet.IndexOf(text[i]))] : alphabet[alphabet.IndexOf(text[i]) + 13];
                }
                return result;
            }
            public static string Decrypt(string text)
            {
                string result = "";
                for (int i = 0; i < text.Length; i++) {
                    if (!alphabet.Contains(text[i].ToString())) {
                        char chr = text[i].ToString().ToLower()[0];
                        if (alphabet.Contains(chr.ToString())) result += (alphabet.IndexOf(chr) - 13 < 0) ? alphabet[13 + alphabet.IndexOf(chr)].ToString().ToUpper()[0] : alphabet[alphabet.IndexOf(chr) - 13].ToString().ToUpper()[0];
                        else result += text[i];
                    } else result += (alphabet.IndexOf(text[i]) - 13 < 0) ? alphabet[13 + alphabet.IndexOf(text[i])] : alphabet[alphabet.IndexOf(text[i]) - 13];
                }
                return result;
            }
        }
    }
}