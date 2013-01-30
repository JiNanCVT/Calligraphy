using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CutPhote.Entities
{
    public class CopybookCut
    {
        BitmapSource _character;

        public BitmapSource Character
        {
          get { return _character; }
          set { _character = value; }
        }

        string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public CopybookCut(BitmapSource imgSource)
        {
            _character = imgSource;
            _name =null;
        }

        public void ImgSave(string path)
        {
            FileStream stream = new FileStream(path+@"\"+_name+".jpg", FileMode.Create);

            JpegBitmapEncoder encoder = new JpegBitmapEncoder();

            encoder.FlipHorizontal = false;

            encoder.FlipVertical = false;

            encoder.QualityLevel = 100;

            encoder.Frames.Add(BitmapFrame.Create(_character));

            encoder.Save(stream);

            stream.Close();
        }
    }
}
