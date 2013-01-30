using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace CutPhote.Entities
{
    class Lines
    {
        public double X1 { set; get; }
        public double X2 { set; get; }
        public double Y1 { set; get; }
        public double Y2 { set; get; }

        public Lines(double x1,double x2,double y1,double y2)
        {
            X1 = x1;
            X2 = x2;
            Y1 = y1;
            Y2 = y2;
        }

        public void LineZoom(double ratio)
        {
            X1 = X1 * ratio;
            X2 = X2 * ratio;
            Y1 = Y1 * ratio;
            Y2 = Y2 * ratio;
        }
    }
}
