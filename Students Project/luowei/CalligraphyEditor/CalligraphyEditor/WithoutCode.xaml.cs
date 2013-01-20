using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NetDimension.Weibo;

namespace CalligraphyEditor
{
    /// <summary>
    /// WithoutCode.xaml 的交互逻辑
    /// </summary>
    public partial class WithoutCode : Window
    {
        OAuth p;
        public WithoutCode(OAuth o)
        {
            InitializeComponent();
            p = o;
        }

        private void 登陆_Click(object sender, RoutedEventArgs e)
        {
            string passport = haoma.Text;
            string password = mima.Password;
            p.ClientLogin(passport, password);




            Properties.Settings.Default.AcessToken = p.AccessToken;

            Properties.Settings.Default.Save();

            Client Sina = new Client(p);
            string uid = Sina.API.Entity.Account.GetUID();
            var entity_userInfo = Sina.API.Entity.Users.Show(uid);

            var statusInfo = Sina.API.Entity.Statuses.Update(string.Format("wolgequ", DateTime.Now.ToShortTimeString()));
            string UserId1 = entity_userInfo.ScreenName;
            MainWindow main = new MainWindow(UserId1);
            
            main.Show();
            this.Hide();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("www.weibo.com");
        }

        
    }
}
