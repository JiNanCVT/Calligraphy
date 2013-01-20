using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalligraphyEditor
{
    class MainViewModel
    {
        public MainViewModel(string[] photoPaths)
        {
            this.Images = new List<string>();
            foreach (var p in photoPaths)
            {
                if (p != null)
                    this.Images.Add(p);
            }
        }
        private List<String> _Images;

        public List<String> Images
        {
            get { return _Images; }
            set { _Images = value; }
        }
    }
}
