using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
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
using System.Windows.Shapes;
using CsvHelper;

namespace 通量数据处理中心.Windows
{
    /// <summary>
    /// DataProcessingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataProcessingWindow : Window
    {
        // 是否有错误
        public bool HasError;
        public bool IsDynamicLoading;
        // 动态加载所需数据
        private string DataPath;
        //////////////////// 暂时未实现（动态加载部分） ////////////////////
        //private const int PageSize = 500;
        //private ObservableCollection<dynamic> _data;
        //private int _currentPage = 0;
        //////////////////// 暂时未实现（动态加载部分） ////////////////////

        public DataProcessingWindow(string path, bool isDynamicLoading)
        {
            InitializeComponent();
            // 启动时窗口居中
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            // 初始化
            DataPath = path;
            IsDynamicLoading = isDynamicLoading;
            // 根据扩展名打开数据
            string fileType = "未知类型文件";
            switch (Core.Core.GetFileExtension(path))
            {
                case ".csv":
                    fileType = "csv表格文件";
                    LoadCsv(path, isDynamicLoading);
                    break;
                default:
                    break;
            }
            // 设置标题
            this.Title += $"【{System.IO.Path.GetFileName(path)}】【{fileType}】";
        }

        

        /// <summary>
        /// 加载csv文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isDynamicLoading"></param>
        private void LoadCsv(string path, bool isDynamicLoading)
        {
            try
            {
                if (isDynamicLoading)
                {
                    // 启用动态加载
                    //////////////////// 暂时未实现（动态加载部分） ////////////////////
                    //_data = new ObservableCollection<dynamic>(Core.Core.ReadDataFromCsv(path, _currentPage * PageSize, PageSize));
                    //DataGrid.ItemsSource = _data;
                    //_currentPage++;
                    //////////////////// 暂时未实现（动态加载部分） ////////////////////
                }
                else
                {
                    // 不启用动态加载
                    DataTable dataTable = Core.Core.ReadCsvFile(path);
                    DataGrid.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (IOException ex)
            {
                HasError = true;
                MessageBox.Show("打开文件失败: " + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //////////////////// 暂时未实现（动态加载部分） ////////////////////
        //private void DataGrid_ScrollChanged(object sender, ScrollChangedEventArgs e)
        //{
        //    if (!IsDynamicLoading)
        //    {
        //        return;
        //    }
        //    var scrollViewer = Core.Core.FindVisualChild<ScrollViewer>(DataGrid);
        //    if (scrollViewer != null && scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
        //    {
        //        var newData = Core.Core.ReadDataFromCsv(DataPath, _currentPage * PageSize, PageSize).ToList();
        //        if (newData.Any())
        //        {
        //            _data.Clear();
        //            newData.ForEach(item => _data.Add(item));
        //            _currentPage++;
        //        }
        //    }
        //}
        //////////////////// 暂时未实现（动态加载部分） ////////////////////
    }
}
