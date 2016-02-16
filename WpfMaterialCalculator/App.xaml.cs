using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;                 

namespace WpfMaterialCalculator
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            //如果验证失败，关闭当前程序
            MessageBox.Show("遇到不可预知的问题，即将关闭程序");
            Application.Current.Shutdown();
        }
        
    }
}
