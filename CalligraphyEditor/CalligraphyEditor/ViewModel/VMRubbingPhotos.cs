using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using CalligraphyEditor.CalligraphyData;
using System.Windows.Data;

namespace CalligraphyEditor.ViewModel
{
    public class VMRubbingPhotos
    {
        private T_Rubbing _rubbing;

        public T_Rubbing Rubbing
        {
            get { return _rubbing; }
        }

        private CollectionViewSource _Cvs_RubbingPhoto;

        public CollectionViewSource Cvs_RubbingPhoto
        {
            get { return _Cvs_RubbingPhoto; }
            set { _Cvs_RubbingPhoto = value; }
        }

        public VMRubbingPhotos(T_Rubbing rubbing)
        {
            _rubbing = rubbing;
            _Cvs_RubbingPhoto = new CollectionViewSource();
            _Cvs_RubbingPhoto.Source = rubbing.T_RubbingPhoto;
            _Cvs_RubbingPhoto.View.SortDescriptions.Add(new System.ComponentModel.SortDescription("PageNumber", System.ComponentModel.ListSortDirection.Ascending));
        }
    }
}
