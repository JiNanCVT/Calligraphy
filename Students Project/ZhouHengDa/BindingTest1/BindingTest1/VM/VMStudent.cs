using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BindingTest1
{
    class VMStudent
    {
        ObservableCollection<Student> student;

        internal ObservableCollection<Student> Student
        {
            get { return student; }
            set { student = value; }
        }

        public VMStudent()
        {
            Student stu1 = new Student();
            stu1.Name = "zhangsan";
            stu1.PhotoBitmapImage1 = "E:\\新建文件夹\\1.png";
            Student stu2 = new Student();
            stu2.Name = "lisi";
            stu2.PhotoBitmapImage1 = "E:\\新建文件夹\\2.png";
            student.Add(stu1);
            student.Add(stu2);
        }

        public void Add()
    {
            
    }
    }
    
}
