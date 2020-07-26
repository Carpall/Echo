﻿using System;
using System.Linq;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using EchoGraphics.Properties;

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
        public static void except(string err, string help)
        {//creare per ogni tipo una info nella msbx
            println($"Except: {err}\nHelp: {help}");
        }
        public static void println(string arg)
        {
            System.Windows.Forms.MessageBox.Show(arg);
        }
        public static void info(string inf)
        {
            Console.WriteLine(inf);
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
            println($"Success: {succ}");
        }
        public static void warm(string wrm)
        {
            println(wrm);
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
                    default: Program.except($"'{value}' is not a valid channel.", "Type 'gc' to get all avaiable channels"); break;
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
                Program.except("Connection, to main server, failed.", "Check your connection!");
            }
        }
        public void ReceiveFile()
        {
            string username = Environment.UserName;
            try {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter($@"C:\Users\{username}\Downloads\echoFile")) {
                    string f = Client.Get("Files").Body.Replace("\\r", "\r").Replace("\\n", "\n").Replace("\\t", "\t").Replace("\\a", "\a").Replace("\\v", "\v").Replace("\\b", "\b").Replace("\\f", "\f").Replace("\\\"", "\"");
                    sw.Write(f.Substring(1, f.Count() - 2));
                }
            } catch (Exception) {
                Program.except("Connection, to main server, failed.", "Check your connection!");
            }
            System.Diagnostics.Process.Start($@"C:\Users\{username}\Downloads\echoFile");
        }
        public async void Reset()
        {
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