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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BindingTest2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //VMStudent vm = new VMStudent();
            //layoutRoot.DataContext = vm.Students2;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            VMStudent vm = new VMStudent();
            Student stu = new Student();
            stu.Name = "zhouqi";
            stu.PhotoBitmapImage1 = "E:\\新建文件夹\\5.png";
            vm.Students2.Add(stu);
            vm.Students3.Source = vm.Students2;
            layoutRoot.DataContext = vm.Students3;
            //Window2 w2 = new Window2();
            //w2.ShowDialog();
        }
    }
}
