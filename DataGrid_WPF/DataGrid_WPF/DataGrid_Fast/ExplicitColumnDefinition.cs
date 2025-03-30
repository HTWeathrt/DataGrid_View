using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid_WPF.DataGrid_Fast
{
    public class ExplicitColumnDefinition
    {
        public string DataField;
        public string HeaderText;

        public ExplicitColumnDefinition(string dataField, string headerText)
        {
            DataField = dataField;
            HeaderText = headerText;
        }

        public ExplicitColumnDefinition(string name)
        {
            DataField = name;
            HeaderText = name;
        }
    }
}
