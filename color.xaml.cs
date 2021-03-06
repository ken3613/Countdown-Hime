﻿using System;
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
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Desktopwpf
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        private double th, tw;
        MainWindow mc;
        public double nh;
        public double nw;
        time mtimer;

        RegistryKey local = Registry.CurrentUser;
        string keyreg;

        public Window1(MainWindow mf,time timer,string mode)
        {
            nw = 776;
            nh = 212;
            this.mc = mf;
            this.mtimer = timer;
            this.th = this.mtimer.Height;
            this.tw = this.mtimer.Width;           
            InitializeComponent();
            this.slider1.Value = this.th / nh;
            if (mode == "gk")
            {
                this.radioButton1.IsChecked = true;
                this.radioButton2.IsChecked = false;
            }
            else
            {
                this.radioButton2.IsChecked = true;
                this.radioButton1.IsChecked = false;
            }

            }



        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.mtimer.Width = nw * slider1.Value;
            this.mtimer.Height = nh * slider1.Value;
        }

        private void colorwin_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            double rate;
            rate = this.slider1.Value;
            winapi.WritePrivateProfileString("size", "sizerate", Convert.ToString(rate), this.mtimer.path);
        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            keyreg = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
            RegistryKey key = local.OpenSubKey(keyreg, true);
            string exepath;
            exepath = "\"" + System.Windows.Forms.Application.StartupPath + "\\DeskTop.exe\"";
            key.SetValue("DeskTop", exepath, RegistryValueKind.String);
            this.mc.onstart = true;
        }

        private void checkBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            keyreg = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
            RegistryKey key = local.OpenSubKey(keyreg, true);
            key.DeleteValue("DeskTop");
            this.mc.onstart = false;
        }

        private void colorwin_Loaded(object sender, RoutedEventArgs e)
        {
            this.checkBox1.IsChecked = this.mc.onstart;
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            mc.numlabel.Content = mc.gk_c_date.Days.ToString();
            mc.titlelabel.Content = "距离高考还有";
            winapi.WritePrivateProfileString("timer", "mode", "gk", System.Windows.Forms.Application.StartupPath + "\\settings.ini");
            mtimer.exam = mtimer.gk_exam;
            mtimer.title.Content = "距离" + mtimer.gk.ToString() +"高考还有";
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            mc.numlabel.Content = mc.zk_c_date.Days.ToString();
            mc.titlelabel.Content = "距离中考还有";
            winapi.WritePrivateProfileString("timer", "mode", "zk", System.Windows.Forms.Application.StartupPath + "\\settings.ini");
            mtimer.exam = mtimer.zk_exam;
            mtimer.title.Content = "距离" + mtimer.zk.ToString() + "中考还有";
        }

    }
}
