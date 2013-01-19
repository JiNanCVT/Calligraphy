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
using System.ComponentModel;

namespace CalligraphyEditor
{
    /// <summary>
    /// PRubbing.xaml 的交互逻辑
    /// </summary>
    public partial class PRubbings : Page
    {
        VMRubbings _vmRubbing;
        public PRubbings(T_Author author)
        {            
            InitializeComponent();
            _vmRubbing = new VMRubbings(author);
            Grd_rubbing.DataContext = _vmRubbing.Cvs_Rubbings;
            Lb_Author.Content = author.Name;
        }        

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            Image i = sender as Image;
            T_Photo p = i.Tag as T_Photo;

            if (p == null)
                return;
            
            Uri uri = CalligraphyEditor.App.Entities.GetReadStreamUri(p);
            if (uri != null)
                i.Source = new BitmapImage(uri);
        }

        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            WAddRubbing w = new WAddRubbing(_vmRubbing);
            w.ShowDialog();
        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            _vmRubbing.DeleteCurrentRubbing();
        }

       

    }
}
