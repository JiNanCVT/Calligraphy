using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CutPhote.Entities
{
    class BaseCopybook
    {
        public BitmapImage _image;

        public double _imagewidth;

        public double _imageheight;

        public enum statuses
        {
            norm,fullscreen
        }

        public statuses _imagestatus;

        public BaseCopybook()
        {
            _image = null;
            _imagewidth = 0;
            _imageheight = 0;
            _imagestatus = statuses.norm;
        }

        //public event ProgressChangedEventHandler PropertyChanged;

        //public void SetPropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}


        //void BaseCopybook_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    _imageheight = _image.Height;
        //    _imagewidth = _image.Width;
        //}
 
    }
}
