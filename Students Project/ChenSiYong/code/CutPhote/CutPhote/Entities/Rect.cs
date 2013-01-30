using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CutPhote.Entities
{
    class Rect
    {
        public Thickness mg;

        public Thickness Mg
        {
            get { return mg; }
            set { mg = value; }
        }

        //public double _siteY;

        //public double SiteY
        //{
        //    get { return _siteY; }
        //    set { _siteY = value; }
        //}

        public double _rect_width;

        public double RectWidth
        {
            get { return _rect_width; }
            set { _rect_width = value; }
        }

        public double _rect_height;

        public double RectHeight
        {
            get { return _rect_height; }
            set { _rect_height = value; }
        }

        public Rect()
        {
            mg = new Thickness(10,10,0,0);
            _rect_width = 100;
            _rect_height = 100;
        }

        public void RectZoom(double ratio)
        {
            Mg = new Thickness(mg.Left * ratio, mg.Top * ratio, 0, 0);
            RectWidth = RectWidth * ratio;
            RectHeight = RectHeight * ratio;
        }
    }
}
