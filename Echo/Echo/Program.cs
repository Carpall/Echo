using System;
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
        public Encryptions _currenc = Encryptions.Rot13;
        public string _username = Settings.Default.Username;
        // Public
        public string Username
        {
            get
            {
                return _username + ": ";
            }
            set
            {
                _username = value;
                Settings.Default.Username = value;
                Settings.Default.Save();
            }
        }
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
        public string PrivateChat { get; set; } = "Null";
        public string Version { get; set; } = "0.1";

        public string Info { get; set; } = "Echo build 1 - auth: Carpal repo: https://github.com/Carpall/Echo.git";
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
            Console.Title = "Echo";

            if (Server.IsRightVersion(Echo.Version)) {
                if (Settings.Default.Username == "") {
                    setColor(ConsoleColor.DarkMagenta);
                    print("Username:\n");
                    Echo.Username = read();
                    clear();
                }
                Echo.CurrentEncryption = Settings.Default.CurrentEncryption;
                clear();
                Server.CurrentChannel = Settings.Default.CurrentChannel;
                info("Echo Build 1 -> Carpal: https://github.com/Carpall/Echo");
                Server.Start();
                ProcessLoop();
            } else {
                warm("Your version is obsolete, downloading page is opening...");
                System.Diagnostics.Process.Start("https://github.com/Carpall/Echo/releases");
                Console.ReadKey();
            }
        }
        static Host Server = new Host();
        static Package Echo = new Package();
        static bool isRun { get; set; } = true;
        public static void ProcessLoop()
        {
            while (isRun) {
                Server.ReceiveMessage();
                setColor(ConsoleColor.DarkMagenta);
                print("@> ");
                switch (toCmd(Console.ReadLine())) {
                    case "help":
                        setColor(ConsoleColor.DarkBlue);
                        println("------------------Help------------------");
                        println("|- info    | get main info about you   |");
                        println("|- cls     | clear console             |");
                        println("|- setc    | change current channel    |");
                        println("|- setu    | change username           |");
                        println("|- sete    | change current encryption |");
                        println("|- getc    | get avaible channel       |");
                        println("|- connect | connect to private chat   |");
                        println("|- leave   | leave a private chat      |");
                        println("|- gete    | get avaible encryption    |");
                        println("|- sendm   | send messages             |");
                        println("|- sendf   | send files                |");
                        println("|- rec     | receive messages          |");
                        println("|- reset   | reset channel             |");
                        println("|- down    | donwload files            |");
                        println("|- exit    | exit from Echo            |");
                        println("|- help    | get all commands          |");
                        println("|- echo    | restart Echo              |");
                        println("----------------------------------------");
                        setColor(ConsoleColor.White);
                        break;
                    case "info":
                        setColor(ConsoleColor.DarkBlue);
                        println("------Info------");
                        println("|- Version: " + Echo.Version);
                        println("|- Current Channel: " + Server.CurrentChannel);
                        println("|- Private Chat: " + Echo.PrivateChat);
                        println("|- Current Encryption: " + Echo.CurrentEncryption);
                        println("|- Username: " + Echo._username);
                        println("----------------");
                        setColor(ConsoleColor.White);
                        break;
                    case "cls":
                        clear();
                        break;
                    case "connect":
                        setColor(ConsoleColor.DarkRed);
                        println("Chat ID & Password (note: id mustn't exists jet, please don't dislose it to everyone!)");
                        ask("Id:");
                        string id = read();
                        ask("Password:");
                        setColor(ConsoleColor.White);
                        Server.GenerateChatRoom(id, read());
                        Echo.PrivateChat = Server.Config.BasePath.Substring(Server.Config.BasePath.IndexOf("Messages/")+9);
                        break;
                    case "leave":
                        Server.CloseChatRoom();
                        success("Disconnected!");
                        Echo.PrivateChat = "Null";
                        break;
                    case "echo":
                        clear();
                        Main(null);
                        break;
                    case "setc":
                        ask("What channel? 'getc' to get aviable channels");
                        Server.CurrentChannel = readCmd();
                        break;
                    case "sete":
                        ask("What encryption? 'gete' to get aviable encryptions");
                        Echo.CurrentEncryption = readCmd();
                        break;
                    case "setu":
                        ask("New Username:");
                        Echo.Username = read();
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
                    case "exit":
                        isRun = false;
                        break;
                    case "sendm":
                        string mess = "";
                        while (true) {
                            string r = read();
                            if (string.IsNullOrWhiteSpace(r)) break;
                            else mess += "[m]"+ Echo.Username + Encryption.Encrypt(r, Echo._currenc) + '⚹';
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
                        }
                        warm("Loading...");
                        break;
                    case "reset":
                        ask("Database Access Token Request:");
                        Server.Reset();
                        break;
                    case "rec":
                        System.Text.StringBuilder m = new System.Text.StringBuilder();
                        string[] split = Server.Messages.Split('⚹');
                        try {
                            for (int i = 0; i < split.Length; i++) {
                                string s = split[i].Substring(split[i].IndexOf("[m]") + 3);
                                setColor(ConsoleColor.DarkGreen);
                                print(s.Substring(0, s.IndexOf(':') + 1));
                                setColor(ConsoleColor.DarkYellow);
                                println(Encryption.Decrypt(s.Substring(s.IndexOf(':') + 1), Echo._currenc));
                            }
                            setColor(ConsoleColor.Green);
                        } catch (ArgumentOutOfRangeException) { }
                        if (!string.IsNullOrWhiteSpace(m.ToString())) ask(" Messages:");
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
        public bool IsRightVersion(string Version)
        {
            Client = new FirebaseClient(new FirebaseConfig {
                AuthSecret = pass[0],
                BasePath = pass[1].Replace("Messages", "Version"),
            });
            if (Client.Get("").Body.Replace("\"", "") != Version) return false;
            return true;
        }
        public void Start()
        {
            Config = new FirebaseConfig {
                AuthSecret = pass[0],
                BasePath = pass[1],
            };
            Client = new FirebaseClient(Config);
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
        public IFirebaseConfig Config;
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
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(path + "/EchoFile")) {
                string f = Client.Get("Files").Body.Replace("\\r", "\r").Replace("\\n", "\n").Replace("\\t", "\t").Replace("\\a", "\a").Replace("\\v", "\v").Replace("\\b", "\b").Replace("\\f", "\f").Replace("\\\"", "\"");
                sw.Write(f.Substring(1, f.Count() - 2));
            }
            Program.success("Downloaded!");
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
        public void GenerateChatRoom(string id, string password)
        {
            Config = new FirebaseConfig {
                AuthSecret = pass[0],
                BasePath = $"{pass[1]}/Privates/{id}{password}/",
            };
            Client = new FirebaseClient(Config);
        }
        public void CloseChatRoom()
        {
            Config = new FirebaseConfig {
                AuthSecret = pass[0],
                BasePath = pass[1],
            };
            Client = new FirebaseClient(Config);
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