using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace CalligraphyEditor.CalligraphyData
{
    public partial class T_RubbingPhoto
    {
        private string _photoBitmapImage = string.Empty;
        [DoNotSerialize]
        public string PhotoBitmapImage
        {
            get
            {
                if (string.IsNullOrEmpty(_photoBitmapImage) && ID != 0)
                {
                    _photoBitmapImage = CalligraphyEditor.App.Entities.GetReadStreamUri(T_Photo).ToString();
                }
                return _photoBitmapImage;
            }
        }
    }
}
