using SoftwareAuthorizeLib;
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
            //启动的时候读取设定的deadline，然后检查软件许可时间是否有效来决定是否关闭程序。
            string tmp = WpfMaterialCalculator.Properties.Settings.Default.AuthorizeTime;
            DateTime deadline;
            if (DateTime.TryParse(tmp,out deadline))
            {
                Permisson per = new Permisson();
                if (!per.CheckTime(deadline))
                {
                    //如果验证失败，关闭当前程序
                    MessageBox.Show("遇到未知错误，即将关闭程序");
                    Application.Current.Shutdown();
                }
            }

        }
        
    }
}
