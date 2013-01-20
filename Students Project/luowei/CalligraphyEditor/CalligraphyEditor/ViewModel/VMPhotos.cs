using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Services.Client;
using CalligraphyEditor.CalligraphyData;
using System.Windows.Media.Imaging;
using System.Windows;

namespace CalligraphyEditor.ViewModel
{
    public class VMPhotos
    {
        private DataServiceCollection<T_RubbingPhoto> _rubbingPhotos;
        
        int _currentPhotoIdx;

        public VMPhotos(DataServiceCollection<T_RubbingPhoto> rubbingPhotos, T_RubbingPhoto currentRubbingPhoto)
        {
            _rubbingPhotos = rubbingPhotos;
            _currentPhotoIdx = _rubbingPhotos.IndexOf(currentRubbingPhoto);
        }

        public bool IsCanNext()
        {
            return _currentPhotoIdx < _rubbingPhotos.Count -1 ;
        }

        public bool IsCanPre()
        {
            return _currentPhotoIdx > 0;
        }

        public void Pre()
        {
            if (IsCanPre())
            {
                _currentPhotoIdx--;
            }
        }
        public void Next()
        {
            if (IsCanNext())
            {
                _currentPhotoIdx++;
            }
        }

        public string GetCurrentBitmapImage()
        {
            return _rubbingPhotos[_currentPhotoIdx].PhotoBitmapImage;
        }


    }
}
