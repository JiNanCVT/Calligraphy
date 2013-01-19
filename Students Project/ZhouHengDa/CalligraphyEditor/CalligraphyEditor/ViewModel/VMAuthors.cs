using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using CalligraphyEditor.CalligraphyData;
using System.Windows.Data;

namespace CalligraphyEditor.ViewModel
{
    public class VMAuthors 
    {
        private CollectionViewSource _cvs_authors;

        public CollectionViewSource Cvs_authors
        {
            get { return _cvs_authors; }
        }

        private ObservableCollection<T_Author> _ocl_authors;

        public ObservableCollection<T_Author> Ocl_Authors
        {
            get { return _ocl_authors; }
            set { _ocl_authors = value; }
        }

        public VMAuthors()
        {
            try
            {
                var qAuthors = from a in CalligraphyEditor.App.Entities.T_Author where a.IsDeleted == null || a.IsDeleted == false select a;
                _ocl_authors = new ObservableCollection<T_Author>(qAuthors);
                _cvs_authors = new CollectionViewSource();
                _cvs_authors.Source = _ocl_authors;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "----");
            }
        }

        public void SaveCurrentItem()
        {
            CalligraphyEditor.App.Entities.UpdateObject(_cvs_authors.View.CurrentItem);
            //CalligraphyEditor.App.Entities.BeginSaveChanges(new AsyncCallback(SaveCallBack), null);
            CalligraphyEditor.App.Entities.SaveChanges();
        }

        //private void SaveCallBack(IAsyncResult result)
        //{
        //    if (result.IsCompleted)
        //        System.Diagnostics.Debug.WriteLine("save completed");
           
        //}

        public void AddAuthor(T_Author a)
        {
            CalligraphyEditor.App.Entities.AddToT_Author(a);
            CalligraphyEditor.App.Entities.SaveChanges();
            _ocl_authors.Add(a);
        }

        public void SaveEidtedCurrentItem()
        {
            if (_cvs_authors.View.CurrentItem == null)
                return;

            CalligraphyEditor.App.Entities.UpdateObject(_cvs_authors.View.CurrentItem);
            CalligraphyEditor.App.Entities.SaveChanges();
        }

        public void DeleteCurrentItem()
        {
            T_Author a = _cvs_authors.View.CurrentItem as T_Author;

            if ( a == null)
                return;

            a.IsDeleted = true;

            CalligraphyEditor.App.Entities.UpdateObject(a);
            CalligraphyEditor.App.Entities.SaveChanges();

            _ocl_authors.Remove((T_Author)_cvs_authors.View.CurrentItem);
        }

    }
}
