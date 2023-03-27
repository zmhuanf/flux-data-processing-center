using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 通量数据处理中心
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // 启动时窗口居中
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// 文件拖入后处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    // 所有的拖入文件
                    Core.Core.OpenNewDataProcessingWindow(file, DynamicLoading.IsChecked ?? false);
                }
            }
        }

        /// <summary>
        /// 文件拖入时图标处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                // 检查拖动的数据是否全部为文件
                bool allFiles = true;
                foreach (string file in files)
                {
                    if (Directory.Exists(file))
                    {
                        allFiles = false;
                        break;
                    }
                }
                // 如果全部为文件，则设置拖放效果为复制（DragDropEffects.Copy）
                if (allFiles)
                {
                    e.Effects = DragDropEffects.Copy;
                }
                else
                {
                    e.Effects = DragDropEffects.None;
                }
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        /// <summary>
        /// 菜单栏文件-打开按钮处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                // 设置文件过滤器，仅显示.dat、.csv和.zdt文件
                Filter = "Dat, Csv, Zdt files (*.dat, *.csv, *.zdt)|*.dat;*.csv;*.zdt"
            };
            // 显示打开文件对话框，并检查用户是否单击了“打开”按钮
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                // 在此处处理文件
                Core.Core.OpenNewDataProcessingWindow(filePath, DynamicLoading.IsChecked ?? false);
            }
        }
    }
}
