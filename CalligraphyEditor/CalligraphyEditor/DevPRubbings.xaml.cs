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
using CalligraphyEditor.ViewModel;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using System.Data.Services.Client;

namespace CalligraphyEditor
{
    /// <summary>
    /// DevPRubbings.xaml 的交互逻辑
    /// </summary>
    public partial class DevPRubbings : Page,IPage
    {
        T_Author _author;
        VMRubbings _vmRubbings;

        public DevPRubbings(T_Author author)
        {
            InitializeComponent();
            _author = author;
            _vmRubbings = new VMRubbings(_author);
            Gdc_Rubbings.DataContext = _vmRubbings.Cvs_Rubbings;
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            Image img = sender as Image;

            EditGridCellData d = img.Tag as EditGridCellData;
            T_Rubbing rubbing = d.RowData.Row as T_Rubbing;

            if (rubbing.T_RubbingPhoto.Count == 0)
                return;

            Random random = new Random();
            int idx = random.Next(rubbing.T_RubbingPhoto.Count);
            T_Photo photo = rubbing.T_RubbingPhoto[idx].T_Photo;

            Uri uri = CalligraphyEditor.App.Entities.GetReadStreamUri(photo);
            if (uri != null)
                img.Source = new BitmapImage(uri);
        }

        public void Add()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                T_Rubbing r = _vmRubbings.NewRubbing();
                r.Name = "碑帖";
                r.Description = "碑帖简介";
                var files = System.IO.Directory.GetFiles(dialog.SelectedPath);
                var errorMsg = _vmRubbings.AddRubbing(r, files);
                if(!string.IsNullOrEmpty(errorMsg))
                {
                    MessageBox.Show(errorMsg, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void Delete()
        {
            _vmRubbings.DeleteCurrentRubbing();
        }

        public event EventHandler<NavigateToPageEventArgs> NavigateToPage;


        public void Save()
        {
            _vmRubbings.SaveCurrentItem();
        }

        private void view_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            Save();
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
            T_Rubbing rubbing = _vmRubbings.Cvs_Rubbings.View.CurrentItem as T_Rubbing;

            NavigateToPageEventArgs arg = new NavigateToPageEventArgs();
            DevPRubbingPhotos p = new DevPRubbingPhotos(rubbing);
            arg.ToPage = p;
            NavigateToPage(this, arg);
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
            get { return "书法家 / "+_vmRubbings.Author.Name.Trim(); }
        }

    }
}
