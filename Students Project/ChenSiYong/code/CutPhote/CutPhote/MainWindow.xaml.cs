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
using CutPhote.ViewModel;
using System.Collections.ObjectModel;
using System.IO;

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
            //this.ScrollViewer.DataContext = vml._LineSets;
            //this.gridline.DataContext = vml.Line;
            lvm = this.Resources["vml"] as VMShape;
            //ItC.SetBinding(ItemsControl.ItemsSourceProperty, new Binding() { Path = new PropertyPath("_LineSets") });
            //ItC.SetBinding(ItemsControl.ItemTemplateProperty, new Binding() { Path = new PropertyPath("line") });
            baseCopybookShow = new BaseCopybook();

            LineMode.IsEnabled = false;
            RectMode.IsEnabled = false;

            btn_Creat.IsEnabled = false;
            btn_Confirm.IsEnabled = false;

            tb_row.IsEnabled = false;
            tb_column.IsEnabled = false;
            //Click_RadioButton(LineMode as UIElement, null);

        }

        BaseCopybook baseCopybookShow;
        //VMLine vml;
        VMCopybookcut vmC;
        Uri baseUri;
        VMShape lvm;
        BitmapSource bitmap;
        //DrawLine line;


        private void open_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBoxResult result=MessageBoxResult.No ;
            if (baseCopybookShow.Image != null)
            {
                result = MessageBox.Show("已打开碑帖，是否替换", "替换碑帖", MessageBoxButton.YesNo);
            }
            if(baseCopybookShow.Image==null | result==MessageBoxResult.Yes)
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
                    baseUri = new Uri(fileName);

                    baseCopybookShow.BaseCopybookResetSet(baseUri);

                    copybook.Source = baseCopybookShow.Image;

                    copybook.Width = baseCopybookShow.Image_basewidth;
                    copybook.Height = baseCopybookShow.Image_baseheight;

                    //widthSlider.Maximum = baseCopybookShow.Image.Width / 10;

                    lvm.ImageSizeChange(baseCopybookShow.Image_baseheight, baseCopybookShow.Image_basewidth);

                    bitmap = baseCopybookShow.Image;
                    //修改按钮模式
                    LineMode.IsEnabled = true;
                    RectMode.IsEnabled = true;
                    //默认 直线切割模式
                    LineMode.IsChecked = true;                 
                    Click_RadioButton(LineMode as UIElement, null);
                }
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (lvm.Image_height != baseCopybookShow.Image_baseheight)
            {
                copybook.Width = baseCopybookShow.Image_basewidth;
                copybook.Height = baseCopybookShow.Image_baseheight;
                lvm.ImageSizeChange(baseCopybookShow.Image_basewidth / copybook.ActualWidth);
            }
            if(LineMode.IsChecked ==true)
                Save_Image_Line();
            if (vmC != null)
            {
                SaveWindows savewin = new SaveWindows(vmC);
                savewin.Show();
            }
            else
                MessageBox.Show("当前没有保存的字，请更换为 直线切割模式 或者 点击 确认切割");
        }

        private void Click_RadioButton(object sender, RoutedEventArgs e)
        {
            RadioButton el = sender as RadioButton;
            if (el.Name == "LineMode")
            {
                ItC.SetBinding(ItemsControl.ItemsSourceProperty, new Binding() { Path = new PropertyPath("_LineSets") });
                //重设DataTemplate
                ItC.ItemTemplate = this.Resources["Line"] as DataTemplate;
                //修改按钮是否可用
                RectMode.IsChecked = false;

                btn_Creat.IsEnabled = false;
                btn_Confirm.IsEnabled = false;

                tb_row.IsEnabled = true;
                tb_column.IsEnabled = true;

                //System.Windows.Controls.Panel.SetZIndex(copybook as UIElement, 1);
            }
            else
            {
                //LineMode.IsEnabled = false; 
                //更改绑定源
                ItC.SetBinding(ItemsControl.ItemsSourceProperty, new Binding() { Path = new PropertyPath("Rect") });
                //重设DataTemplate
                ItC.ItemTemplate = this.Resources["Rect"] as DataTemplate;
                //修改按钮是否可用
                LineMode.IsChecked = false;

                btn_Creat.IsEnabled = true;
                btn_Confirm.IsEnabled = true;

                tb_row.IsEnabled = false;
                tb_column.IsEnabled = false;

                //System.Windows.Controls.Panel.SetZIndex(copybook as UIElement, 1);
            }
        }

        private void Button_Rect_Click_Creat(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if ((string)btn.Content == "逐字切割")
            {
                btn.Content = "开始切割";
                if (lvm.Rect.Count == 0)
                    lvm.Rect.Add(new Entities.Rect());
            }
            else
            {
                btn.Content = "逐字切割";
            }
        }

        private void Button_Rect_Click_Confirm(object sender, RoutedEventArgs e)
        {
            //方框方式下保存
            if (vmC == null)
                vmC = new VMCopybookcut();
            else
                vmC._ocl_CopybookCut.Clear();

            CutPhote.Entities.Rect R = lvm.Rect[0];
            Save_Image(R.Mg.Left, R.Mg.Top, R.Mg.Left + R.RectWidth, R.Mg.Top + R.RectHeight);
        }

        //这两个变量在中发挥作用。
        private bool _isDrag = false;

        private Point _dragOffset;
        
        private void Line_MouseLeftButtonDown_Line(object sender, MouseButtonEventArgs e)
        {
            //读取鼠标是否在线上
            UIElement el = sender as UIElement;

            if (el != null)
            {

                _isDrag = true;

                _dragOffset = e.GetPosition(el);

                el.CaptureMouse();

            }

        }

        private void Line_MouseLeftButtonUp_Line(object sender, MouseButtonEventArgs e)
        {
            //读取鼠标是否放开
            if (_isDrag == true)
            {

                UIElement el = sender as UIElement;

                el.ReleaseMouseCapture();

                _isDrag = false;

            }
        }

        private void Line_MouseMove_Line(object sender, MouseEventArgs e)
        {
            //移动线
            if (_isDrag == true)
            {

                Point pt = Mouse.GetPosition(Canvas1);

                Line el = sender as Line;

                if (el.X1 != el.X2)
                {

                    el.Y1 = pt.Y;// -_dragOffset.Y;

                    el.Y2 = pt.Y;// -_dragOffset.Y;
                }

            }

        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point pt = Mouse.GetPosition(Canvas1);

            UIElement el = sender as UIElement;

            if (el != null)
            {

                _isDrag = true;

                _dragOffset = e.GetPosition(el);

                el.CaptureMouse();

                _dragOffset.X = pt.X;

                _dragOffset.Y = pt.Y;

                //Rectangles RectShow = new Rectangles();

                //RectShow._siteX = _dragOffset.X;

                //RectShow._siteY = _dragOffset.Y;

                //lvm.Rect.Add(RectShow);

                //el.CaptureMouse();

            }
        }

        private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_isDrag == true)
            {

                UIElement el = sender as UIElement;

                el.ReleaseMouseCapture();

                _isDrag = false;

            }
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDrag == true)
            {

                Point pt = Mouse.GetPosition(Canvas1);

                Rectangle el = sender as Rectangle;
                //鼠标右键，改变大小
                if (e.RightButton == MouseButtonState.Pressed)
                {

                    el.Width += pt.X - _dragOffset.X;

                    el.Height += pt.Y - _dragOffset.Y;

                    _dragOffset.X = pt.X;

                    _dragOffset.Y = pt.Y;
                }
                //鼠标左键，改变位置
                if(e.LeftButton==MouseButtonState.Pressed)
                {
                    //Canvas.SetLeft(el, Canvas.GetLeft(el) + 50*(pt.X - _dragOffset.X));
                    //Canvas.SetLeft(el, pt.X);
                    Left = lvm.Rect[0].Mg.Left + pt.X - _dragOffset.X;
                    Top = lvm.Rect[0].Mg.Top + pt.Y - _dragOffset.Y;
                    el.Margin = new Thickness(Left, Top, 0, 0);
                    //Canvas.SetTop(el, Canvas.GetTop(el) +50*( pt.Y - _dragOffset.Y));
                    //Canvas.SetTop(el, pt.Y);
                    //lvm.Rect[0].Mg.Top += pt.Y - _dragOffset.Y;

                    _dragOffset.X = pt.X;

                    _dragOffset.Y = pt.Y;
                }
                //Rectangles el = lvm.Rect[0];

                //el.RectWidth = pt.X - _dragOffset.X;

                //el.RectHeight = pt.Y - _dragOffset.Y;
                
                }
        }
        
        public void Save_Image_Line()
        {
            //划线方式下保存
            if(vmC==null)
                vmC = new VMCopybookcut();
            else
                vmC._ocl_CopybookCut.Clear();

            for (int i = 0; i < int.Parse(this.tb_column.Text); i++)
            {
                Lines l_last = lvm._LineSets[i * (int.Parse(this.tb_row.Text) + 1)];
                for (int j = 0; j < int.Parse(this.tb_row.Text); j++)
                {
                    Lines line = lvm._LineSets[i * (int.Parse(this.tb_row.Text) + 1) + j + 1];

                    Save_Image(l_last.X1,l_last.Y1,line.X2,line.Y2);

                    l_last = line;


                }
            }
        }

        //public void Save_Image_Rect()
        //{
        //    //方框方式下保存
        //    if (vmC == null)
        //        vmC = new VMCopybookcut();
        //    else
        //        vmC._ocl_CopybookCut.Clear();

        //    CutPhote.Entities.Rect R=lvm.Rect[0];
        //    Save_Image(R.Mg.Left, R.Mg.Top, R.Mg.Left + R.RectWidth, R.Mg.Top + R.RectHeight);
        //}

        public void Save_Image(double X1 , double Y1 , double X2 , double Y2)
        {
            int width = Convert.ToInt32(X2 - X1 - 2);

            int stride = Convert.ToInt32((width - 1.1) / 4 + 1) * 4;

            int heigth = Convert.ToInt32(Y2 - Y1 );
            // 缓存数组（RGBA的每个像素需要4Byte）    
            var array = new byte[stride * heigth * 4];
            // 截取区域    
            var rect = new Int32Rect(Convert.ToInt32(X1), Convert.ToInt32(Y1), stride, heigth);

            // 复制到数组    
            bitmap.CopyPixels(rect, array, stride * 4, 0);
            // 从数组创建新的位图对象    
            var Imgcut = BitmapSource.Create(width, heigth, 96, 96, PixelFormats.Bgr32, BitmapPalettes.Halftone256, array, stride * 4);

            vmC.Ocl_CopybookCut.Add(new CopybookCut(Imgcut));
        }

        private void Button_Click_ZoomIn(object sender, RoutedEventArgs e)
        {
            //放大图片
            Button UIEl = sender as Button;
            if (baseCopybookShow.Image == null)
                MessageBox.Show("请先打开碑帖");
            else
            {
                double ratio = 1.25;
                //调整画布大小
                copybook.Height = copybook.ActualHeight * ratio;
                copybook.Width = copybook.ActualWidth * ratio;
                //调整线和方框位置
                lvm.ImageSizeChange(ratio);
            }

        }

        private void Button_Click_ZoomOut(object sender, RoutedEventArgs e)
        {
            //缩小图片
            if (baseCopybookShow.Image == null)
                MessageBox.Show("请先打开碑帖");
            else
            {
                double ratio = 0.8;
                //调整画布大小
                copybook.Height = copybook.ActualHeight * ratio;
                copybook.Width = copybook.ActualWidth * ratio;
                //调整线和方框位置
                lvm.ImageSizeChange(ratio);
            }
        }

        private void Button_Click_Fullscreen(object sender, RoutedEventArgs e)
        {
            if (baseCopybookShow.Image == null)
                MessageBox.Show("请先打开碑帖");
            else
            {
                //全屏切换
                if (baseCopybookShow._imagestatus == BaseCopybook.statuses.norm)
                {
                    //横向比
                    double horizontalRatio = ScrollViewer.ActualWidth / copybook.ActualWidth;
                    //纵向比
                    double verticalRatio = ScrollViewer.ActualHeight / copybook.ActualHeight;
                    //转换比例
                    double ratio = horizontalRatio < verticalRatio ? horizontalRatio : verticalRatio;
                    //double ratio = 1.25;
                    copybook.Height = baseCopybookShow.Image_baseheight * ratio;
                    copybook.Width = baseCopybookShow.Image_basewidth * ratio;
                    //调整线和方框位置
                    //ZoomInOut.ScaleX = 1.25;
                    //ZoomInOut.ScaleY = 1.25;
                    lvm.ImageSizeChange(ratio);
                    baseCopybookShow._imagestatus = BaseCopybook.statuses.fullscreen;
                    FullScreen.Content = "复原";
                }
                else
                {
                    copybook.Width = baseCopybookShow.Image_basewidth;
                    copybook.Height = baseCopybookShow.Image_baseheight;
                    lvm.ImageSizeChange(baseCopybookShow.Image_basewidth / copybook.ActualWidth);
                    baseCopybookShow._imagestatus = BaseCopybook.statuses.norm;
                    FullScreen.Content = "全屏";
                }
            }
        }


    }
}
