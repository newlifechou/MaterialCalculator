using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace WpfMaterialCalcualator.Common
{
    /// <summary>
    /// 通用窗口控制类
    /// </summary>
    public class WindowController
    {
        public WindowController()
        {
            //注册窗口打开消息
            Messenger.Default.Register<NotificationMessage<object>>(this, WindowAction);
        }

        private void WindowAction(NotificationMessage<object> obj)
        {
            string winName = obj.Target.ToString();
            if (obj.Notification=="OpenWindow")
            {
                //得到View的全名
                string ViewName = Assembly.GetExecutingAssembly().GetName().Name + ".View." + winName + "View";
                //利用反射得到View实例
                Window win = (Window)Assembly.GetExecutingAssembly().CreateInstance(ViewName, true);
                if (win!=null)
                {
                    win.ShowDialog();
                }
            }
        }
    }

}
