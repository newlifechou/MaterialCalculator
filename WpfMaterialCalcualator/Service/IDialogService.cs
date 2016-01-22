using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMaterialCalcualator.Service
{
    public interface IDialogService
    {
        bool ShowDialog(string content, string caption);
    }
}
