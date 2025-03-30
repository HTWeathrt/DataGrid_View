using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid_WPF.DataGrid_Fast
{
    public class ActiveSeries
    {
        public HashSet<int> ScrollVisible = new HashSet<int>();
        public HashSet<int> Selected = new HashSet<int>();
        public HashSet<int> Frozen = new HashSet<int>();
    }
}
