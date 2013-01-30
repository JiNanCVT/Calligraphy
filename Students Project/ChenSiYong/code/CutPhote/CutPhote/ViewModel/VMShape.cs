using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CutPhote.Entities;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CutPhote.ViewModel
{
    class VMShape : INotifyPropertyChanged
    {
        //public DrawLine _line;
        private int _row;

        public int Row
        {
            get { return _row; }
            set {
                if (_row != value)
                {
                    _row = value;
                    INotifyPropertyChanged();
                }
            }
        }

        private int _column;

        public int Column
        {
            get { return _column; }
            set
            {
                if (_column != value)
                {
                    _column = value;
                    INotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        //public event PropertyChangingEventHandler PropertyChanging;

        public ObservableCollection<Lines> _LineSets { get; set; }

        private ObservableCollection<Rect> _Rect;

        public ObservableCollection<Rect> Rect
        {
            get { return _Rect; }
            set { _Rect = value; }
        }

        private double image_width;

        public double Image_width
        {
            get { return image_width; }
            set
            {
                if (_column != value)
                {
                    image_width = value;
                    //INotifyPropertyChanged();
                }
            }
        }

        private double image_height;

        public double Image_height
        {
            get { return image_height; }
            set
            {
                if (_column != value)
                {
                    image_height = value;
                    //INotifyPropertyChanged();
                }
            }
        }

        //private Rectangles _Rect;

        //public Rectangles Rect
        //{
        //    get { return _Rect; }
        //    set { _Rect = value; }
        //}

        public VMShape()
        {
            //image_width = 1000;
            //image_height = 1000;
            _row = 2;
            _column = 2;
            _LineSets =new ObservableCollection<Lines>();
            //LineSet();
            
            _Rect = new ObservableCollection<Rect>();
            //_Rect.Add(new Rect());
        }

        public void LineSet()
        {
            //_LineSets = new ObservableCollection<Lines>();
            _LineSets.Clear();
            if (_row != 0 & _column != 0)
            {
                double _gridHeight = Image_height / _row;
                double _gridWidth = Image_width / _column;
                for (int i = 0; i < _column; i++)
                    for (int j = 0; j <= _row; j++)
                    {
                        _LineSets.Add(new Lines(_gridWidth * i, _gridWidth * (i+1), _gridHeight * j, _gridHeight * j));
                    }
                for (int i = 0; i <= _column; i++)
                    _LineSets.Add(new Lines(_gridWidth * i, _gridWidth * i, 0, image_height));
            }
        }

        public void INotifyPropertyChanged()
        {
            if (PropertyChanged != null)
                LineSet();
        }

        public void ImageSizeChange(double height, double width)
        {
            Image_height = height;
            Image_width = width;
            LineSet();
        }

        public void ImageSizeChange(double ratio)
        {
            Image_height = Image_height * ratio;
            Image_width = Image_width * ratio;
            if (_LineSets != null)
            {
                for (int i = 0; i < _LineSets.Count; i++)
                {
                    Lines l = _LineSets[i];
                    _LineSets[i] = new Lines(l.X1 * ratio, l.X2 * ratio, l.Y1 * ratio, l.Y2 * ratio);
                }
            }
            if (_Rect.Count!=0)
            {
                _Rect[0].mg = new System.Windows.Thickness(_Rect[0].mg.Left * ratio, _Rect[0].mg.Top * ratio, 0, 0);
                _Rect[0].RectHeight = _Rect[0].RectHeight * ratio;
                _Rect[0].RectWidth = _Rect[0].RectWidth * ratio;
            }
        }
    }
}
