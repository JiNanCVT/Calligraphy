using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BindingTest2
{
    class Student
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string PhotoBitmapImage;

        public string PhotoBitmapImage1
        {
            get { return PhotoBitmapImage; }
            set { PhotoBitmapImage = value; }
        }
    }
}
