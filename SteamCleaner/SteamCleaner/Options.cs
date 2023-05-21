using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SteamCleaner
{
    internal class Options
    {
        public void Option1()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            string defaultPath = @"C:\Program Files (x86)\Steam\";
            Console.WriteLine("Default = " + defaultPath);

            Console.Write("Enter Steam Folder Path: ");
            string path1 = Console.ReadLine();

            KillSteamProcess();
            DeleteDirectory(Path.Combine(path1, "userdata"));
            DeleteDirectory(Path.Combine(path1, "appcache"));
            DeleteDirectory(Path.Combine(path1, "dumps"));
            DeleteDirectory(Path.Combine(path1, "logs"));

            Directory.SetCurrentDirectory(path1);
            DeleteFiles("*.log");
            DeleteFiles("*.json");
            DeleteFiles("*.css");
            DeleteFiles("*.html");

            DeleteRegistryKey(@"HKEY_CURRENT_USER\SOFTWARE\Valve\Steam");
            Console.Clear();
            Console.WriteLine("Steam Cache Cleaned");
            Thread.Sleep(2500);
            Environment.Exit(1);
        }
        public void Option2()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            KillSteamProcess();
            string steamInstall = @"C:\Program Files (x86)\Steam"; // Default Steam installation folder
            string csgoFolder;
            Console.WriteLine("If CS:GO is inside SteamApps");
            Console.WriteLine("Press Enter To Continue");
            csgoFolder = Console.ReadLine();

            if (string.IsNullOrEmpty(csgoFolder))
            {
                csgoFolder = Path.Combine(steamInstall, @"SteamApps\common\Counter-Strike Global Offensive\csgo");
            }

            if (csgoFolder.EndsWith("\\"))
            {
                steamInstall = csgoFolder.Substring(0, csgoFolder.Length - 1);
            }

            string csgoCacheFolder = Path.Combine(csgoFolder, "cache");
            string cacheFolder = Path.Combine(steamInstall, "appcache");

            Directory.Delete(Path.Combine(steamInstall, "dumps"), true);
            Directory.Delete(Path.Combine(steamInstall, "logs"), true);

            if (Directory.Exists(csgoFolder))
            {
                Directory.Delete(csgoFolder, true);
            }
            if (Directory.Exists(cacheFolder))
            {
                Directory.Delete(cacheFolder, true);
            }

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Valve\\Steam", true))
            {
                key.DeleteSubKeyTree("");
            }

            Console.Clear();
            Console.WriteLine("Deleting CSGO cache");
            DeleteFiles(csgoCacheFolder);
            DeleteFiles(cacheFolder);

            Console.WriteLine("Deleting this shitty blob file");
            DeleteFiles(Path.Combine(steamInstall, "clientregistry.blob"));

            Console.WriteLine("Enabling Data Execution Prevention");
            RunCommand("bcdedit.exe", "/set {current} nx AlwaysOn");

            Console.WriteLine("Repairing Steam Service");
            Console.WriteLine(steamInstall);

            Console.WriteLine("Enabling Kernel Integrity");
            RunCommand("bcdedit.exe", "/deletevalue nointegritychecks");
            RunCommand("bcdedit.exe", "/deletevalue loadoptions");

            Console.WriteLine("Disabling Kernel Debug");
            RunCommand("bcdedit.exe", "/debug off");

            Console.WriteLine("Disabling UAC");
            RunCommand("reg.exe", "ADD HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System /v EnableLUA /t REG_DWORD /d 0 /f");

            Console.Clear();
            Console.WriteLine("CS:GO & Steam Successfully Cleaned");
            Thread.Sleep(2500);
            Environment.Exit(1);
        }

        public void Option3()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            KillSteamProcess();
            DeleteRegistryKey("HKEY_CURRENT_USER\\Software\\Valve\\Steam\\Users");
            DeleteRegistryKey("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Valve\\Users");
            DeleteRegistryKey("HKEY_LOCAL_MACHINE\\SOFTWARE\\Valve\\Users");
            DeleteRegistryKey("HKEY_CURRENT_USER\\Software\\Valve\\Steam");
            DeleteRegistryKey("HKEY_CURRENT_USER\\Software\\Valve");
            DeleteRegistryKey("HKEY_CURRENT_USER\\Software\\Wow6432Node\\Valve\\Steam");
            DeleteRegistryKey("HKEY_CURRENT_USER\\Software\\Wow6432Node\\Valve");
            DeleteRegistryKey("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Valve\\Users");
            DeleteRegistryKey("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Valve");
            DeleteRegistryKey("HKEY_LOCAL_MACHINE\\Software\\Valve\\Steam\\Users");
            DeleteRegistryKey("HKEY_LOCAL_MACHINE\\Software\\Valve");
            DeleteRegistryKey("HKEY_LOCAL_MACHINE\\SOFTWARE\\Classes\\steam");
            DeleteRegistryKey("HKEY_CURRENT_USER\\Software\\Classes\\steam");
            DeleteRegistryKey("HKEY_CLASSES_ROOT\\steam");

            RunCommand("cmd.exe", "/c cd\\");
            RunCommand("cmd.exe", "/c C:");

            string steamInstallFolder = @"C:\Program Files (x86)\Steam";
            RunCommand("cmd.exe", $"/c cd {steamInstallFolder}");

            DeleteFiles(@"C:\Program Files (x86)\Steam\appcache\Steam.log");
            DeleteDirectory(@"C:\Program Files (x86)\Steam\config");
            DeleteFiles(@"C:\Steam\appcache\AppUpdateStats.blob");
            DeleteFiles(@"C:\Steam\appcache\GameOverlayRenderer.log");
            DeleteFiles(@"C:\Steam\appcache\GameOverlayUI.exe.log");
            DeleteFiles(@"C:\Steam\appcache\crashhandler.dll");
            DeleteFiles(@"C:\Steam\appcache\crashhandler64.dll");
            DeleteFiles(@"C:\Steam\appcache\CSERHelper.dll");
            DeleteFiles(@"C:\Steam\appcache\Steam.dll");
            DeleteFiles(@"C:\Steam\appcache\steamclient.dll");
            DeleteFiles(@"C:\Steam\appcache\steamclient64.dll");
            DeleteFiles(@"C:\Steam\appcache\SteamUI.dll");
            DeleteFiles(@"C:\Steam\appcache\streaming_client.exe");
            DeleteFiles(@"C:\Steam\appcache\WriteMiniDump.exe");
            DeleteFiles(@"C:\Steam\appcache\debug.log");
            DeleteFiles(@"C:\Steam\*.mdmp");
            DeleteFiles(@"ClientRegistry.blob");
            DeleteDirectory(@"C:\Program Files (x86)\Steam\appcache");
            DeleteDirectory(@"C:\Program Files (x86)\Steam\config");
            DeleteDirectory(@"C:\Program Files (x86)\Steam\userdata");
            DeleteDirectory(@"C:\Program Files (x86)\Steam\dumps");
            DeleteDirectory(@"C:\Program Files (x86)\Steam\logs");
            DeleteDirectory(@"C:\Program Files (x86)\Steam\appcache");
            Console.Clear();
            Console.WriteLine("CS:GO & Steam Successfully Cleaned");
            Thread.Sleep(2500);
            Environment.Exit(1);
        }

        static void KillSteamProcess()
        {
            Process[] steamProcesses = Process.GetProcessesByName("steam");
            foreach (Process process in steamProcesses)
            {
                process.Kill();
            }
        }

        static void DeleteDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }

        static void DeleteFiles(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        static void DeleteRegistryKey(string keyPath)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath, true))
            {
                if (key != null)
                {
                    Registry.CurrentUser.DeleteSubKey(keyPath);
                }
            }
        }

        static void RunCommand(string command, string arguments)
        {
            Process process = new Process();
            process.StartInfo.FileName = command;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            process.WaitForExit();
        }
    }
}
