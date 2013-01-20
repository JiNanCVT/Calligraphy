using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Windows.Media.Imaging;

namespace CalligraphyEditor.CalligraphyData
{
    public partial class T_Author
    {
        static Random _random = new Random();
        [DoNotSerialize]
        public string RandomRubbingPhoto
        {
            get
            {
                
                    var q = from r in CalligraphyEditor.App.Entities.T_Rubbing.Expand("T_RubbingPhoto").Expand("T_RubbingPhoto/T_Photo").Expand("T_Author") where (r.IsDeleted == null || r.IsDeleted == false) && r.AuthorID.Equals(this.ID) select r;
                    if (q.Count<T_Rubbing>() > 0)
                    {
                        T_Rubbing rubbing = q.First<T_Rubbing>();
                        return rubbing.RandomRubbingPhoto;
                    }
                    return string.Empty;
            }
        }
    }
}
