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
using CalligraphyEditor.ViewModel;
using CalligraphyEditor.CalligraphyData;
using System.IO;

namespace CalligraphyEditor
{
    /// <summary>
    /// WAddRubbing.xaml 的交互逻辑
    /// </summary>
    public partial class WAddRubbing : Window
    {
        VMRubbings _vmRubbings;
        public WAddRubbing(VMRubbings vmRubbings)
        {
            _vmRubbings = vmRubbings;
            InitializeComponent();
            T_Rubbing r = _vmRubbings.NewRubbing();
            Grd_Rubbing.DataContext = r;
        }

        private void Btn_OK_Click(object sender, RoutedEventArgs e)
        {
            T_Rubbing rubbing = (T_Rubbing)Grd_Rubbing.DataContext;
            try
            {
                var files = System.IO.Directory.GetFiles(Txt_Folder.Text);
                _vmRubbings.AddRubbing(rubbing, files);
                this.DialogResult = true;
            }
            catch (Exception )
            {
                MessageBox.Show("请指定图片的路径");
                this.Close();
            }           
            
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Btn_BrowserFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Txt_Folder.Text = dialog.SelectedPath;
            }
        }
    }
}
