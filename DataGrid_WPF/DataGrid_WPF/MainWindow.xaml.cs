using DataGrid_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataGrid_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

       
        public MainWindow()
        {
            InitializeComponent();
            var list = new List<ViewDataGrid>();
            for (int i = 0; i < 10000; i++)
            {
                list.Add(new ViewDataGrid
                {
                    Id = i,
                    Description = $"Item A SDKMUPXLXJ {i}",
                    Name = "i.ToString()",
                    Supbrice = "DescHellHellskjdakjshdkasdlkasoBoyHellskjdakjshdkasdlkasoBoyHellskjdakjshdkasdlkasoBoyoBoyHellHellskjdakjshdkasdlkasoBoyHellskjdakjshdkasdlkasoBoyHellskjdakjshdkasdlkasoBoyoBoy",
                    Data1 = "HelloView "+i,
                    Data2 = i+ "Dataview 2",
                    Data3 = $"Data {i} View"
                    ,Data4 = i+"IDX",
                    Data5 = i+"TeamViewer ",
                    Data6 = "ITX Viewer  "+ i,
                    Data7 = "DX TeamViewer HEHE "+i,
                    Data8 = "HEHEVIEWER ="+i,
                    Data9 ="HueDAY HUE DAY",
                    Data10 = "TAO LA HUE DAY",
                    Data11 = "Hue LA TAO ",
                    Data12 = "HWWXALAJSDKJS",
                    Data13 = "WUWYEIQDSKA SKHDKAJSDKSA",
                    Data14 = "WIKDHKQHDKHASID",
                    Data15 ="WWHXKAQQHJDLKJSDLJSLD"
                });
            }
            grid1.ItemsSource = list;
        }
        private void columnsCfgChanged(object sender, RoutedEventArgs e)
        {
            /*var hidden = new HashSet<int>();
            var frozen = new HashSet<int>();
            if (chbHideColumn3.IsChecked == true) hidden.Add(2);
            if (chbFreezeColumn5.IsChecked == true) frozen.Add(4);
            _model1.SetColumnArrange(hidden, frozen);*/
           
        }
        private void grid1_SelectedCellsChanged(object sender, DataGrid_Fast.SelectionChangedEventArgs e)
        {

            /* var view =grid1;
             if (view.GetSelectedModelCells().Count > 1)
             {
                MessageBox.Show(view.ToString());
                 //view.ShowSelectionMenu(new string[] { "CMD1", "CMD2" });
             }
             else
             {
                 view.ShowSelectionMenu(null);
             }*/
        }
    }
}
