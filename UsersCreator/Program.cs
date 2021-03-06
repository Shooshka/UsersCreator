﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.DirectoryServices.AccountManagement;
using System.Diagnostics;
using System.Reflection;
using System.IO;


namespace UsersCreator
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void CreateLocalWindowsAccount(string username, string password, string displayName, string description, bool canChangePwd, bool pwdExpires)
        {            
            {
                PrincipalContext context = new PrincipalContext(ContextType.Machine);
                UserPrincipal user = new UserPrincipal(context);
                user.SetPassword(password);
                user.DisplayName = displayName;
                user.Name = username;
                user.Description = description;
                user.UserCannotChangePassword = canChangePwd;
                user.PasswordNeverExpires = pwdExpires;
                user.Save();                

                GroupPrincipal group = GroupPrincipal.FindByIdentity(context, "Пользователи");
                group.Members.Add(user);
                group.Save();

                string exeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                Directory.SetCurrentDirectory(exeDir);
                Process process = new Process();
                process.StartInfo.FileName = "tscmd.exe";
                process.StartInfo.Arguments = $"localhost {user} TimeoutDisconnect 1";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
                process.WaitForExit();// Waits here for the process to exit.
            }
        }

        public static string generate()
        {
            string high = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string low = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";
            string password = "";

            Random rnd = new Random();
            password += high[rnd.Next(0, high.Length)];
            for (int i = 1; i < 7; i++)
            {
                password += low[rnd.Next(0, low.Length)];
            }

            password += numbers[rnd.Next(0, numbers.Length)];

            return password;
        }
    }
}
