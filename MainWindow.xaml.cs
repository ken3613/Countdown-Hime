using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.IO;
using Microsoft.Win32;

namespace Desktopwpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// 

    public class winapi
    {
        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
    }

    public partial class MainWindow : Window
    {

        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_TOOLWINDOW = 0x00000080;

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd,int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd,int nIndex);



        //工具类
        private System.ComponentModel.IContainer components = null;
        private NotifyIcon notifyicon1;
        private ContextMenuStrip menu1;
        private ToolStripMenuItem 关于toolmenu;
        private ToolStripMenuItem 退出toolmenu;
        private ToolStripMenuItem sizemenu;
        private ToolStripMenuItem showmenu;

        //实用类
        time timewindow;
        bool timershow;
        string path;
        int year;

        RegistryKey localreg = Registry.CurrentUser;
        string keystr;
        public bool onstart;

        public MainWindow()
        {

            if (DateTime.Now.Month > 6)
                year = DateTime.Now.Year + 1;
            else if (DateTime.Now.Month < 6)
                year = DateTime.Now.Year;
            else if ((DateTime.Now.Month == 6) && (DateTime.Now.Day < 7))
                year = DateTime.Now.Year;
            else year = DateTime.Now.Year + 1;




            path = System.Windows.Forms.Application.StartupPath + "\\settings.ini";
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            if (!File.Exists(path))
            {
                File.Create(path).Close();
                winapi.WritePrivateProfileString("size", "sizerate", "1.0", path);
                winapi.WritePrivateProfileString("timer", "timershow", "1", path);
            }
            InitializeComponent();

            //设定位置
            int swidth, sheight;
            swidth = (int)SystemParameters.PrimaryScreenWidth;
            sheight = (int)SystemParameters.PrimaryScreenHeight;
            this.Left = swidth - 240;
            this.Top = 10;

            this.menu1 = new ContextMenuStrip(this.components);
            this.退出toolmenu = new ToolStripMenuItem();
            this.关于toolmenu = new ToolStripMenuItem();
            this.sizemenu = new ToolStripMenuItem();
            this.showmenu = new ToolStripMenuItem();
            this.notifyicon1 = new NotifyIcon(this.components);

            //toolmenuitem
            this.关于toolmenu.Text = "关于";
            this.关于toolmenu.Name = "关于toolmenu";
            this.关于toolmenu.Click += new EventHandler(this.关于_Click);
            this.退出toolmenu.Text = "退出";
            this.退出toolmenu.Name = "退出toolmenu";
            this.退出toolmenu.Click += new EventHandler(this.退出_Click);
            this.sizemenu.Text = "设置";
            this.sizemenu.Name = "sizemenu";
            this.sizemenu.Click += new EventHandler(this.size);
            this.showmenu.Text = "关闭倒计时";
            this.showmenu.Name = "showmenu";
            this.showmenu.Click += new EventHandler(this.showclick);

            //menu1
            this.menu1.Items.AddRange(new ToolStripItem[]{
                this.showmenu,
                this.sizemenu,
                this.关于toolmenu,
                this.退出toolmenu});
            this.menu1.Name = "menu1";

            this.notifyicon1.Icon = Properties.Resources.ico;
            this.notifyicon1.Text = "倒计时~姬~";
            this.notifyicon1.ContextMenuStrip = this.menu1;
            this.notifyicon1.Visible = true;


            //设置数字
            DateTime n_date = new DateTime();
            DateTime exam = new DateTime(year, 6, 7, 0, 0, 0);
            TimeSpan c_date;
            n_date = DateTime.Today;
            c_date = exam - n_date;
            numlabel.Content = c_date.Days.ToString();
            timewindow = new time(this,year);
            timewindow.Show();
           

            
        }










        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.notifyicon1.Dispose();
        }

        private void 退出_Click(object sender, EventArgs e)
        {
            this.notifyicon1.Dispose();
            System.Environment.Exit(0);
        }

        private void 关于_Click(object sender, EventArgs e)
        {
            about gy=new about();
            gy.Show();
        }

        private void size(object sender, EventArgs e)
        {
            Window1 colorwindow = new Window1(this,timewindow);
            colorwindow.Show();
        }

        private void showclick(object sender, EventArgs e)
        {
            if (timershow)
            {
                timershow = false;
                this.showmenu.Text = "打开倒计时";
                this.timewindow.Visibility = System.Windows.Visibility.Collapsed;
                winapi.WritePrivateProfileString("timer", "timershow", "0", path);
            }
            else
            {
                timershow = true;
                this.showmenu.Text = "关闭倒计时";
                this.timewindow.Visibility = System.Windows.Visibility.Visible;
                winapi.WritePrivateProfileString("timer", "timershow", "1", path);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowInteropHelper wndHelper = new WindowInteropHelper(this);

            int exStyle = (int)GetWindowLong(wndHelper.Handle, (int)-20);

            exStyle |= (int)0x00000080;
            SetWindowLong(wndHelper.Handle, (int)-20, (IntPtr)exStyle);

            //设置桌面倒计时是否显示
            StringBuilder isshow = new StringBuilder(500);
            winapi.GetPrivateProfileString("timer","timershow","",isshow,500,System.Windows.Forms.Application.StartupPath+"\\settings.ini");
            if(isshow.ToString()=="1")
            {
                this.timershow = true;
                this.showmenu.Text="关闭倒计时";
                timewindow.Visibility = System.Windows.Visibility.Visible;
            }else
            {
                timershow = false;
                showmenu.Text="打开倒计时";
                timewindow.Visibility = System.Windows.Visibility.Collapsed;
            }



            //开机自动启动
            keystr = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
            RegistryKey key = localreg.OpenSubKey(keystr, true);
            string exepath;
            exepath = "\"" + System.Windows.Forms.Application.StartupPath + "\\DeskTop.exe\"";
            if ((key.GetValue("DeskTop") == null) || (key.GetValue("DeskTop").ToString() != exepath))
            {
                if (System.Windows.MessageBox.Show("是否设置开机自动启动？", "开机启动", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
                {
                    key.SetValue("DeskTop", exepath, RegistryValueKind.String);
                    this.onstart = true;
                }
                else this.onstart = false;
            }
            else
            {
                this.onstart = true;
            }

        }



    }


}
