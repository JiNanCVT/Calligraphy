using CutPhote.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CutPhote.ViewModel
{
    public class VMCopybookcut
    {
        //CollectionViewSource _cvs_CopybookCut;

        //public CollectionViewSource Cvs_Copybook
        //{
        //    get { return _cvs_CopybookCut; }
        //    set { _cvs_CopybookCut = value; }
        //}

        public ObservableCollection<CopybookCut> _ocl_CopybookCut;

        public ObservableCollection<CopybookCut> Ocl_CopybookCut
        {
            get { return _ocl_CopybookCut; }
            set { _ocl_CopybookCut = value; }
        }

        public VMCopybookcut()
        {
            _ocl_CopybookCut = new ObservableCollection<CopybookCut>();
        }

        public void delet(int n )
        {
            _ocl_CopybookCut.RemoveAt(n);
        }

        public void save(string path)
        {
            foreach (CopybookCut c in Ocl_CopybookCut)
                c.ImgSave(path);
        }
    }
}
