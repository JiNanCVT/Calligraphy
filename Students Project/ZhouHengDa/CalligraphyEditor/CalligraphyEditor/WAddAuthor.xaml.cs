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
using CalligraphyEditor.CalligraphyData;
using System.Collections.ObjectModel;
using CalligraphyEditor.ViewModel;

namespace CalligraphyEditor
{
    /// <summary>
    /// WAddAuthor.xaml 的交互逻辑
    /// </summary>
    public partial class WAddAuthor : Window
    {
        VMAuthors _vmAuthors;
        T_Author _author;

        /// <summary>
        /// 添加书法家
        /// </summary>
        /// <param name="authors"></param>
        public WAddAuthor(VMAuthors vmAuthors)
        {
            InitializeComponent();
            _author = new T_Author();
            Grd_Author.DataContext = _author;
            _vmAuthors = vmAuthors;
        }


        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            _vmAuthors.AddAuthor(_author);
            Close();
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Grd_Author.DataContext = _author;
        }
    }
}
