using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using 通量数据处理中心.Windows;
using System.Windows.Media;

namespace 通量数据处理中心.Core
{
    public class Core
    {
        /// <summary>
        /// 打开新的数据处理窗口
        /// </summary>
        /// <param name="paths"></param>
        public static void OpenNewDataProcessingWindow(string[] paths, bool isDynamicLoading)
        {
            foreach (var path in paths)
            {
                OpenNewDataProcessingWindow(path, isDynamicLoading);
            }
        }

        /// <summary>
        /// 打开新的数据处理窗口
        /// </summary>
        /// <param name="path"></param>
        public static void OpenNewDataProcessingWindow(string path, bool isDynamicLoading)
        {
            // 创建DataProcessingWindow实例并传入path参数
            DataProcessingWindow dataProcessingWindow = new DataProcessingWindow(path, isDynamicLoading);
            // 显示新窗口
            if (!dataProcessingWindow.HasError)
            {
                dataProcessingWindow.Show();
            }
            else
            {
                dataProcessingWindow.Close();
            }
        }

        /// <summary>
        /// 获取文件的扩展名（包括点号）
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileExtension(string path)
        {
            if (File.Exists(path))
            {
                // 获取文件的扩展名（包括点号）并转换为小写
                string extension = Path.GetExtension(path).ToLower();
                return extension;
            }
            else
            {
                return "ERROR";
            }
        }

        /// <summary>
        /// 读取csv文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static DataTable ReadCsvFile(string filePath)
        {
            DataTable dataTable = new DataTable();
            using (StreamReader sr = new StreamReader(filePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dataTable.Columns.Add(header);
                }

                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dataRow[i] = rows[i];
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }
            return dataTable;
        }

        //////////////////// 暂时未实现（动态加载部分） ////////////////////
        //    public static IEnumerable<dynamic> ReadDataFromCsv(string path, int start, int count)
        //    {
        //        using (var reader = new StreamReader(path))
        //        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        //        {
        //            csv.Read();
        //            csv.ReadHeader();
        //            var headers = csv.HeaderRecord;

        //            var records = new List<dynamic>();
        //            int currentRow = 0;

        //            while (csv.Read())
        //            {
        //                if (currentRow >= start + count)
        //                {
        //                    break;
        //                }
        //                if (currentRow >= start)
        //                {
        //                    dynamic record = new ExpandoObject();
        //                    var recordDict = (IDictionary<string, object>)record;
        //                    foreach (var header in headers)
        //                    {
        //                        recordDict[header] = csv.GetField(header);
        //                    }
        //                    records.Add(record);
        //                }
        //                currentRow++;
        //            }
        //            return records;
        //        }
        //    }

        //    public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        //    {
        //        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
        //        {
        //            var child = VisualTreeHelper.GetChild(obj, i);
        //            if (child is T visualChild)
        //            {
        //                return visualChild;
        //            }
        //            var childOfChild = FindVisualChild<T>(child);
        //            if (childOfChild != null)
        //            {
        //                return childOfChild;
        //            }
        //        }
        //        return null;
        //    }
        //////////////////// 暂时未实现（动态加载部分） ////////////////////
    }
}
