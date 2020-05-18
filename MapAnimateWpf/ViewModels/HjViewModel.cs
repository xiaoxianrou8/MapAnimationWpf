using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapAnimateWpf.ViewModels
{
    [POCOViewModel]
    public class HjViewModel
    {
       
        public virtual string Name { get; set; }
        public virtual GeoPoint Location { get; set; }

    }
}
