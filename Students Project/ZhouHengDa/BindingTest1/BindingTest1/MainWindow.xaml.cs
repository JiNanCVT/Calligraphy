using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BindingTest1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ObservableCollection<Student> students = new ObservableCollection<Student>();
            InitializeComponent();
            Student stu1 = new Student();
            stu1.Name = "zhangsan";
            stu1.PhotoBitmapImage1 = "E:\\新建文件夹\\1.png";
            Student stu2 = new Student();
            stu2.Name = "lisi";
            stu2.PhotoBitmapImage1 = "E:\\新建文件夹\\2.png";

            students.Add(stu1);
            students.Add(stu2);
            layoutRoot.DataContext = students;
        }
    }
}
