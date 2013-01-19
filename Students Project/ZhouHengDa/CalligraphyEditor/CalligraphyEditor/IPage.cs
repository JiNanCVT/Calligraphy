using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace CalligraphyEditor
{
    public interface IPage
    {
        bool Editable { get;  }
        string Path { get; }
        void Add();
        void Delete();
        void Save();
        event EventHandler<NavigateToPageEventArgs> NavigateToPage;        
    }

    public class NavigateToPageEventArgs : EventArgs
    {
        public Page ToPage
        {
            get;
            set;
        }
    }
}
