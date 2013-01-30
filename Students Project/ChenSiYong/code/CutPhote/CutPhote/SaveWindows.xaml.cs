using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.IO;
using CutPhote.Entities;
using CutPhote.ViewModel;

namespace CutPhote
{
    /// <summary>
    /// SaveWindows.xaml 的交互逻辑
    /// </summary>
    public partial class SaveWindows : Window
    {
        public SaveWindows(VMCopybookcut vmC)
        {
            InitializeComponent();
            Cvm = vmC;
            this.layoutBoot.DataContext = Cvm.Ocl_CopybookCut;
        }

        VMCopybookcut Cvm;

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            //Microsoft.Win32.SaveFileDialog SaveImageDialog = new Microsoft.Win32.SaveFileDialog();
            System.Windows.Forms.FolderBrowserDialog SaveImageDialog = new System.Windows.Forms.FolderBrowserDialog();

            System.Windows.Forms.DialogResult SelectedPath = SaveImageDialog.ShowDialog();

            string SavePath;

            if ((SavePath = SaveImageDialog.SelectedPath) != null)
            {                
                Cvm.save(SavePath);
            }
            //Microsofvar SaveImageDialog = new  CommonOpenFileDialog();
            //string path = SaveImageDialog.FileName ;

        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (list.SelectedItem != null)
            {
                int n = list.SelectedIndex;
                Cvm.delet(n);
            }
            else
            {
                MessageBox.Show("请选择要删除的字");
            }
        }

    }
}
