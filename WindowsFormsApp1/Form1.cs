using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Win32;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           // Class1.test();
           // Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("软件自启动");

         
            String LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
            MessageBox.Show(Environment.CurrentDirectory+"\n"+LogPath);

            if (File.Exists("log.txt"))
                MessageBox.Show("当前路径下存在log文件");
            else
                MessageBox.Show("当前路径下不存在log文件");
            //Class1.test();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AutoStart();
        }

        private void AutoStart(bool isAuto = true, bool showinfo = true)
        {
            try
            {
                if (isAuto == true)
                {
                    RegistryKey R_local = Registry.CurrentUser;//RegistryKey R_local = Registry.CurrentUser;
                    RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    R_run.SetValue("应用名称", Application.ExecutablePath);
                    R_run.Close();
                    R_local.Close();
                }
                else
                {
                    RegistryKey R_local = Registry.CurrentUser;//RegistryKey R_local = Registry.CurrentUser;
                    RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    R_run.DeleteValue("应用名称", false);
                    R_run.Close();
                    R_local.Close();
                }
            }


            // if (showinfo)
            //      MessageBox.Show("您需要管理员权限修改", "提示");
            // Console.WriteLine("您需要管理员权限修改");
            catch (Exception ex)
            {
                String LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
                if (!Directory.Exists(LogPath))
                    Directory.CreateDirectory(LogPath);
                if (!File.Exists(LogPath + "\\log.txt"))
                    File.Create(LogPath + "\\log.txt").Close();
                string fileName = LogPath + "\\log.txt";
                string content = DateTime.Now.ToLocalTime() + " 0001_" + "您需要管理员权限修改" + "\n" + ex.StackTrace + "\r\n";
                Logger(fileName, content);

            }

        }
        public static void  disPlay(string path)
        {

            MessageBox.Show(path);
        }
        public  void Logger(string fileName, string content)
        {

            using (StreamWriter sw = new StreamWriter(fileName, true))
            {
                sw.Write(content);
                sw.Close(); sw.Dispose();
            }
        }
    }
}
