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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CutPhote.Entities;

namespace CutPhote
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        BaseCopybook baseCopybookShow = new BaseCopybook();

        private void open_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result=MessageBoxResult.No ;
            if (baseCopybookShow._image != null)
            {
                string message = "已打开碑帖，是否替换";
                string caption = "替换碑帖";
                MessageBoxButton button = MessageBoxButton.YesNo;
                
                result = MessageBox.Show(message, caption, button);
            }
            if(baseCopybookShow._image==null | result==MessageBoxResult.Yes)
            {
                Microsoft.Win32.OpenFileDialog openImageDialog = new Microsoft.Win32.OpenFileDialog();
                openImageDialog.Filter = @"图片|*.jpg;*.png;*.gif;*.bmp;*.jpeg";
                openImageDialog.FilterIndex = 3;
                openImageDialog.RestoreDirectory = true;

                if (openImageDialog.ShowDialog() == true)
                {
                    //获取路径
                    string fileName = openImageDialog.FileName;

                    //显示图片
                    baseCopybookShow._image = new BitmapImage(new Uri(fileName));
                    copybook.Source = baseCopybookShow._image;
                    copybook.Width = baseCopybookShow._image.Width;
                    copybook.Height = baseCopybookShow._image.Height;
                }
            }
        }

        private void Button_Click_ZoomIn(object sender, RoutedEventArgs e)
        {
            //放大图片
            if (baseCopybookShow._image == null)
                MessageBox.Show("请先打开碑帖");
            else
            {
                copybook.Width = copybook.Width * 1.25;
                copybook.Height = copybook.Height * 1.25;
            }
            //调整分割线的位置，暂缺
        }

        private void Button_Click_ZoomOut(object sender, RoutedEventArgs e)
        {
            //缩小图片
            if (baseCopybookShow._image == null)
                MessageBox.Show("请先打开碑帖");
            else
            {
                copybook.Width = copybook.Width * 0.8;
                copybook.Height = copybook.Height * 0.8;
            }
            //调整分割线的位置，暂缺
        }

        private void Button_Click_Fullscreen(object sender, RoutedEventArgs e)
        {
            if (baseCopybookShow._image == null)
                MessageBox.Show("请先打开碑帖");
            else
            {
                //横向比，scrollViewer.width 无法获取！
                double horizontalRatio = ScrollViewer.Width / baseCopybookShow._image.Width;
                //纵向比
                double verticalRatio = ScrollViewer.Height / baseCopybookShow._image.Height;
                //转换比例
                double ratio = horizontalRatio < verticalRatio ? horizontalRatio : verticalRatio;

                //全屏切换
                if (baseCopybookShow._imagestatus == BaseCopybook.statuses.norm)
                {
                    copybook.Width = baseCopybookShow._image.Width * ratio;
                    copybook.Height = baseCopybookShow._image.Height * ratio;
                    baseCopybookShow._imagestatus = BaseCopybook.statuses.fullscreen;
                    FullScreen.Content = "复原";
                }
                else
                {
                    copybook.Width = baseCopybookShow._image.Width;
                    copybook.Height = baseCopybookShow._image.Height;
                    baseCopybookShow._imagestatus = BaseCopybook.statuses.norm;
                    FullScreen.Content = "全屏";
                }
            }
        }




    }
}
