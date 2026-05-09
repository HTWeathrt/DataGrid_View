# DataGrid_View — Siêu Nhanh cho WPF

Một control **DataGrid tốc độ cao** dành cho WPF, được xây dựng trên nền tảng `WriteableBitmapEx`. Thay vì tạo từng `UIElement` cho mỗi ô, control này vẽ toàn bộ lưới dữ liệu trực tiếp lên một `WriteableBitmap`, giúp hiển thị **hàng chục nghìn dòng** mà không bị giật lag.

---

## Mục lục

- [Tính năng](#tính-năng)
- [Yêu cầu hệ thống](#yêu-cầu-hệ-thống)
- [Cấu trúc dự án](#cấu-trúc-dự-án)
- [Bắt đầu nhanh](#bắt-đầu-nhanh)
- [Sử dụng trong XAML](#sử-dụng-trong-xaml)
- [Model dữ liệu](#model-dữ-liệu)
  - [GridModel1\<T\> — Generic model từ List](#gridmodel1t--generic-model-từ-list)
  - [DataTableGridModel — Model từ DataTable](#datatablegridmodel--model-từ-datatable)
  - [FastGridModelBase — Tự tạo model tùy chỉnh](#fastgridmodelbase--tự-tạo-model-tùy-chỉnh)
- [API quan trọng](#api-quan-trọng)
- [Thư viện phụ thuộc](#thư-viện-phụ-thuộc)
- [License](#license)

---

## Tính năng

| Tính năng | Mô tả |
|-----------|-------|
| ⚡ Hiệu năng cao | Render bằng `WriteableBitmap`, không tạo UIElement cho từng ô |
| 📋 Hỗ trợ 10 000+ dòng | Cuộn mượt mà với lượng dữ liệu lớn |
| 🔒 ReadOnly / Editable | Hỗ trợ cả chế độ chỉ đọc và chỉnh sửa trực tiếp |
| 📌 Freeze / Hide cột & hàng | Cố định hoặc ẩn cột/hàng tùy ý |
| 🗂️ Generic model | `GridModel1<T>` tự động đọc tất cả `public property` qua Reflection |
| 📊 DataTable model | `DataTableGridModel` cho phép bind thẳng từ `System.Data.DataTable` |
| 🎨 Tùy biến giao diện | Override màu nền, màu chữ, in đậm, in nghiêng, tooltip, decoration cho từng ô |
| 🖱️ Selection menu | Hiển thị context menu khi chọn nhiều ô |
| 🔄 Transpose | Hỗ trợ chế độ hoán đổi hàng/cột |

---

## Yêu cầu hệ thống

- **Visual Studio** 2017 trở lên (hoặc MSBuild tương đương)
- **.NET Framework** 4.5+ (WPF)
- **Platform**: x64 (cấu hình Debug mặc định là `x64`)
- Thư viện `WriteableBitmapEx.Wpf.dll` (đã có sẵn trong thư mục `Lib/`)

---

## Cấu trúc dự án

```
DataGrid_View/
├── README.md
└── DataGrid_WPF/
    ├── DataGrid_WPF.sln              # Solution file
    ├── Lib/
    │   └── WriteableBitmapEx.Wpf.dll # Thư viện render bitmap
    └── DataGrid_WPF/
        ├── App.xaml / App.xaml.cs
        ├── MainWindow.xaml           # Cửa sổ demo
        ├── MainWindow.xaml.cs
        ├── Models/
        │   └── Class1.cs             # ViewDataGrid model & GridModel1<T>
        └── DataGrid_Fast/            # Toàn bộ logic của FastGridControl
            ├── FastGridControl.xaml              # XAML của control
            ├── FastGridControl.xaml.cs           # Entry point, vòng đời
            ├── FastGridControl_Render.cs         # Vẽ lưới lên WriteableBitmap
            ├── FastGridControl_Arrange.cs        # Tính toán layout
            ├── FastGridControl_Input.cs          # Xử lý bàn phím & chuột
            ├── FastGridControl_Selection.cs      # Logic chọn ô/hàng/cột
            ├── FastGridControl_Invalidation.cs   # Invalidate / refresh
            ├── FastGridControl_DependencyProps.cs# Dependency properties
            ├── FastGridControl_StyleProps.cs     # Style & theme
            ├── IFastGridModel.cs                 # Interface dữ liệu
            ├── IFastGridView.cs                  # Interface view
            ├── IFastGridCell.cs                  # Interface ô
            ├── IFastGridCellBlock.cs             # Interface block trong ô
            ├── FastGridModelBase.cs              # Base class tiện ích
            ├── FastGridCellImpl.cs               # Triển khai IFastGridCell
            ├── FastGridCellAddress.cs            # Địa chỉ (row, col)
            ├── DataTableGridModel.cs             # Model cho DataTable
            ├── ExplicitColumnDefinition.cs       # Định nghĩa cột tùy chỉnh
            ├── SeriesSizes.cs                    # Kích thước hàng/cột
            ├── ActiveSeries.cs                   # Quản lý dải hiển thị
            ├── ImageHolder.cs                    # Cache ảnh
            ├── EventArgs.cs                      # Event arguments
            ├── SelectionChangedEventArgs.cs      # Sự kiện thay đổi selection
            ├── SelectionQuickCommand.cs          # Quick command khi chọn
            └── model_cls_show.cs                 # Model hiển thị ví dụ
```

---

## Bắt đầu nhanh

1. **Clone** repo về máy.
2. Mở `DataGrid_WPF/DataGrid_WPF.sln` bằng Visual Studio.
3. Build & Run (`F5`).  
   Cửa sổ demo sẽ hiển thị **10 000 dòng** dữ liệu mẫu với 19 cột.

---

## Sử dụng trong XAML

Thêm namespace và đặt control vào layout:

```xml
<Window ...
        xmlns:FastGrid="clr-namespace:DataGrid_WPF.DataGrid_Fast">
    <Grid>
        <FastGrid:FastGridControl x:Name="grid1"
                                  IsReadOnly="True"
                                  SelectedCellsChanged="grid1_SelectedCellsChanged" />
    </Grid>
</Window>
```

---

## Model dữ liệu

### GridModel1\<T\> — Generic model từ List

Tự động đọc tất cả `public property` của kiểu `T` qua Reflection.  
Phù hợp khi bạn đã có sẵn một `List<T>` và muốn hiển thị ngay.

```csharp
// Định nghĩa model
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// Bind dữ liệu
var products = Enumerable.Range(0, 10000)
    .Select(i => new Product { Id = i, Name = $"Product {i}", Price = i * 1.5m })
    .ToList();

grid1.ItemsSource = products; // FastGridControl nhận IEnumerable
```

> Tên cột được lấy tự động từ tên property (`Id`, `Name`, `Price`).  
> Ô có thể chỉnh sửa nếu `IsReadOnly = false`.

---

### DataTableGridModel — Model từ DataTable

```csharp
var table = new DataTable();
table.Columns.Add("STT", typeof(int));
table.Columns.Add("Tên", typeof(string));
for (int i = 0; i < 5000; i++)
    table.Rows.Add(i, $"Nhân viên {i}");

var model = new DataTableGridModel { DataSource = table };
grid1.Model = model;
```

Dùng `ExplicitColumnDefinition` để chọn cột hiển thị và đặt tiêu đề riêng:

```csharp
model.ExplicitColumns = new List<ExplicitColumnDefinition>
{
    new ExplicitColumnDefinition { DataField = "STT",  HeaderText = "Số thứ tự" },
    new ExplicitColumnDefinition { DataField = "Tên",  HeaderText = "Họ và tên" },
};
```

---

### FastGridModelBase — Tự tạo model tùy chỉnh

Kế thừa `FastGridModelBase` và override các phương thức cần thiết:

```csharp
public class MyModel : FastGridModelBase
{
    private readonly string[,] _data;

    public MyModel(int rows, int cols)
    {
        _data = new string[rows, cols];
    }

    public override int RowCount => _data.GetLength(0);
    public override int ColumnCount => _data.GetLength(1);

    public override string GetCellText(int row, int column)
        => _data[row, column] ?? string.Empty;

    public override string GetColumnHeaderText(int column)
        => $"Cột {column + 1}";
}
```

#### Làm mới dữ liệu

```csharp
model.InvalidateCell(row, column);   // Làm mới một ô
model.InvalidateAll();               // Làm mới toàn bộ lưới
model.NotifyAddedRows();             // Thông báo có thêm dòng mới
model.NotifyRefresh();               // Yêu cầu vẽ lại toàn bộ
```

#### Ẩn / Đóng băng cột & hàng

```csharp
model.SetColumnArrange(
    hidden: new HashSet<int> { 2, 5 },   // Ẩn cột 3 và 6
    frozen: new HashSet<int> { 0 }       // Cố định cột 1
);

model.SetRowArrange(
    hidden: new HashSet<int>(),
    frozen: new HashSet<int> { 0 }       // Cố định hàng tiêu đề
);
```

---

## API quan trọng

### FastGridControl (Dependency Properties)

| Property | Kiểu | Mô tả |
|----------|------|-------|
| `Model` | `IFastGridModel` | Model dữ liệu của grid |
| `ItemsSource` | `IEnumerable` | Bind list đối tượng (dùng `GridModel1<T>` bên trong) |
| `IsReadOnly` | `bool` | Chế độ chỉ đọc |
| `IsTransposed` | `bool` | Hoán đổi hàng / cột |

### Sự kiện

| Sự kiện | Mô tả |
|---------|-------|
| `SelectedCellsChanged` | Khi vùng chọn thay đổi |

### IFastGridModel

| Thành viên | Mô tả |
|-----------|-------|
| `RowCount` | Số dòng |
| `ColumnCount` | Số cột |
| `GetCell(view, row, col)` | Trả về thông tin ô |
| `GetRowHeader(view, row)` | Tiêu đề hàng |
| `GetColumnHeader(view, col)` | Tiêu đề cột |
| `GetHiddenColumns(view)` | Tập cột bị ẩn |
| `GetFrozenColumns(view)` | Tập cột bị đóng băng |
| `HandleCommand(...)` | Xử lý lệnh từ ô |
| `HandleSelectionCommand(...)` | Xử lý lệnh khi chọn |

---

## Thư viện phụ thuộc

| Thư viện | Phiên bản | Mục đích |
|---------|-----------|---------|
| [WriteableBitmapEx](https://github.com/reneschulte/WriteableBitmapEx) | WPF build | Vẽ đường, hình chữ nhật, bitmap tốc độ cao trên `WriteableBitmap` |

Thư viện đã được nhúng sẵn trong `DataGrid_WPF/Lib/`, không cần cài thêm NuGet.

---

## License

Dự án này được phát hành theo giấy phép **MIT**. Xem file `LICENSE` (nếu có) để biết thêm chi tiết.
