using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CutPhote.Entities
{
    class BaseCopybook
    {
        private BitmapImage _image;

        public BitmapImage Image
        {
            get { return _image; }
            set { _image = value; }
        }

        private double _image_basewidth;

        public double Image_basewidth
        {
            get { return _image_basewidth; }
            set { _image_basewidth = value; }
        }

        private double _image_baseheight;

        public double Image_baseheight
        {
            get { return _image_baseheight; }
            set { _image_baseheight = value; }
        }

        public enum statuses
        {
            norm,fullscreen
        }

        public statuses _imagestatus;

        public BaseCopybook()
        {
            _image = null;
            //_image_basewidth = 0;
            //_image_baseheight = 0;
            _imagestatus = statuses.norm;
        }

        public void BaseCopybookResetSet(Uri uri)
        {
            Image = new BitmapImage(uri);
            Image_baseheight = Image.Height;
            Image_basewidth = Image.Width;
        }

    }
}
