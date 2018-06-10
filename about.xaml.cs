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
using System.Windows.Shapes;

namespace Desktopwpf
{
    /// <summary>
    /// about.xaml 的交互逻辑
    /// </summary>
    public partial class about : Window
    {
        public about()
        {
            InitializeComponent();
            textBox1.Text ="Code By Nona&广州三中2018届高三6班\r\n"
                + "最后编译时间:" + System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location)
                +"(C#.NET4.0WPF)\r\n"
                + "此项目在Github上开源，欢迎提交Pull Request\r\nGithub地址:https://github.com/ken3613/Countdown-Hime"
                +"\r\n\r\n2018.6.8：\r\n这算是学长留给学弟学妹们的礼物吧，看着倒计时，希望你们从365坚持到0！"
                +"\r\n你握笔的手，有我的力量！（雾）"
                +"\r\n\r\n这个地方是给作者玩的"
                +"\r\n本来想把自动更新写进去的，无奈那段时间WIFI密码被改了，就放弃了这个念头"
                +"\r\n\r\n2018.6.10:\r\n1、可以在高考模式跟中考模式中自由切换啦！"
                +"\r\n\r\n2018.5.20:\r\n1、增加桌面倒计时\r\n2、优化UI"
                +"\r\n\r\n2018.4:\r\n1、用WPF重写了一遍，终于算是把WPF的托盘菜单问题给解决了"
                +"\r\n\r\n2018.3.25:\r\n1、试着用winform重写，虽然原生支持托盘控件，但无奈半透明效果太麻烦，放弃"
                +"\r\n\r\n2018.2:\r\n1、用E写的最初版本成形！";
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ken3613/Countdown-Hime");
        }
    }
}
