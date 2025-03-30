using DataGrid_WPF.DataGrid_Fast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataGrid_WPF.Models
{
    internal class ViewDataGrid
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Supbrice { get; set; }
        public string Data1 { get; set; }
        public string Data2 { get; set; }
        public string Data3 { get; set; }
        public string Data4 { get; set; }
        public string Data5 { get; set; }
        public string Data6 { get; set; }
        public string Data7 { get; set; }
        public string Data8 { get; set; }
        public string Data9 { get; set; }
        public string Data10 { get; set; }
        public string Data11 { get; set; }
        public string Data12 { get; set; }
        public string Data13 { get; set; }
        public string Data14 { get; set; }
        public string Data15 { get; set; }
    }
    public class GridModel1<T> : FastGridModelBase
    {
        private readonly List<T> _items;
        private readonly PropertyInfo[] _properties;

        public GridModel1(IEnumerable<T> items)
        {
            _items = items?.ToList() ?? new List<T>();
            _properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
        private Dictionary<Tuple<int, int>, string> _editedCells = new Dictionary<Tuple<int, int>, string>();

        public override int ColumnCount
        {
            get { return _properties.Length; }
        }
        public override int RowCount
        {
            get { return _items.Count; }
        }
        public override string GetColumnHeaderText(int column)
        {
            return _properties[column].Name;
        }
        public override string GetCellText(int row, int column)
        {
            var key = Tuple.Create(row, column);
            if (_editedCells.ContainsKey(key))
            {
                return _editedCells[key];
            }
            // Lấy item và property
            var item = _items[row];
            var prop = _properties[column];
            // Lấy giá trị của property từ item
            var value = prop.GetValue(item);
            // Tránh null reference
            return value?.ToString() ?? string.Empty;
        }
        /*public override string GetCellText(int row, int column)
        {
            var key = Tuple.Create(row, column);
            if (_editedCells.ContainsKey(key)) return _editedCells[key];
            return String.Format("{0}{1},{2}", _columnBasicNames[column % _columnBasicNames.Length], row + 1, column + 1);
        }*/
        public override void SetCellText(int row, int column, string value)
        {
            var key = Tuple.Create(row, column);
            _editedCells[key] = value;
        }
        public override IFastGridCell GetGridHeader(IFastGridView view)
        {
            var impl = new FastGridCellImpl();/*
            var btn = impl.AddImageBlock(view.IsTransposed
                ? "/Images/flip_horizontal_small.png"
                : "/Images/flip_vertical_small.png");
           // btn.CommandParameter = FastWpfGrid.FastGridControl.ToggleTransposedCommand;
            btn.ToolTip = "Swap rows and columns";*/
            return impl;
        }
        /* public override IFastGridCell GetGridHeader(IFastGridView view)
         {
             var impl = new FastGridCellImpl();
             var btn = impl.AddImageBlock(view.IsTransposed ? "/Images/flip_horizontal_small.png" : "/Images/flip_vertical_small.png");
             btn.CommandParameter = FastWpfGrid.FastGridControl.ToggleTransposedCommand;
             btn.ToolTip = "Swap rows and columns";
             impl.AddImageBlock("/Images/foreign_keysmall.png").CommandParameter = "FK";
             impl.AddImageBlock("/Images/primary_keysmall.png").CommandParameter = "PK";
             return impl;
         }*/
        public override void HandleCommand(IFastGridView view, FastGridCellAddress address, object commandParameter, ref bool handled)
        {
            base.HandleCommand(view, address, commandParameter, ref handled);
            if (commandParameter is string) MessageBox.Show(commandParameter.ToString());
        }

        public override int? SelectedRowCountLimit
        {
            get { return 100; }
        }

        public override void HandleSelectionCommand(IFastGridView view, string command)
        {
            MessageBox.Show(command);
        }
    }
}
