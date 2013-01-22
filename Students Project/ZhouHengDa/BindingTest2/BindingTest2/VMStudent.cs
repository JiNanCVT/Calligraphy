using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BindingTest2
{
    class VMStudent
    {
        CollectionViewSource students3;

        public CollectionViewSource Students3
        {
            get { return students3; }
            set { students3 = value; }
        }
        ObservableCollection<Student> students1;

        internal ObservableCollection<Student> Students1
        {
            get { return students1; }
            set { students1 = value; }
        }

        ObservableCollection<Student> students2;

        internal ObservableCollection<Student> Students2
        {
            get { return students2; }
            set { students2 = value; }
        }

        public VMStudent()
        {
            students3 = new CollectionViewSource();

            Student stu1 = new Student();
            Student stu2 = new Student();
            stu1.Name = "zhangsan";
            stu1.PhotoBitmapImage1 = "E:\\新建文件夹\\3.png";
            stu2.Name = "lisi";
            stu2.PhotoBitmapImage1 = "E:\\新建文件夹\\4.png";

            students1 = new ObservableCollection<Student>();
            students1.Add(stu1);
            students1.Add(stu2);
            

            Student stu3 = new Student();
            Student stu4 = new Student();
            stu3.Name = "wangwu";
            stu3.PhotoBitmapImage1 = "E:\\新建文件夹\\1.png";
            stu4.Name = "zhaoliu";
            stu4.PhotoBitmapImage1 = "E:\\新建文件夹\\2.png";

            students2 = new ObservableCollection<Student>();
            students2.Add(stu3);
            students2.Add(stu4);
            students3.Source = students2;
        }
    }
}
