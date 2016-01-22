using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfMaterialCalcualator.Service
{
    public class DialogService : IDialogService
    {
        public bool ShowDialog(string content, string caption)
        {
            return MessageBox.Show(content, caption, MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK;
        }
    }
}
