using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace CalligraphyEditor.CalligraphyData
{
    public partial class T_Rubbing
    {        
        [DoNotSerialize]
        public string RandomRubbingPhoto
        {
            get
            {
                if (ID != 0)
                {
                    if(T_RubbingPhoto != null && T_RubbingPhoto.Count > 0)
                        return T_RubbingPhoto[0].PhotoBitmapImage;
                }
                return string.Empty;
            }
        }
    }
}
