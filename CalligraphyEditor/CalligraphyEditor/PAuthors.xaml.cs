using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CalligraphyEditor
{
    /// <summary>
    /// PAuthor.xaml 的交互逻辑
    /// </summary>
    public partial class PAuthor : Page
    {
        public event EventHandler<NavigateToRubbingArg> NavigateToRubbing;
        
        public PAuthor()
        {
            InitializeComponent();
            CollectionViewSource cvs = GetViewSource();
            cvs.View.CurrentChanging += new System.ComponentModel.CurrentChangingEventHandler(View_CurrentChanging);
            cvs.View.CurrentChanged += new EventHandler(View_CurrentChanged);
            Txb_Description.TextChanged += new TextChangedEventHandler(Txb_Description_TextChanged);
        }

        void Txb_Description_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetBtnIsEnable(true);
        }

        void View_CurrentChanged(object sender, EventArgs e)
        {
            SetBtnIsEnable(false);
        }

        private void View_CurrentChanging(object sender, System.ComponentModel.CurrentChangingEventArgs e)
        {
            if (Btn_Save.IsEnabled == true)
            {
                if (MessageBox.Show("确定放弃修改吗？", "确认", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    UndoAll();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void UndoAll()
        {
            while (Txb_Description.CanUndo)
            {
                Txb_Description.Undo();
            }
        }

        private CollectionViewSource GetViewSource()
        {
            return (CollectionViewSource)Grd_Authors.DataContext; 
        }

        private VMAuthors GetVMAuthor()
        {
            return (VMAuthors)this.Resources["Vm_authos"]; 
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }


        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            WAddAuthor w = new WAddAuthor(GetVMAuthor());
            w.ShowDialog();
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            CalligraphyEditor.App.Entities.UpdateObject(Lst_Authors.SelectedItem);
            CalligraphyEditor.App.Entities.SaveChanges();
            SetBtnIsEnable(false);
        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            VMAuthors vma = GetVMAuthor();
            vma.DeleteCurrentItem();
        }

        private void SetBtnIsEnable(bool isEnable)
        {
            if (Btn_Cancel != null && Btn_Save != null)
            {
                Btn_Cancel.IsEnabled = isEnable;
                Btn_Save.IsEnabled = isEnable;
            }
        }

        private void Lst_Authors_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            T_Author a = Lst_Authors.SelectedItem as T_Author;
            if( a != null)
            {
                if (NavigateToRubbing != null)
                {
                    NavigateToRubbingArg arg = new NavigateToRubbingArg();
                    arg.Author = a;
                    NavigateToRubbing(this, arg);
                }
            }
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            UndoAll();
            SetBtnIsEnable(false);
        } 
    }

    public class NavigateToRubbingArg : EventArgs
    {
        public T_Author Author
        {
            get;
            set;
        }
    }
}
