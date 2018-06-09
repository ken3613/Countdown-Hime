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
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Windows.Threading;
using System.IO;

namespace Desktopwpf
{
    /// <summary>
    /// time.xaml 的交互逻辑
    /// </summary>
    public partial class time : Window
    {
        //隐藏alt+tab
        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_TOOLWINDOW = 0x00000080;

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        //

        //窗口大小倍率
        double sizerate;
        public string path;
        //日期类
        DispatcherTimer timer1 = new DispatcherTimer();
        string day, hour, minute, second,cont;
        DateTime n_date = new DateTime();
        DateTime exam;
        TimeSpan c_date;

        public time(MainWindow w,int y)
        {
            int year = y;
            InitializeComponent();
            exam = new DateTime(year, 6, 7, 9, 0, 0);
            title.Content = "距离" + year.ToString() + "高考还有";
            timer1.Interval = TimeSpan.FromSeconds(1);
            timer1.Tick += new EventHandler(changetime);
            timer1.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //隐藏alt+tab
            WindowInteropHelper wndHelper = new WindowInteropHelper(this);

            int exStyle = (int)GetWindowLong(wndHelper.Handle, (int)-20);

            exStyle |= (int)0x00000080;
            SetWindowLong(wndHelper.Handle, (int)-20, (IntPtr)exStyle);
            //

            //自动调整大小
            
            path = System.Windows.Forms.Application.StartupPath+ "\\settings.ini";
                StringBuilder rate = new StringBuilder(500);
                winapi.GetPrivateProfileString("size", "sizerate", "", rate, 500, path);
                sizerate = Convert.ToDouble(rate.ToString());
                this.Height = this.Height * sizerate;
                this.Width = this.Width * sizerate;
        }

        public void changetime(object sender, EventArgs e)
        {
            n_date = DateTime.Now;
            c_date = exam - n_date;
            if (c_date.Days < 10)
            {
                day = "0" + c_date.Days.ToString();
            }
            else
            {
                day = c_date.Days.ToString();
            }

            if (c_date.Hours < 10)
            {
                hour = "0" + c_date.Hours.ToString();
            }
            else
            {
                hour = c_date.Hours.ToString();
            }
            if (c_date.Minutes < 10)
            {
                minute = "0" + c_date.Minutes.ToString();
            }
            else
            {
                minute = c_date.Minutes.ToString();
            }
            if (c_date.Seconds < 10)
            {
                second = "0" + c_date.Seconds.ToString();
            }
            else
            {
                second = c_date.Seconds.ToString();
            }
            cont = day + "天" + hour + "时" + minute + "分" + second + "秒";
            num.Content = cont;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Rect workArea = SystemParameters.WorkArea;
            Left = (workArea.Width - e.NewSize.Width) / 2 + workArea.Left;
            Top = (workArea.Height - e.NewSize.Height) / 2 + workArea.Top;  
        }

        
    
    }
}
