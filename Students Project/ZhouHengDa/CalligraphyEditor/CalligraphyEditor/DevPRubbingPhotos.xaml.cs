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
using CalligraphyEditor.ViewModel;
using CalligraphyEditor.CalligraphyData;
using DevExpress.Xpf.Grid;
using System.Data.Services.Client;
using DevExpress.Xpf.Editors;

namespace CalligraphyEditor
{
    /// <summary>
    /// DevPRubbingPhotos.xaml 的交互逻辑
    /// </summary>
    public partial class DevPRubbingPhotos : Page,IPage
    {
        VMRubbingPhotos _vmrubbingPhotos;

        public DevPRubbingPhotos(T_Rubbing rubbing)
        {
            InitializeComponent();
            _vmrubbingPhotos = new VMRubbingPhotos(rubbing);
            Grd_Photos.DataContext = _vmrubbingPhotos.Cvs_RubbingPhoto;
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {

            ImageEdit img = sender as ImageEdit;

            EditGridCellData d = img.Tag as EditGridCellData;
            T_RubbingPhoto rubbingPhoto = d.RowData.Row as T_RubbingPhoto;
            if (rubbingPhoto == null)
                return;
            T_Photo photo = rubbingPhoto.T_Photo;

            if (photo == null)
                return;

            System.Diagnostics.Debug.WriteLine("|||| " + rubbingPhoto.PageNumber.ToString());

            //Uri uri = CalligraphyEditor.App.Entities.GetReadStreamUri(photo);
            //System.Diagnostics.Debug.WriteLine("|||| " + uri.ToString());
            //if (uri != null)
            //    img.Source = new BitmapImage(uri);
        }

        public void Add()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public event EventHandler<NavigateToPageEventArgs> NavigateToPage;

        

        bool IPage.Editable
        {
            get
            {
                return false;
            }
        }


        public string Path
        {
            get { return "书法家 / " + _vmrubbingPhotos.Rubbing.T_Author.Name.Trim() + " / " + _vmrubbingPhotos.Rubbing.Name.Trim(); }
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenPhotoWindow();
        }

        private void Image_TouchUp(object sender, TouchEventArgs e)
        {
            OpenPhotoWindow();
        }

        private void OpenPhotoWindow()
        {
            DevWPhoto w = new DevWPhoto(_vmrubbingPhotos.Cvs_RubbingPhoto.Source as DataServiceCollection<T_RubbingPhoto>, _vmrubbingPhotos.Cvs_RubbingPhoto.View.CurrentItem as T_RubbingPhoto);
            w.ShowDialog();
        }

    }
}
