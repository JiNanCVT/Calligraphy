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
using System.Data.Services.Client;
using CalligraphyEditor.ViewModel;

namespace CalligraphyEditor
{
    /// <summary>
    /// DevWPhoto.xaml 的交互逻辑
    /// </summary>
    public partial class DevWPhoto : Window
    {
        private VMPhotos _vmPhoto;
        public DevWPhoto(DataServiceCollection<T_RubbingPhoto> rubbingPhotos, T_RubbingPhoto currentRubbingPhoto )
        {
            InitializeComponent();
            _vmPhoto = new VMPhotos(rubbingPhotos, currentRubbingPhoto);
            LoadImage();
            SetButtonEnable();
        }



        private void LoadImage()
        {
            Img_Photo.Source = new BitmapImage(new Uri(_vmPhoto.GetCurrentBitmapImage()));
            
        }

        private void SetButtonEnable()
        {
            Btn_Next.IsEnabled = _vmPhoto.IsCanNext();
            Btn_Pre.IsEnabled = _vmPhoto.IsCanPre();
        }

        private void Btn_Pre_Click(object sender, RoutedEventArgs e)
        {
            _vmPhoto.Pre();
            LoadImage();
            SetButtonEnable();
        }

        private void Btn_Next_Click(object sender, RoutedEventArgs e)
        {
            _vmPhoto.Next();
            LoadImage();
            SetButtonEnable();
        }

        private void Btn_Resize_Click(object sender, RoutedEventArgs e)
        {
            if (Img_Photo.Stretch == Stretch.None)
            {
                Img_Photo.Stretch = Stretch.Uniform;
                Btn_Resize.Content = "实际大小";
            }
            else
            {
                Img_Photo.Stretch = Stretch.None;
                Btn_Resize.Content = "满屏";
            }
        }

    }
}
