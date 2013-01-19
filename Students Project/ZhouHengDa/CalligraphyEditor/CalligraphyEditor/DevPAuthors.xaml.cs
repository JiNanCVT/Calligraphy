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
using DevExpress.Xpf.Grid;
using CalligraphyEditor.CalligraphyData;
using CalligraphyEditor.ViewModel;

namespace CalligraphyEditor
{
    /// <summary>
    /// DevAuthors.xaml 的交互逻辑
    /// </summary>
    public partial class DevPAuthors : Page, IPage
    {
        public DevPAuthors()
        {
            InitializeComponent();
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NavigateToPhotos();
        }

        private void Image_TouchUp(object sender, TouchEventArgs e)
        {
            NavigateToPhotos();
        }

        private void NavigateToPhotos()
        {
            VMAuthors vma = this.Resources["Vm_authos"] as VMAuthors;
            NavigateToPageEventArgs arg = new NavigateToPageEventArgs();
            DevPRubbings p = new DevPRubbings(vma.Cvs_authors.View.CurrentItem as T_Author);
            arg.ToPage = p;
            NavigateToPage(this, arg);
        }

        public void Add()
        {
            T_Author author = new T_Author();
            author.Name = "书法家";
            author.Description = "简介";

            VMAuthors vma = GetVMAuthor();
            vma.AddAuthor(author);
        }

        private VMAuthors GetVMAuthor()
        {
            return (VMAuthors)this.Resources["Vm_authos"];
        }

        public void Delete()
        {
            VMAuthors vma = GetVMAuthor();
            vma.DeleteCurrentItem();
        }

        public event EventHandler<NavigateToPageEventArgs> NavigateToPage;


        public void Save()
        {
            VMAuthors vma = GetVMAuthor();
            vma.SaveCurrentItem();
        }


        private void view_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            Save();
        }

        bool IPage.Editable
        {
            get
            {
                return true;
            }
        }


        public string Path
        {
            get { return "书法家"; }
        }
    }
}
