using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalligraphyEditor.CalligraphyData;
using System.Windows.Media.Imaging;

namespace CalligraphyEditor.ViewModel
{
    public  class LoadPhotoHelper
    {
        static Dictionary<T_Photo, BitmapImage> _photoBitmapImage = new Dictionary<T_Photo, BitmapImage>();
        static Random _random = new Random();

        public  BitmapImage GetFirstRubbingBtimapImageByAuthor(T_Author author)
        {
            var q = from r in CalligraphyEditor.App.Entities.T_Rubbing.Expand("T_RubbingPhoto").Expand("T_RubbingPhoto/T_Photo") where r.AuthorID.Equals(author.ID) && (r.IsDeleted == null || r.IsDeleted == false) select r;
            return GetFirstRubbingPhotoBtimapImageByRubbing(q.First<T_Rubbing>());
        }

        public static BitmapImage GetFirstRubbingPhotoBtimapImageByRubbing(T_Rubbing r)
        {
            return GetBtimapImageByRubbingPhoto(r.T_RubbingPhoto[0]);
        }

        public static BitmapImage GetBtimapImageByRubbingPhoto(T_RubbingPhoto rp)
        {
            return GetBtimapImageByPhoto(rp.T_Photo);
        }

        public static BitmapImage GetBtimapImageByPhoto(T_Photo p)
        {
            if (!_photoBitmapImage.ContainsKey(p))
            {

                var uri = CalligraphyEditor.App.Entities.GetReadStreamUri(p);
                _photoBitmapImage.Add(p, new  BitmapImage(uri));
            }

            return _photoBitmapImage[p];
        }
    }
}
