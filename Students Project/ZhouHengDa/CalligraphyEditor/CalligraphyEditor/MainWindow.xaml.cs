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
using CalligraphyEditor.CalligraphyData;

namespace CalligraphyEditor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigateToAuthor();
        }

        void NavigateToAuthor()
        {
            IPage ip = Frm_Content.Content as IPage;
            if (ip != null)
            {
                if (ip.Path == "书法家")
                    return;
            }

            DevPAuthors devAuthors = new DevPAuthors();

            NavigateToPage(devAuthors);
        }

        void NavigateToPage(Page p)
        {
            Frm_Content.Navigate(p);            
            IPage ip = (IPage)p;
            Lbl_Path.Content = ip.Path;
            ip.NavigateToPage += new EventHandler<NavigateToPageEventArgs>(NavigateToPage);
        }

        void SetEditable(bool isEditable)
        {
            Btn_Add.IsEnabled = isEditable;
            Btn_delete.IsEnabled = isEditable;
        }

        void NavigateToPage(object sender, NavigateToPageEventArgs e)
        {
            NavigateToPage(e.ToPage);
            Frm_Content.Navigate(e.ToPage);
        }


        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            IPage ip = Frm_Content.Content as IPage;
            ip.Add();
        }

        private void Btn_delete_Click(object sender, RoutedEventArgs e)
        {
            IPage ip = Frm_Content.Content as IPage;
            ip.Delete();
        }

        private void Frm_Content_Navigated(object sender, NavigationEventArgs e)
        {
            IPage ip = e.Content as IPage;
            SetEditable(ip.Editable);
        }

        private void Btn_Home_Click(object sender, RoutedEventArgs e)
        {
            NavigateToAuthor();
        }
    }
}
